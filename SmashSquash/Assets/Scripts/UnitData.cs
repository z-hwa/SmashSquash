using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 每個戰鬥中的單位都應該有一個 用於處理那個單位的資料管理
 * 因為是透過prefab生成 故應該得到初始化
 */

public class UnitData : MonoBehaviour
{
    //單位資料 從單位模板中 解壓出來使用
    public string unitName; //名字
    public int spiritGrade; //靈力等級
    public string spiritControl;    //靈力控制階級

    public int hp;  //生命
    public int atk; //攻擊
    public int def; //防禦

    public void InitUnitData(A000_Default unitData)
    {
        unitName = unitData.unitName;
        spiritGrade = unitData.spiritGrade;
        spiritControl = unitData.spiritControl;

        hp = unitData.hp;
        atk = unitData.atk;
        def = unitData.def;
    }
}
