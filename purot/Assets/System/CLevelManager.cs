/*==============================================================================
    Project_HOGE
    [CLevelManager.cs]
    �E�V�X�e������
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

    private GameObject gTimer;                   // �^�C�}�[
    private int iFrame;                          // �t���[���J�E���^�[

    private GameObject gScore;

    [SerializeField] private float fStartTime;
    private bool isEnd = false;

    // Start is called before the first frame update
    void Start() {
        COrderManager.Instance.CreateOrder(Level[iLevel].iOrderNum);
        CClearLampManager.Instance.CreateLamp(Level[iLevel].iOrderNum);

        // �I�u�W�F�N�g�W�J
        OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
        CObjectManager.Instance.AddCreateList(first);
        CObjectManager.Instance.Create(Level[iLevel].iObjectNum - 1);

        // �^�C�}�[�Q�b�g
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
            if (isEnd) {// �I��
                CSceneManager CSM = GameObject.Find("FadeCanvas").GetComponent<CSceneManager>();
                CSM.OnChangeScene_Title();
            }
            else {
                // ���ݎ��Ԏ擾
                float endtime = gTimer.GetComponent<CNumberManager>().Get_fTime();

                // �X�e�[�W�N���A���ԎZ�o
                float totaltime = endtime - fStartTime;
                int timelimit = Level[iLevel].iMinutes * 60 + Level[iLevel].iSeconds;
                Level[iLevel].fTotalTime = totaltime;
                if(totaltime > timelimit) {// �������ԓ��ɃN���A���Ȃ������玟�̃X�e�[�W�ŏI��
                    isEnd = true;
                }
                // �X�^�[�g���ԍX�V
                fStartTime = endtime;

                iLevel++;
                gScore.GetComponent<CScore>().Set_iScoreParam(Level[iLevel].iGoal);
                CObjectManager.Instance.Create(Level[iLevel].iObjectNum - Level[iLevel - 1].iObjectNum);
            }
        }
    }

    // �I�u�W�F�N�g��getter
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
