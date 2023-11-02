using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 處理Unit會發生在遊戲中的實際行為，造成的數據變化，會記錄到UnitData中
 * 每個Unit都應該有一個UnitBehavior
 */

public class UnitBehavior : MonoBehaviour
{
    private float defaultMass = 1f;  //單位質量
    private float defaultDrag = 0.5f;  //單位阻尼
    private float endVelocity = 0.5f; //下限速度(速度強度低於該數值 就停止

    private new Rigidbody2D rigidbody2D;    //該unit的鋼體
    private Vector2 lastPoint;  //上一個點
    private Vector2 inVelocity; //進入碰撞前的速度向量

    //用於記錄當前unit移動的速度、方向(向量
    private void LateUpdate()
    {
        inVelocity = rigidbody2D.velocity;  //實時更新速度向量 (DEBUG: 如果進入碰撞才獲取速度 就會變成0
        
        //速度小於一定程度 直接停止
        if(rigidbody2D.velocity.magnitude < endVelocity)
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    //初始化 這個Unit
    public void InitUnit()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();  //獲得鋼體
        
        //賦予單位 質量與阻尼(之後從單位資料獲取
        rigidbody2D.mass = defaultMass;
        rigidbody2D.drag = defaultDrag;
    }

    public void ShootUnit(Vector2 _force)
    {
        rigidbody2D.AddForce(_force);   //施加 力的向量
    }

    //與牆壁的反彈檢測
    //bug 反彈力度不對
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");

        if(collision.gameObject.tag == "wall")
        {
            //Debug.Log("hit wall");

            //傳入進入向量、法向量 計算射出向量
            Vector2 reflexAngle = Vector2.Reflect(inVelocity, collision.GetContact(0).normal);

            rigidbody2D.velocity = reflexAngle.normalized * inVelocity.magnitude;   //施加新的速度
        }
    }
}
