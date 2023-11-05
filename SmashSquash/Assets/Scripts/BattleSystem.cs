using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    //測試假資料(應該從背包中獲取
    public A000_Default[] unitLineUp;  //出戰unit對列
    public int mapIndex = 0;

    //戰鬥相關設定
    public GameObject unitPrefab;   //單位預制體
    public Transform unitInitPoint;     //單位生成位置 (從地圖獲取

    public GameObject[] unit = new GameObject[4];   //單位的遊戲物件 生成實體後 放進去 用於管理單位
    public GameObject enemy;    //當前操控的敵人單位
    public int unitNumber;  //單位數量 (應該從背包中獲取

    public int nowTurnUnit; //這個回合 可以操控的單位
    public int nowTurnEnemy;    //這個回合操控的敵方單位

    //System
    public PlayerControlSystem playerControlSystem;
    public CameraSystem cameraSystem;
    public MapSystem mapSystem;

    // Start is called before the first frame update
    void Start()
    {
        //GetBattleInf from package or ...
        //

        InitBattle();

        //LoadBattle

        StartCoroutine(PlayerTurn());   //進入玩家回合
    }

    //玩家回合
    IEnumerator PlayerTurn()
    {
        //攝影機給到單位
        //設置這回合操控的單位
        cameraSystem.TargetChange(unit[nowTurnUnit]);
        playerControlSystem.ChangeControlUnit(unit[nowTurnUnit]);

        //啟動玩家控制系統的 操控可用
        playerControlSystem.manipulateAvailable = true;

        //等待玩家操控
        yield return new WaitUntil(() => playerControlSystem.manipulateAvailable == false);

        yield return new WaitUntil(() => IsAllUnitStop());  //等待單位停下來

        //判斷遊戲狀態
        if (IsPlayerWin() == true)
        {
            //勝利結算畫面
        }
        else if (IsPlayerLose() == true)
        {
            //失敗結算畫面
        }
        else
        {
            nowTurnUnit = (nowTurnUnit + 1) % unitNumber;   //下回合 輪到下一個單位
            StartCoroutine(EnemyTurn());    //敵人回合
        }
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("enter enemy turn");

        enemy = mapSystem.enemy[nowTurnEnemy];  //獲得當前操控的敵人單位

        Debug.Log("getted enemy object");

        //攝影機給到敵人 (DEBUG:以不同的攝影機移動方式
        cameraSystem.TargetChange(enemy);

        yield return new WaitForSeconds(2f);  //等待攝影機移動

        //執行對應敵人agent的互動(攻擊
        mapSystem.enemyData[nowTurnEnemy].EnemyAgent(enemy, unit, unitNumber);

        yield return new WaitUntil(() => IsAllUnitStop());  //等待單位停下來

        //判斷遊戲狀態
        if (IsPlayerWin() == true)
        {
            //勝利結算畫面
        }
        else if (IsPlayerLose() == true)
        {
            //失敗結算畫面
        }
        else
        {
            nowTurnEnemy = (nowTurnEnemy + 1) % mapSystem.enemyNum;   //下回合 輪到下一個敵人單位
            StartCoroutine(PlayerTurn()); //玩家回合
        }
    }

    //判斷場上狀況是否都停止了
    private bool IsAllUnitStop()
    {
        bool isStop = true;

        for(int i=0;i<unitNumber;i++)
        {
            if (unit[i].GetComponent<UnitBehavior>().nowState == UnitBehavior.State.Moving) isStop = false;
        }

        for (int i = 0; i < mapSystem.enemyNum; i++)
        {
            if (mapSystem.enemy[i].GetComponent<UnitBehavior>().nowState == UnitBehavior.State.Moving) isStop = false;
        }

        return isStop;
    }

    //判斷玩家是否已經勝利這一局
    private bool IsPlayerWin()
    {
        //玩家的勝利條件是否成立

        return false;
    }

    //判斷玩家是否已經輸掉這一局
    private bool IsPlayerLose()
    {
        //玩家的失敗條件是否成立

        return false;
    }

    //初始化戰鬥
    private void InitBattle()
    {
        mapSystem.InitMap(mapIndex);

        nowTurnUnit = 0;    //第一回合可以使用的單位index 0
        nowTurnEnemy = 0;   //敵人第一回合使用的單位

        for(int i=0;i<unitNumber;i++)
        {
            unit[i] = Instantiate(unitPrefab, unitInitPoint.position, Quaternion.identity); //生成遊戲單位實體 放進Unit
            unit[i].GetComponent<UnitBehavior>().InitUnitBehavior();    //遊戲單位 物理性質、操控 初始化

            unit[i].GetComponent<UnitData>().InitUnitData(unitLineUp[i]);   //初始化遊戲中的數據資料
        }

        //初始化系統
        playerControlSystem.InitPlayerControlSystem();

        //初始化cameraSystem, PlayerSystem的鎖定單位為 index0
        playerControlSystem.ChangeControlUnit(unit[nowTurnUnit]);
        cameraSystem.TargetChange(unit[nowTurnUnit]);
    }
}
