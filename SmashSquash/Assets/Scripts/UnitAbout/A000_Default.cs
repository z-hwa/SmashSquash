using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 所有unit的模板 */
public class A000_Default: MonoBehaviour
{
    /* 使用資料區(public
     * 整個遊戲過程中會實際改動的區域
     * 腳色的成長、升級等等
     */

    public string unitName; //名字
    public int spiritGrade; //靈力等級
    public SpiritControlStandard spiritControl; //靈力控制階級

    public int hp;  //生命
    public int atk; //攻擊
    public int def; //防禦

    /* 函數區域 繼承的unit 會有不同的技能 寫在這區 */

    //instance後 需要將初始資料 紀錄進使用資料區
    public A000_Default()
    {
        this.unitName = "unitName";
        this.spiritGrade = 1;
        this.spiritControl = SpiritControlStandard.A;

        this.hp = 1;
        this.atk = 1;
        this.def = 1;
    }

    //預設的敵人AI智能
    public virtual void EnemyAgent(GameObject enemy, GameObject[] playerUnit0, int unitNum)
    {
        //射擊力度
        float shootMagnitude = Random.Range(200f, 700f);

        GameObject target = playerUnit0[0];  //攻擊目標

        //找到玩家存活的第一個單位 從0~unitNum
        for(int i=0;i<unitNum;i++)
        {
            if(playerUnit0[i].GetComponent<UnitData>().hp != 0)
            {
                target = playerUnit0[i];
                break;
            }
        }

        //獲得應該攻擊的方向
        //指向傳入的player unit
        Vector2 shootDir = (target.transform.position - enemy.transform.position).normalized;

        //彈射攻擊
        enemy.GetComponent<UnitBehavior>().ShootUnit(shootDir * shootMagnitude);
    }
}
