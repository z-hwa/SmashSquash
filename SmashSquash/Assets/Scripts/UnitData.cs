using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 每個戰鬥中的單位都應該有一個 用於處理那個單位的資料管理
 * 因為是透過prefab生成 故應該得到初始化
 */

public enum LifeState
{
    died,
    life
}

public class UnitData : MonoBehaviour
{
    //單位資料 從單位模板中 解壓出來使用
    public string unitName; //名字
    public int spiritGrade; //靈力等級
    public SpiritControlStandard spiritControl;    //靈力控制階級
    public LifeState isLife = LifeState.died;    //預設為死亡狀態

    public int hp;  //生命
    public int atk; //攻擊
    public int def; //防禦

    //當碰撞發生時，呼叫者與被動者的資料結算
    public void SmashSettlement(GameObject passive)
    {
        //獲取被碰撞者的資料
        UnitData paData = passive.GetComponent<UnitData>();

        //靈力控制力 影響倍率
        float ac_SpiritRate = Mathf.Pow(1.3f, (int)spiritControl);
        float pa_SpiritRate = Mathf.Pow(1.3f, (int)paData.spiritControl);

        //減傷率 = ((攻擊者的攻擊力+靈力影響) / (被攻擊者的防禦力+靈力影響))
        float damageRate = (atk + spiritGrade * ac_SpiritRate) 
            / (paData.atk + paData.spiritGrade * pa_SpiritRate);

        if (damageRate > 1f) damageRate = 1f; //計算出剩下的傷害率

        /*
         套用最終單位技能 or 其他最終效果
         */

        float damage = atk * damageRate;   //得到最終傷害

        paData.HpChange(damage, -1);    //扣除被碰撞者血量
        paData.DiedJudge(passive);  //檢測被碰撞者是否死亡
    }

    //呼叫者 unitData的生命改變
    //value 為改變純量
    //state 為改變方向 扣血或回血
    private void HpChange(float value, int state)
    {
        Debug.Log(value);
        hp = hp + (int)value * state;   //計算傷害
    }

    //檢測呼叫單位是否死亡
    //unit為呼叫單位的遊戲物件 用以deactive
    private void DiedJudge(GameObject unit)
    {
        if(hp <= 0)
        {
            isLife = LifeState.died;
            unit.SetActive(false);  //關閉單位
        }
    }

    //初始化 呼叫者的單位資料
    public void InitUnitData(A000_Default unitData)
    {
        unitName = unitData.unitName;
        spiritGrade = unitData.spiritGrade;
        spiritControl = unitData.spiritControl;
        isLife = LifeState.life;

        hp = unitData.hp;
        atk = unitData.atk;
        def = unitData.def;
    }
}
