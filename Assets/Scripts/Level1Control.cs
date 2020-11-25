using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level1Control : MonoBehaviour
{
    public GameObject globalPivot;  // 主控旋轉與位置的物件
    public GameObject player;       // 玩家
    public Text scoreTxt;           // 分數物件
    public float speed = 1;         // 前進速度
    public float rotateSpeed = 250; // 旋轉速度

    float rotation = 0;             // 旋轉度數
    int score = 0;                  // 分數
    float startTouchPositionX = 0;  // 開始觸碰的位置
    float startTouchRotation = 0;   // 開始觸碰時的旋轉度數
    Animator playerAnimator;        // 玩家動畫控制器
    
    void Start()
    {
        // 取得玩家動畫控制器，供之後使用
        playerAnimator = player.GetComponent<Animator>();
    }

    
    void Update()
    {
        // 用方向鍵旋轉
        RotateByKeyboard();

        // 用觸控旋轉
        RotateByTouch();

        // 旋轉玩家
        globalPivot.transform.rotation = Quaternion.Euler(0, 0, rotation);

        // 更新分數
        scoreTxt.text = score.ToString();

        // 往前加速
        float tz = globalPivot.transform.position.z + speed * Time.deltaTime * 10;
        globalPivot.transform.position = new Vector3(0, 0, tz);
    }


    // 用方向鍵旋轉
    void RotateByKeyboard()
    {
        if (Input.touchCount == 0)
        {
            // 左方向鍵
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotation += rotateSpeed * Time.deltaTime;
                playerAnimator.SetInteger("pivot", 1);
            }
            // 右方向鍵
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rotation -= rotateSpeed * Time.deltaTime;
                playerAnimator.SetInteger("pivot", -1);
            }
            else
            {
                playerAnimator.SetInteger("pivot", 0);
            }
        }
    }


    // 用觸控旋轉
    void RotateByTouch()
    {
        if (Input.touchCount > 0)
        {
            // 觸控開始，儲存當下位置＆旋轉量
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouchRotation = rotation;
                startTouchPositionX = Input.touches[0].position.x;
            }
            // 觸控中
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                // 計算水平滑動距離
                float dx = startTouchPositionX - Input.touches[0].position.x;

                // 將滑動距離轉為旋轉量
                rotation = startTouchRotation + dx * 0.5f;

                // 變更玩家傾斜動畫
                float deltaX = Input.touches[0].deltaPosition.x;
                if (Mathf.Abs(deltaX) > 3)
                {
                    if (deltaX > 0)
                        playerAnimator.SetInteger("pivot", -1);
                    if (deltaX < 0)
                        playerAnimator.SetInteger("pivot", 1);
                }
            }
            // 觸控結束
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                playerAnimator.SetInteger("pivot", 0);
            }
        }

    }
}
