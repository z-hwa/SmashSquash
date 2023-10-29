﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* 戰鬥過程中只會有一個，處理戰鬥中，玩家操控的事件 */

public class PlayerControlSystem : MonoBehaviour
{
    private float beganTime = 0f;   //點擊開始的時間
    private float interval = 0f;    //間隔的時間
    private float swipeMagnitude = 120f;    //滑動的標準

    private Vector2 startPos = Vector2.zero;    //點擊初始點
    private Vector2 endPos = Vector2.zero;  //點擊結束點
    private Vector2 direction = Vector2.zero;   //紀錄滑動的方向

    public UnitBehavior unit;   //測試Unit

    private void Start()
    {
        unit.InitUnit();    
    }

    void Update()
    {
        TouchDetect();
    }

    //偵測對於遊戲的點擊
    private void TouchDetect()
    {
        //發生了點擊
        if(Input.touchCount == 1)
        {

            Touch touch = Input.GetTouch(0);    //獲得第一個接觸的手指 點擊的事件

            bool isTouchUIElement = EventSystem.current.IsPointerOverGameObject(touch.fingerId);    //檢查是否典籍到UI

            //如果沒點到UI
            if(!isTouchUIElement)
            {
                //根據點擊的狀況 進入不同的操作偵測
                switch(touch.phase)
                {
                    //點擊的開始
                    case TouchPhase.Began:
                        startPos = touch.position;  //紀錄點擊開始的位置
                        beganTime = Time.realtimeSinceStartup;  //不受timescale影響的時間

                        //QuickDoubleTab();   //判斷是否雙擊
                        break;

                    //移動
                    case TouchPhase.Moved:
                        direction = touch.position - startPos;  //獲得往哪個方向滑動
                        interval = Time.realtimeSinceStartup - beganTime;   //獲得間隔時間

                        //Hold(); 偵測是否為長按
                        break;

                    //停止
                    case TouchPhase.Stationary:
                        interval = Time.realtimeSinceStartup - beganTime;   //獲得間隔時間

                        //Hold(); 偵測是否為長按
                        break;

                    //離開
                    case TouchPhase.Ended:
                        interval = Time.realtimeSinceStartup - beganTime;   //獲得間隔時間
                        endPos = direction + startPos;  //計算結束點

                        Swipe(direction);    //判斷是否滑動
                        break;
                }
            }
        }
    }

    private void Swipe(Vector2 _direction)
    {
        //判斷滑動距離
        if(_direction.magnitude > swipeMagnitude)
        {
            //滑動
            Debug.Log("Swipe");

            unit.ShootUnit(-_direction);
        }
    }
}