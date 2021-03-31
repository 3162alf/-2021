/*==============================================================================
    Priject_
    [CLampController.cs]
    ・PFB_LampManagerオブジェクトとLampオブジェクトとの2点間の角度を求める
    ・CLampManager からスティックの傾きに対応したラジアン角を取得する
    ・2点間の角度と取得したラジアン角を比較して、対応したLampの色を変える
--------------------------------------------------------------------------------
    2021.03.24 @Author Hirano Tomoki
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLampController : MonoBehaviour {

    private GameObject gLampManager;    // LampManagerのオブジェクトを格納
    private Vector3 vDt;

    private int iControllerAngle = 0;
    private float fAngle = 0f;
    private float fRad   = 0f;
    private float fDeg   = 0f;

    void Start() {
        // "PFB_LampManager"を取得してgLampManagerに格納
        gLampManager = GameObject.Find("PFB_LampManager");

        // "Lamp"オブジェクトの初期色をGrayに設定
        GetComponent<Renderer>().material.color = Color.gray;
        // fAngle変数にPFB_LampManagerとこのオブジェクトの2点間の角度を格納
        fAngle = Get_fAngle(gLampManager.transform.position, this.transform.position);
    }

    void Update() {
        // コントローラーの傾きから算出したラジアン角を格納
        iControllerAngle = gLampManager.GetComponent<CLampManager>().Get_iRad();

        // 指定範囲内にある"Lamp"オブジェクトを黄色に、それ以外を灰色にする
        if (iControllerAngle - 10 < fAngle && iControllerAngle + 10 > fAngle) {
            //Debug.Log(transform.name + "hit");

            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else {
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    // 2点間の角度を算出する関数
    float Get_fAngle(Vector3 vStart, Vector3 vTarget) {
        vDt = vTarget - vStart;
        fRad = Mathf.Atan2(vDt.x, vDt.z);
        fDeg = fRad * -Mathf.Rad2Deg;

        if (fDeg < 0) {
            fDeg += 360;
        }

        return fDeg;
    }
}
