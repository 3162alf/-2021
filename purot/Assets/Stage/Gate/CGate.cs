/*==============================================================================
    Project
    [CGate.cs]
    �E�Q�[�g����
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        2021.05.09 @Author Sasaki Misaki
            125~127�s�ڂ����Ă��������B�����ƁA�X�R�A��Add���鏈���ǉ����Ă܂��B
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGate : MonoBehaviour {
    private int iPassNum;                             // �ʉߐ�
    private int iMatchNum;                            // ��v��
    private int iClearNum;                            // �N���A��
    private COrderManager csOrderManager;             // OrderManager�X�N���v�g

    [SerializeField] private GameObject gClear;       // �N���A�X�^���v

    private float fRadius = 9f;        // ��]���a
    private float fSpeed = 0.5f;       // ��]���x
    private float fDegree;             // �p�x
    private RotateState State;         // �I�u�W�F�N�g�̉�]���

    private CGateTimerController csGateTimerController; // �Q�[�g�^�C�}�[�X�N���v�g

    // Start is called before the first frame update
    void Start() {
        iMatchNum = 0;
        iClearNum = 0;
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();
        csGateTimerController = GameObject.Find("PFB_GateTimerController").GetComponent<CGateTimerController>();
    }

    // Update is called once per frame
    void Update() {
        // �p�x���Z
        fDegree -= fSpeed;

        Vector3 pos;

        // �ʒu�X�V
        pos.x = fRadius * Mathf.Sin((fDegree + 180) * Mathf.Deg2Rad);
        pos.y = 0.0f;
        pos.z = fRadius * Mathf.Cos((fDegree + 180) * Mathf.Deg2Rad);
        transform.position = pos;

        // �������������A�O�������ւ��鏈��
        // �p�x��360�𒴂����痠�\��ς��āA�p�x�ϐ���0�ɂ���
        if (fDegree <= 0 && State == RotateState.OUTSIDE) {
            fDegree = 720.0f;
            Set_State(RotateState.INSIDE);
        }
        else if (fDegree <= 360 && State == RotateState.INSIDE) {
            Set_State(RotateState.OUTSIDE);
        }
    }

    public void Set_fDegree(float d) {
        fDegree = d;
    }

    // ��]�X�e�[�g�ύX�֐�setter
    public void Set_State(RotateState newState) {
        State = newState;
        switch (State) {
            case RotateState.INSIDE:
                //������]�p�̏���
                fRadius = 3.0f;
                if (fDegree < 360.0f) {
                    fDegree += 360.0f;
                }
                break;
            case RotateState.OUTSIDE:
                //�O����]�p����
                fRadius = 9.0f;
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
            GameObject lamp = csOrderManager.Get_gClearLamp(iPassNum);
            OBJECT_SHAPE order = csOrderManager.Get_Order(iPassNum);

            // �w�߂ƈ�v
            if (order == col.gameObject.GetComponent<CRotateObject>().Get_Shape()) {
                iMatchNum++;
                lamp.GetComponent<Renderer>().material.color = Color.green;
            }
            else {
                lamp.GetComponent<Renderer>().material.color = Color.red;
            }

            // �ʉ߂����I�u�W�F�N�g���폜
            CObjectManager.Instance.Remove(col.gameObject);
            Destroy(col.gameObject);

            // �V�����I�u�W�F�N�g����
            CObjectManager.Instance.Create(1);
            iPassNum++;

            int ordernum = csOrderManager.Get_iOrderNum();

            // �w�ߐ��̃I�u�W�F�N�g���ʉ߂����烊�Z�b�g
            if (iPassNum == ordernum) {
                if (iMatchNum == ordernum)
                {
                    // �N���A�X�^���v����
                    //Instantiate(gClear, new Vector3(20, 0, -10 + iClearNum * 5),
                    //    Quaternion.Euler(0, 180, 0));

                    //========== 2021/5/09
                    // �X�R�A���L�^����̂ɕK�v�Ȃ̂ő����܂����@by���X��
                    CScore.AddScore();

                    // �w�ߐ���
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
