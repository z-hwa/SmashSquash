using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 攝影機操控腳本 負責跟蹤由battleSystem決定的當前腳色 */

public class CameraSystem : MonoBehaviour
{
    public GameObject target;   //跟蹤的目標
    public float smoothing; //平滑數值

    //定義兩個位置 限制相機移動的邊界
    //獲取地圖參數 進行設置
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private void LateUpdate()
    {
        CameraTracing();    //相機跟蹤
    }

    //相機追蹤的函數
    private void CameraTracing()
    {
        //如果target存在
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;    //獲得target的位置

            //限制相機移動範圍
            targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

            //平滑移動
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }

    //更換相機追蹤的對象
    public void TargetChange(GameObject newTarget)
    {
        target = newTarget;
    }
}
