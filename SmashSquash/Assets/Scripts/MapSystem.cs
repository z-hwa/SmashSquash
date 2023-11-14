using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public GameObject[] mapPrafabs; //地圖預制件

    //地圖遊戲物件實體
    public GameObject nowMap;   //當前的地圖

    //地圖生成位置
    public Transform mapInitPoint;

    //會從地圖資訊獲取以下資料 並透過init設定
    public int enemyNum;    //敵人數量

    public GameObject[] enemy;  //敵人物件
    public UnitData[] enemyDatas;    //敵人資料
    public UnitBehavior[] enemyBehaviors;    //敵人行為腳本

    public A000_Default[] originalData;    /* 圖鑑中的
                                         * 敵人資料(用於獲取敵人的Agent 和技能方法
                                         * ## 未來會利用裡面的init方法來初始化敵人單位
                                         * 這樣在遊戲中 做關卡 就只需要設定等級 腳色之類的東西
                                         * 細節數值就不用調整(由等級自動加算
                                         */

    //初始化當前地圖(根據地圖index
    public void InitMap(int mapIndex)
    {
        //根據給的id 生成地圖
        nowMap = Instantiate(mapPrafabs[mapIndex], mapInitPoint.position, Quaternion.identity);
        
        //從地圖獲取敵人相關資訊
        enemyNum = nowMap.GetComponent<MapInfo>().enemyNum; //數量
        enemy = nowMap.GetComponent<MapInfo>().enemy;   //敵人實體

        //獲取空實體 之後再從enemy身上抓存在的腳本組件進去  
        enemyBehaviors = nowMap.GetComponent<MapInfo>().unitBehaviors;   //敵人行為腳本
        enemyDatas = nowMap.GetComponent<MapInfo>().unitDatas;   //敵人資料
        originalData = nowMap.GetComponent<MapInfo>().originalData; //敵人原型(技能庫

        //初始化敵人 行為、資料腳本
        for(int i = 0;i < enemyNum;i++)
        {
            //獲得實體腳本
            enemyBehaviors[i] = enemy[i].GetComponent<UnitBehavior>();
            enemyDatas[i] = enemy[i].GetComponent<UnitData>();
            originalData[i] = enemy[i].GetComponent<A000_Default>();

            enemyBehaviors[i].InitUnitBehavior();    //初始化敵人行為腳本
            enemyDatas[i].InitUnitData(originalData[i]);    //初始化敵人資料
        }
    }
}
