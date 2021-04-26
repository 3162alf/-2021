/*==============================================================================
    Project_
    [CGate1.cs]
    �E�Q�[�g����
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGate1 : MonoBehaviour {
    private int iPassNum;                             // �ʉߐ�
    private int iMatchNum;                            // ��v��
    private int iClearNum;                            // �N���A��
    private COrderManager1 csOrderManager;            // OrderManager�X�N���v�g

    [SerializeField] private GameObject gClear;       // �N���A�X�^���v

    private float fRadius = 9f;        // ��]���a
    private float fSpeed = 2.0f;       // ��]���x
    private float fDegree;             // �p�x
    private RotateState State;         // �I�u�W�F�N�g�̉�]���

    private CGateTimerController csGateTimerController;

    // Start is called before the first frame update
    void Start() {
        iMatchNum = 0;
        iClearNum = 0;
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager1>();
        csGateTimerController = GameObject.Find("GateTimerController").GetComponent<CGateTimerController>();
    }

    // Update is called once per frame
    void Update() {
        // �p�x���Z
        fDegree -= fSpeed;

        Vector3 pos;

        // �ʒu�X�V
        pos.x = fRadius * Mathf.Sin(fDegree * Mathf.Deg2Rad);
        pos.y = 0.0f;
        pos.z = fRadius * Mathf.Cos(fDegree * Mathf.Deg2Rad);
        transform.position = pos;

        // �������������A�O�������ւ��鏈��
        // �p�x��360�𒴂����痠�\��ς��āA�p�x�ϐ���0�ɂ���
        if (fDegree <= 0 && State == RotateState.OUTSIDE) {
            Set_State(RotateState.INSIDE);
            fDegree = 360;
        }
        else if (fDegree <= 0 && State == RotateState.INSIDE) {
            Set_State(RotateState.OUTSIDE);
            fDegree = 360;
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

                break;
            case RotateState.OUTSIDE:
                //�O����]�p����
                fRadius = 9.0f;

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
            iPassNum++;

            int ordernum = csOrderManager.Get_iOrderNum();

            // �w�ߐ��̃I�u�W�F�N�g���ʉ߂����烊�Z�b�g
            if (iPassNum == ordernum) {
                if (iMatchNum == ordernum) {
                    // �N���A�X�^���v����
                    Instantiate(gClear, new Vector3(20, 0, -10 + iClearNum * 5),
                        Quaternion.Euler(0, 180, 0));

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
