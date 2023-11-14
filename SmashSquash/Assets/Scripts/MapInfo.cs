using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    //地圖資訊
    public int enemyNum;    //敵人數量
    public GameObject[] enemy;  //敵人實體

    //空實體 用於讓map system 操控敵人
    public UnitData[] unitDatas;    //敵人資料
    public UnitBehavior[] unitBehaviors;    //敵人行為腳本
    public A000_Default[] originalData;    //預設敵人 的圖鑑資料(用於存取技能
}
