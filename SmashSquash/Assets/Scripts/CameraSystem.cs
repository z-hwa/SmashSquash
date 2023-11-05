using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 攝影機操控腳本 負責跟蹤由battleSystem決定的當前腳色
 * DEBUG:跟蹤抖動問題
 */

public class CameraSystem : MonoBehaviour
{
    public GameObject reporter; //記者
    public float trackingError;   //跟蹤誤差
    public float reporterSmoothing; //記者平滑跟蹤

    public GameObject target;   //記者要跟蹤的目標
    public float smoothing; //平滑數值 range[0, 1]

    //定義兩個位置 限制相機移動的邊界
    //獲取地圖參數 進行設置
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private void LateUpdate()
    {
        GoReporter();   //記者跟蹤
        CameraTracing(reporter);    //相機跟蹤
    }

    //記者跟蹤目標的函數
    private void GoReporter()
    {

        if (reporter != null && (reporter.transform.position - target.transform.position).magnitude > trackingError)
        {
            //舊方法 短距離移動 會造成記者移動不平滑
            //Vector3 dir = (target.transform.position - reporter.transform.position).normalized;  //獲得前進方向
            //reporter.transform.position += dir * reporterSpeed * Time.deltaTime;    //記者移動

            //平滑移動
            reporter.transform.position = Vector3.Lerp(reporter.transform.position, target.transform.position, reporterSmoothing);
        }
    }

    //相機追蹤的函數
    private void CameraTracing(GameObject cameraTarget)
    {
        //如果target存在
        if (cameraTarget != null && (transform.position - reporter.transform.position).magnitude > trackingError)
        {
            Vector3 targetPos = cameraTarget.transform.position;    //獲得reporter的位置

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
