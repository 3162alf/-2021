/*==============================================================================
    Project
    [CGate.cs]
    �E�Q�[�g����
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        2021.05.09 @Author Misaki Sasaki 
            125~127�s�ڂ���Ă��������B�����ƁA�X�R�A��Add���鏈���ǉ����Ă܂��B
        2021.5.15 @Author Misaki Sasaki 
            36~,52~,156~,169~�s�ځB�I�[�_�[���s/�������ɐԂ�/�΃p�l�����o��悤�ɂ��܂����B
        2021.05.27
            SE用にいろいろ追加
        2021.0715 hirano
            SE追加
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CGate : MonoBehaviour {
    private int iPassNum;                             // �ʉߐ�
    private int iMatchNum;                            // ��v��
    private int iClearNum;                            // �N���A��
    private COrderManager csOrderManager;             // OrderManager�X�N���v�g

    [SerializeField] private GameObject gClear;       // �N���A�X�^���v

    private float fRadius = 9f;        // ��]���a
    //private float fSpeed = 0.5f;       // ��]���x
    private float fDegree;             // �p�x
    private float fDegreeSub;          // �p�x
    private RotateState State;         // �I�u�W�F�N�g�̉�]���

    private bool isInverse = false;             // 角度

    private GameObject gGateTimerController;            // LampManager�̃I�u�W�F�N�g��i�[
    private CGateTimerController csGateTimerController; // �Q�[�g�^�C�}�[�X�N���v�g
    //private bool bLampReset = false;
    //private int iResetTimer = 0;

    //-- 2021.5.15�ǉ� sasaki
    [SerializeField] private GameObject gPanelObjectRed;    // �p�l���v���n�u�i�ԁj
    [SerializeField] private GameObject gPanelObjectGreen;  // �p�l���v���n�u�i�΁j
    private GameObject gPanelRed;    // ��������p�l���{�́i�ԁj
    private GameObject gPanelGreen;  // ��������p�l���{�́i�΁j

    private GameObject gCanvas; // �p�l���̐e�ɂ������L�����o�X

    private CCreateTrail CCreateTrail;
    [SerializeField] private GameObject TrailManager;

    // SE用
    [SerializeField] private AudioClip aSEClear;    // SE格納するやつ
    [SerializeField] private AudioClip aSEMiss;     // SE格納するやつ
    [SerializeField] private AudioClip aSEVanish;   // 消滅音
    private GameObject gCamera;                     // AudioSource取得用
    AudioSource aAudioSourceGreen;                  // コンポーネント取得用
    AudioSource aAudioSourceRed;                    // コンポーネント取得用

    // Start is called before the first frame update
    void Start() {
        iMatchNum = 0;
        iClearNum = 0;
        gGateTimerController = GameObject.Find("PFB_GateTimerController");
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();
        csGateTimerController = GameObject.Find("PFB_GateTimerController").GetComponent<CGateTimerController>();

        //-- 2021.5.15�ǉ� sasaki
        gCanvas = GameObject.Find("PanelCanvas");
        Quaternion rot = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        gPanelRed = (GameObject)Instantiate(gPanelObjectRed, new Vector3(0.0f, 0.0f, 0.0f), rot);
        gPanelRed.GetComponent<RectTransform>().sizeDelta = new Vector2(1920.0f, 1080.0f);
        //gPanelRed.gameObject.transform.parent = gCanvas.gameObject.transform;
        gPanelRed.gameObject.transform.SetParent(gCanvas.gameObject.transform, true);
        gPanelRed.SetActive(false);

        gPanelGreen = (GameObject)Instantiate(gPanelObjectGreen, new Vector3(0.0f, 0.0f, 0.0f), rot);
        gPanelGreen.GetComponent<RectTransform>().sizeDelta = new Vector2(1920.0f, 1080.0f);
        //gPanelGreen.gameObject.transform.parent = gCanvas.gameObject.transform;
        gPanelGreen.gameObject.transform.SetParent(gCanvas.gameObject.transform, true);
        gPanelGreen.SetActive(false);

        CCreateTrail = TrailManager.GetComponent<CCreateTrail>();

        // カメラ(SE用)を取得
        gCamera = Camera.main.gameObject;

        // コンポーネント取得
        aAudioSourceGreen = gCamera.GetComponent<AudioSource>();
        aAudioSourceRed = gCamera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        // ポーズ画面の時にオブジェクトが回転しないような処理
        if (Mathf.Approximately(Time.timeScale, 0f)) {
            return;
        }

        // 角度のブール処理
        if ((fDegree <= 400 && fDegree >= 390) ||
            (fDegree <= 40 && fDegree >= 30)) {
            isInverse = true;
        }

        if (isInverse) {
            float sp = 1.0f;

            fDegreeSub += sp;

            if (State == RotateState.OUTSIDE) {
                fRadius -= 4 / (180.0f / sp);

                if (fDegreeSub >= 180) {
                    isInverse = false;
                    Set_State(RotateState.INSIDE);
                }
            }
            else if (State == RotateState.INSIDE) {
                fRadius += 4 / (180.0f / sp);

                if (fDegreeSub >= 360) {
                    isInverse = false;
                    Set_State(RotateState.OUTSIDE);
                }
            }
        }

        float s = 180 * 0.05f / Mathf.PI / fRadius;
        // �p�x���Z
        fDegree -= s;

        Vector3 pos;

        // �ʒu�X�V
        pos.x = 7 * Mathf.Sin((fDegree + 180) * Mathf.Deg2Rad)
        + 2 * Mathf.Cos(fDegreeSub * Mathf.Deg2Rad) * Mathf.Sin((fDegree + 180) * Mathf.Deg2Rad);

        pos.y = 2 * Mathf.Sin(fDegreeSub * Mathf.Deg2Rad);

        pos.z = 7 * Mathf.Cos((fDegree + 180) * Mathf.Deg2Rad)
        + 2 * Mathf.Cos(fDegreeSub * Mathf.Deg2Rad) * Mathf.Cos((fDegree + 180) * Mathf.Deg2Rad);

        transform.position = pos;

        Vector3 p = transform.position;
        p += new Vector3(Mathf.Sin((fDegree + 90) * Mathf.Deg2Rad),
                         -0.5f,
                         Mathf.Cos((fDegree + 90) * Mathf.Deg2Rad));
        transform.LookAt(p);

        if (fDegree < 0) {
            fDegree += 720;
        }
        if(fDegree > 720) {
            fDegree -= 720;
        }
        //gGateTimerController.transform.LookAt(this.transform);
    }

    public void Set_fDegree(float d) {
        fDegree = d;
    }

    public void Set_fDegreeSub(float f) {
        fDegreeSub = f;
    }

    public void Set_isInverse(bool b) {
        isInverse = b;
    }

    // ��]�X�e�[�g�ύX�֐�setter
    public void Set_State(RotateState newState) {
        State = newState;
        switch (State) {
            case RotateState.INSIDE:
                //�����]�p�̏���
                fRadius = 5.0f;
                fDegreeSub = 180;
                if (fDegree < 360.0f) {
                    fDegree += 360.0f;
                }
                break;
            case RotateState.OUTSIDE:
                //�O����]�p����
                fRadius = 9.0f;
                fDegreeSub = 0.0f;
                if (fDegree > 360.0f) {
                    fDegree -= 360.0f;
                }
                break;
            default: break;
        }
    }


    void OnTriggerEnter(Collider col) {
        // �ʉ߃I�u�W�F�N�g����
        if (col.gameObject.tag == "RotateObject") {

            GetComponent<CCreateEffect>().CreateEffect();

            OBJECT_SHAPE order = csOrderManager.Get_Order(iPassNum);

            // �w�߂ƈ�v
            if (order == col.gameObject.GetComponent<CRotateObject>().Get_Shape()) {
                iMatchNum++;
                CClearLampManager.Instance.Lighting(iPassNum, Color.green);
                // hirano.20210715 回収効果音を再生
                aAudioSourceGreen.PlayOneShot(aSEVanish);
            }
            else {
                CClearLampManager.Instance.Lighting(iPassNum, Color.red);
                // hirano.20210715 回収効果音を再生
                aAudioSourceGreen.PlayOneShot(aSEVanish);
            }

            // �ʉ߂����I�u�W�F�N�g��폜
            CObjectManager.Instance.Remove(col.gameObject);
            Destroy(col.gameObject);
            //Debug.Log("hit");

            iPassNum++;

            int ordernum = csOrderManager.Get_iOrderNum();

            // �w�ߐ��̃I�u�W�F�N�g���ʉ߂����烊�Z�b�g
            if (iPassNum == ordernum) {
                if (iMatchNum == ordernum) {
                    // �N���A�X�^���v����
                    //Instantiate(gClear, new Vector3(20, 0, -10 + iClearNum * 5),
                    //    Quaternion.Euler(0, 180, 0));

                    //========== 2021/5/09
                    // �X�R�A��L�^����̂ɕK�v�Ȃ̂ő����܂����@by���X��
                    CScore.AddScore();
                    CCreateTrail.Create();
                    //-- 2021.5.15�ǉ� sasaki
                    gPanelGreen.SetActive(true);

                    //Debug.Log("SE!!");
                    aAudioSourceGreen.PlayOneShot(aSEClear);

                    CLevelManager.Instance.UpdateLevel();
                    // �w�ߐ���
                    csOrderManager.CreateOrder(CLevelManager.Instance.Get_iOrderNum());
                    CClearLampManager.Instance.CreateLamp(CLevelManager.Instance.Get_iOrderNum());
                    iClearNum++;
                }
                else {
                    
                    //-- 2021.5.15�ǉ� sasaki
                    gPanelRed.SetActive(true);

                    //Debug.Log("SE!!");
                    aAudioSourceRed.PlayOneShot(aSEMiss);
                }
                // �V�����I�u�W�F�N�g����
                CObjectManager.Instance.Create(ordernum);
                CClearLampManager.Instance.LightingOff();

                csGateTimerController.Reset();
                gGateTimerController.transform.LookAt(new Vector3(0, 0, 10));

                Destroy(this.gameObject);
            }
        }
    }
}
