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
using UnityEngine.SceneManagement;

public class CLevelManager : CSingletonMonoBehaviour<CLevelManager> {
    [System.Serializable]
    struct LevelParam {
        public int iObjectNum;
        public int iOrderNum;
        public int iGoal;
        public float fGateTime;
        public float fSpeed;
        public int iMinutes;
        public int iSeconds;
        public float fTotalTime;
    }

    private int iLevel = 0;
    [SerializeField] private LevelParam[] Level;

    private GameObject gTimer;                   // タイマー
    private int iFrame;                          // フレームカウンター

    private GameObject gScore;

    [SerializeField] private float fStartTime;
    private bool isEnd = false;

    // Start is called before the first frame update
    void Start() {
        COrderManager.Instance.CreateOrder(Level[iLevel].iOrderNum);
        CClearLampManager.Instance.CreateLamp(Level[iLevel].iOrderNum);

        // オブジェクト展開
        OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
        CObjectManager.Instance.AddCreateList(first);
        CObjectManager.Instance.Create(Level[iLevel].iObjectNum - 1);

        // タイマーゲット
        gTimer = GameObject.Find("PFB_Number");

        gScore = GameObject.Find("PFB_ScoreObj");
        gScore.GetComponent<CScore>().Set_iScoreParam(Level[iLevel].iGoal);

        fStartTime = gTimer.GetComponent<CNumberManager>().Get_fTime();
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
            if (isEnd) {// 終了
                CSceneManager CSM = GameObject.Find("FadeCanvas").GetComponent<CSceneManager>();
                CSM.OnChangeScene_Title();
            }
            else {
                // 現在時間取得
                float endtime = gTimer.GetComponent<CNumberManager>().Get_fTime();

                // ステージクリア時間算出
                float totaltime = endtime - fStartTime;
                int timelimit = Level[iLevel].iMinutes * 60 + Level[iLevel].iSeconds;
                Level[iLevel].fTotalTime = totaltime;
                if(totaltime > timelimit) {// 制限時間内にクリアしなかったら次のステージで終了
                    isEnd = true;
                }
                // スタート時間更新
                fStartTime = endtime;

                iLevel++;
                gScore.GetComponent<CScore>().Set_iScoreParam(Level[iLevel].iGoal);
                CObjectManager.Instance.Create(Level[iLevel].iObjectNum - Level[iLevel - 1].iObjectNum);
            }
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

    public float Get_fGateTime() {
        return Level[iLevel].fGateTime;
    }
}
