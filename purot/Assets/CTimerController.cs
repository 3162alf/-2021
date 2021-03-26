/*==============================================================================
    PROJECT ???
    [CTimerController.cs]
    ・お題をクリアするまでの制限時間
--------------------------------------------------------------------------------
    2021.03.25 @Author Kaname Ota
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTimerController : MonoBehaviour{
    public Text tTimer;          // テキストオブジェクト(Timer)を格納する

    [SerializeField]
    private float fTotalTime;   // 制限時間
    private int iSecond = 0;    // テキストに表示する秒数

    void Start(){
        // 制限時間の最大値。とりあえず二桁
        if(fTotalTime > 100.0f){
            fTotalTime = 99.9f;
        }
    }

    void Update(){
        // フレームごとの秒数を減算 
        fTotalTime -= Time.deltaTime;

        // 制限時間をintでキャストして秒数を算出
        iSecond = (int)fTotalTime;

        // 秒数を文字列にして表示
        tTimer.text = iSecond.ToString("00");

        // 制限時間がなくなったら表示と制限時間を0に固定
        if(iSecond <= 0){
            tTimer.text = "00";
            fTotalTime = 0.0f;
        }

    }
}
