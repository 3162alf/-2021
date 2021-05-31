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
    [System.Serializable]
    struct LevelParam {
        public int iObjectNum;
        public int iOrderNum;
        public int iGoal;
        public float fGateTime;
        public float fSpeed;
    }

    private int iLevel = 0;
    [SerializeField] private LevelParam[] Level;

    private GameObject gTimer;                   // タイマー
    private int iFrame;                          // フレームカウンター

    private GameObject gScore;

    // Start is called before the first frame update
    void Start() {
        COrderManager.Instance.CreateOrder(Level[iLevel].iOrderNum);

        // オブジェクト展開
        OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
        CObjectManager.Instance.AddCreateList(first);
        CObjectManager.Instance.Create(Level[iLevel].iObjectNum - 1);

        // タイマーゲット
        gTimer = GameObject.Find("PFB_TimerController");

        gScore = GameObject.Find("PFB_ScoreObj");
        gScore.GetComponent<CScore>().Set_iScoreParam(Level[iLevel].iGoal);
    }

    // Update is called once per frame
    void Update() {
        //if (CScore.GetScore() >= Level[iLevel].iGoal) {
        //    iLevel++;
        //    gScore.GetComponent<CScore>().Set_iScoreParam(Level[iLevel].iGoal);
        //    CObjectManager.Instance.Create(Level[iLevel].iObjectNum - Level[iLevel - 1].iObjectNum);
        //}
    }

    public void UpdateLevel() {
        if (CScore.GetScore() >= Level[iLevel].iGoal) {
            iLevel++;
            gScore.GetComponent<CScore>().Set_iScoreParam(Level[iLevel].iGoal);
            CObjectManager.Instance.Create(Level[iLevel].iObjectNum - Level[iLevel - 1].iObjectNum);
        }
    }

    // オブジェクト数getter
    public int Get_iObjectNum() {
        return Level[iLevel].iObjectNum;
    }

    public int Get_iGoal() {
        return Level[iLevel].iGoal;
    }

    public int Get_iOrderNum() {
        return Level[iLevel].iOrderNum;
    }
}
