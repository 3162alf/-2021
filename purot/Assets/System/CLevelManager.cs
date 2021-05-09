/*==============================================================================
    Project_HOGE
    [CLevelManager.cs]
    ・システム制御
--------------------------------------------------------------------------------
    2021.05.09 @Author Suzuki Hayase
================================================================================
    History

        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelManager : CSingletonMonoBehaviour<CLevelManager> {
    [SerializeField] private int iObjectNum;   // オブジェクト数

    private GameObject gTimer;                 // タイマー
    private int iFrame;                        // フレームカウンター

    // Start is called before the first frame update
    void Start() {
        // オブジェクト展開
        OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
        CObjectManager.Instance.AddCreateList(first);
        CObjectManager.Instance.Create(iObjectNum - 1);

        // タイマーゲット
        gTimer = GameObject.Find("PFB_TimerController");
    }

    // Update is called once per frame
    void Update() {
        // 1分ごとに一個追加
        if (iObjectNum <= (int)OBJECT_SHAPE.MAX) {
            iFrame++;
            if (iFrame > 60 * 60) {
                iObjectNum++;
                CObjectManager.Instance.Create(1);
                iFrame = 0;
            }
        }
    }

    // オブジェクト数getter
    public int Get_iObjectNum() {
        return iObjectNum;
    }
}
