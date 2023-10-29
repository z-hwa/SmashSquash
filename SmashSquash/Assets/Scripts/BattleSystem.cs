using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public GameObject unitPrefab;   //單位預制體
    public Transform unitInitPoint;     //單位生成位置
    public GameObject[] unit = new GameObject[4];   //單位的遊戲物件 生成實體後 放進去
    public int unitNumber;  //單位數量

    //System
    public PlayerControlSystem playerControlSystem;
    public CameraSystem cameraSystem;

    // Start is called before the first frame update
    void Start()
    {
        InitBattleSystem();

        //LoadBattle

        //PlayerTurn
    }

    private void InitBattleSystem()
    {
        for(int i =0;i<unitNumber;i++)
        {
            unit[i] = Instantiate(unitPrefab, unitInitPoint.position, Quaternion.identity);//生成遊戲單位實體 放進Unit
            unit[i].GetComponent<UnitBehavior>().InitUnit();//遊戲單位實體初始化

            //初始化cameraSystem, PlayerSystem
            playerControlSystem.ChangeControlUnit(unit[i]);
            cameraSystem.TargetChange(unit[i]);
        }
    }
}
