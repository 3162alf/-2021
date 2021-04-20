/*==============================================================================
    Priject_
    [CLampManager.cs]
    ・スティックの傾きをラジアン角に変換する
    ・Inspecter上で設定した半径をもとに指定個数分の"Lamp"オブジェクトの生成
    ・変換後のラジアン角をreturnする
--------------------------------------------------------------------------------
    2021.03.24 @Author Hirano Tomoki
================================================================================
    History
        210404 Hirano
            スティック入力の強さによる検知範囲の調整処理
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLampManager : MonoBehaviour {

    // 角度取得、変換処理用変数------------------------------------------------

    [SerializeField, TooltipAttribute("水平方向用のスティックの登録名")]
    private string stHorStickName = "Horizontal";

    [SerializeField, TooltipAttribute("垂直方向用のスティックの登録名")]
    private string stVerStickName = "Vertical";

    [SerializeField] private float fDeadZone = 0.01f;

    private float fHorizontal;                      // 水平方向のスティックの傾き格納変数
    private float fVertivcal;                       // 垂直方向のスティックの傾き格納変数
    private float fRad;                             // スティックの傾きから算出したラジアン角格納用変数
    private float fOldRad;                          // 1フレーム前のラジアン角記憶用変数

    //-------------------------------------------------------------------------

    // ランプ自動生成用変数----------------------------------------------------

    [SerializeField] private GameObject gLamp;              // 生成するオブジェクト
    [SerializeField] private float fCreateRad = 5f;         // "Lamp"オブジェクト生成に使用する半径

    private int iLampNum = 16;      // "Lamp"オブジェクトを生成する個数
    private float fRepeat = 1f;     // "Lamp"オブジェクトを生成する周期

    private float fOneCycle = 0f;   // 
    private float fPoint = 0f;      // 
    private float fRepeatPoint = 0f;// 
    private float fPosX = 0f;       // 
    private float fPosZ = 0f;       // 

    private Vector3 vCreatePos;     // 

    //-------------------------------------------------------------------------

    void Start() {
        // 初期のラジアン角を90°に設定
        fRad = 0;
        fOldRad = fRad;

        // ランプを円状に自動生成
        CreateLamp();
    }

    void Update() {

        // 水平方向と垂直方向のスティックの傾きを取得
        fHorizontal = Input.GetAxis(stHorStickName);
        fVertivcal = Input.GetAxis(stVerStickName);

        // スティックの検知範囲を調整
        if (fHorizontal <  fDeadZone &&
            fHorizontal > -fDeadZone &&
            fVertivcal  <  fDeadZone &&
            fVertivcal  > -fDeadZone)  {
            fRad = fOldRad;
            return;
        }

        // スティックの傾きをラジアン角に変換
        fRad = Mathf.Atan2(fVertivcal, fHorizontal) * Mathf.Rad2Deg - 90;

        // ラジアン角がマイナスにならないように補正
        if (fRad < 0) {
            fRad += 360;
        }

        fOldRad = fRad;
        //Debug.Log((int)fRad);
    }

    // ランプ自動生成関数
    private void CreateLamp() {

        fOneCycle = 2.0f * Mathf.PI;

        // 指定個数分のオブジェクトを生成するまで繰り返す
        for (var i = 0; i < iLampNum; ++i) {

            fPoint = ((float)i / iLampNum) * fOneCycle;
            fRepeatPoint = fPoint * fRepeat;

            // オブジェクトを生成する位置を計算する
            fPosX = Mathf.Cos(fRepeatPoint) * fCreateRad;
            fPosZ = Mathf.Sin(fRepeatPoint) * fCreateRad;

            vCreatePos = new Vector3(fPosX, 0f, fPosZ);

            // Lampオブジェクトを生成
            gLamp = Instantiate(
                gLamp,
                vCreatePos,
                Quaternion.identity,
                transform
            ) as GameObject;

            // オブジェクト名を "Lamp + 番号"に変更
            gLamp.name = "Lamp" + (i + 1);
        }
    }

    // 変換後の角度のゲッター
    public int Get_iRad() {
        return (int)fRad;
    }
}
