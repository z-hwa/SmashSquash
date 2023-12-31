﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 處理Unit會發生在遊戲中的實際行為，造成的數據變化，會記錄到UnitData中
 * 每個Unit都應該有一個UnitBehavior
 */

public class UnitBehavior : MonoBehaviour
{
    //狀態機(移動、靜止
    public enum State
    {
        Moving,
        Stop
    }
    
    public State nowState = State.Stop;   //紀錄現在的單位物理狀態

    private float defaultMass = 1f;  //單位質量
    private float defaultDrag = 0.5f;  //單位阻尼
    private float endVelocity = 0.25f; //下限速度(速度強度低於該數值 就停止

    private new Rigidbody2D rigidbody2D;    //該unit的鋼體
    private UnitData unitData;  //該unit的資料
    private Vector2 inVelocity; //進入碰撞前的速度向量

    private float lastRec = 0f; //上一次紀錄速度的時間
    private float freOfRecordVelocity = 0.5f;   //紀錄速度的頻率
    private float lastVec = 0f; //上一次紀錄的速度強度

    //用於記錄當前unit移動的速度、方向(向量
    private void LateUpdate()
    {
        inVelocity = rigidbody2D.velocity;  //實時更新速度向量 (DEBUG: 如果進入碰撞才獲取速度 就會變成0
        
        //如果是靜止的 速度大於0 則狀態設為移動
        if(nowState == State.Stop && rigidbody2D.velocity.magnitude > 0f)
        {
            nowState = State.Moving;
        } 

        //如果在移動 則開始記錄前0.5秒的速度強度
        if(nowState == State.Moving && Time.time - lastRec > freOfRecordVelocity)
        {
            lastRec = Time.time;
            lastVec = rigidbody2D.velocity.magnitude;
        }

        
        //移動 且 在減速中 那速度小於一定程度 直接停止
        if(nowState == State.Moving && lastVec > rigidbody2D.velocity.magnitude && rigidbody2D.velocity.magnitude < endVelocity)
        {
            rigidbody2D.velocity = Vector2.zero;
            nowState = State.Stop;  //進入靜止狀態
        }
    }

    //初始化 這個Unit的物理性質
    public void InitUnitBehavior()
    {
        //component
        rigidbody2D = GetComponent<Rigidbody2D>();  //獲得鋼體
        unitData = GetComponent<UnitData>();    //獲取這個單位的資料
        
        //賦予單位 質量與阻尼(之後從單位資料獲取
        rigidbody2D.mass = defaultMass;
        rigidbody2D.drag = defaultDrag;

        //剛開始為靜止
        nowState = State.Stop;

        //速度紀錄相關
        lastRec = 0f;
        lastVec = 0f;
    }

    //根據力量 將單位射擊出去
    //只有發射的時候 才會使用
    //反彈 則是直接賦予unit速度
    public void ShootUnit(Vector2 _force)
    {
        rigidbody2D.AddForce(_force);   //施加 力的向量
        nowState = State.Moving;    //進入移動
    }

    //與牆壁的反彈檢測
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidOfColli = collision.gameObject.GetComponent<Rigidbody2D>();    //獲取被撞者的rigid

        if (collision.gameObject.tag == "wall")
        {
            //傳入進入向量、法向量 計算射出向量
            Vector2 reflexAngle = Vector2.Reflect(inVelocity, collision.GetContact(0).normal);

            rigidbody2D.velocity = reflexAngle.normalized * inVelocity.magnitude;   //施加新的速度
        }
        else if ((collision.gameObject.tag == "unit" || collision.gameObject.tag == "enemy")
            && inVelocity.magnitude > rigidOfColli.velocity.magnitude)
        {
            //計算碰撞後新速度的強度
            float newMagnitude = inVelocity.magnitude - rigidOfColli.velocity.magnitude;    //計算新的速度強度

            //傳入進入向量、法向量 計算射出向量
            Vector2 reflexAngle = Vector2.Reflect(inVelocity, collision.GetContact(0).normal);
            rigidbody2D.velocity = reflexAngle.normalized * newMagnitude;   //施加新的速度

            rigidOfColli.velocity = inVelocity.normalized * newMagnitude; //對被撞擊的物體 施加新的速度

            //雙方標籤不同 才扣血 且速度慢的會被攻擊
            if(gameObject.tag != collision.gameObject.tag)
                unitData.SmashSettlement(collision.gameObject);  //碰撞結算
        }
    }
}
