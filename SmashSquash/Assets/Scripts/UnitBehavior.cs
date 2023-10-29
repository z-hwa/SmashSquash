using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 處理Unit會發生在遊戲中的實際行為，造成的數據變化，會記錄到UnitData中
 * 每個Unit都應該有一個UnitBehavior
 */

public class UnitBehavior : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;    //該unit的鋼體

    //初始化 這個Unit
    public void InitUnit()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();  //獲得鋼體
    }

    public void ShootUnit(Vector2 _force)
    {
        rigidbody2D.AddForce(_force);   //施加 力的向量
    }
}
