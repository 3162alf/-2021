/*==============================================================================
    Project_
    [CGate.cs]
    ・ゲート制御
--------------------------------------------------------------------------------
    2021.04.19 @Author Suzuki Hayase
================================================================================
    History
        
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGate : MonoBehaviour {
    private int[] iOrder;                             // 指令
    private int iOrderNum;                            // 指令数
    private int iPassNum;                             // 通過数
    private int iMatchNum;                            // 一致数
    private int iClearNum;                            // クリア数
    private COrderManager csOrderManager;             // OrderManagerスクリプト

    [SerializeField] private GameObject[] gClearLamp; // クリアランプ

    [SerializeField] private GameObject gClear;       // クリアスタンプ

    // Start is called before the first frame update
    void Start() {
        iMatchNum = 0;
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();

        // 指令生成
        iOrderNum = 3;
        iOrder = csOrderManager.CreateOrder(iOrderNum);      
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider col) {
        // 通過オブジェクト判定
        if (col.gameObject.tag == "RotateObject") {
            // 指令と一致
            if(iOrder[iPassNum] == col.gameObject.GetComponent<CRotateObject>().Get_Shape()) {
                iMatchNum++;
                gClearLamp[iPassNum].GetComponent<Renderer>().material.color = Color.green;
            }
            else {
                gClearLamp[iPassNum].GetComponent<Renderer>().material.color = Color.red;
            }
            iPassNum++;

            // 指令数のオブジェクトが通過したらリセット
            if (iPassNum == iOrderNum) {
                if(iMatchNum == iOrderNum) {
                    // クリアスタンプ生成
                    Instantiate(gClear, new Vector3(20, 0, -10 + iClearNum * 5), Quaternion.Euler(0, 0, 0));
                    
                    // 指令生成
                    iOrder = csOrderManager.CreateOrder(iOrderNum);
                    iClearNum++;
                }

                iMatchNum = 0;
                iPassNum = 0;

                gClearLamp[0].GetComponent<Renderer>().material.color = Color.white;
                gClearLamp[1].GetComponent<Renderer>().material.color = Color.white;
                gClearLamp[2].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
