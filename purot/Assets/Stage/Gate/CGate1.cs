/*==============================================================================
    Project_
    [CGate1.cs]
    ・ゲート制御
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGate1 : MonoBehaviour {
    private int iPassNum;                             // 通過数
    private int iMatchNum;                            // 一致数
    private int iClearNum;                            // クリア数
    private COrderManager1 csOrderManager;            // OrderManagerスクリプト

    [SerializeField] private GameObject gClear;       // クリアスタンプ

    private float fRadius = 9f;        // 回転半径
    private float fSpeed = 2.0f;       // 回転速度
    private float fDegree;             // 角度
    private RotateState State;         // オブジェクトの回転状態

    private CGateTimerController csGateTimerController;

    // Start is called before the first frame update
    void Start() {
        iMatchNum = 0;
        iClearNum = 0;
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager1>();
        csGateTimerController = GameObject.Find("GateTimerController").GetComponent<CGateTimerController>();
    }

    // Update is called once per frame
    void Update() {
        // 角度加算
        fDegree -= fSpeed;

        Vector3 pos;

        // 位置更新
        pos.x = fRadius * Mathf.Sin(fDegree * Mathf.Deg2Rad);
        pos.y = 0.0f;
        pos.z = fRadius * Mathf.Cos(fDegree * Mathf.Deg2Rad);
        transform.position = pos;

        // 一周したら内側、外側を入れ替える処理
        // 角度が360を超えたら裏表を変えて、角度変数を0にする
        if (fDegree <= 0 && State == RotateState.OUTSIDE) {
            Set_State(RotateState.INSIDE);
            fDegree = 360;
        }
        else if (fDegree <= 0 && State == RotateState.INSIDE) {
            Set_State(RotateState.OUTSIDE);
            fDegree = 360;
        }
    }

    public void Set_fDegree(float d) {
        fDegree = d;
    }

    // 回転ステート変更関数setter
    public void Set_State(RotateState newState) {
        State = newState;
        switch (State) {
            case RotateState.INSIDE:
                //内側回転用の処理
                fRadius = 3.0f;

                break;
            case RotateState.OUTSIDE:
                //外側回転用処理
                fRadius = 9.0f;

                break;
            default: break;
        }
    }


    void OnTriggerEnter(Collider col) {
        // 通過オブジェクト判定
        if (col.gameObject.tag == "RotateObject") {
            GameObject lamp = csOrderManager.Get_gClearLamp(iPassNum);
            OBJECT_SHAPE order = csOrderManager.Get_Order(iPassNum);

            // 指令と一致
            if (order == col.gameObject.GetComponent<CRotateObject>().Get_Shape()) {
                iMatchNum++;
                lamp.GetComponent<Renderer>().material.color = Color.green;
            }
            else {
                lamp.GetComponent<Renderer>().material.color = Color.red;
            }
            iPassNum++;

            int ordernum = csOrderManager.Get_iOrderNum();

            // 指令数のオブジェクトが通過したらリセット
            if (iPassNum == ordernum) {
                if (iMatchNum == ordernum) {
                    // クリアスタンプ生成
                    Instantiate(gClear, new Vector3(20, 0, -10 + iClearNum * 5),
                        Quaternion.Euler(0, 180, 0));

                    // 指令生成
                    csOrderManager.CreateOrder(3);
                    iClearNum++;
                }
                else {
                    for (int i = 0; i < ordernum; i++) {
                        GameObject l = csOrderManager.Get_gClearLamp(i);
                        l.GetComponent<Renderer>().material.color = Color.white;
                    }
                }

                csGateTimerController.Reset();
                Destroy(this.gameObject);
            }
        }
    }
}
