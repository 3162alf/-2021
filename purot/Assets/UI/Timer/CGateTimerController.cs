/*==============================================================================
    Project_
    [CGateTimerController.cs]
    �E�Q�[�g�^�C�}�[����
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        
            
/*============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class CGateTimerController : MonoBehaviour {
    public Text tTimer;          // �e�L�X�g�����锠

    [SerializeField]
    private float fTotalTime;   // �������Ԃ̑�������
    private int iSecond = 0;    // �b��

    [SerializeField] GameObject gGate; // �Q�[�g
    private CObjectManager csObjectManager;
    private COrderManager csOrderManager;

    void Start() {
        csObjectManager = GameObject.Find("PFB_ObjectManager").GetComponent<CObjectManager>();
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();
    }

    void Update() {
        // �t���[�����Ƃɑ������Ԃ��猸�Z
        fTotalTime -= Time.deltaTime;

        // �L���X�g�����������Ԃ�b���ɑ��
        iSecond = (int)fTotalTime;

        // �e�L�X�g�ɕb����\��
        tTimer.text = iSecond.ToString("00");

        /* �����l�̐ݒ�
        if (iSecond <= 0) {
            tTimer.text = "00";
            fTotalTime = 0.0f;
        }*/

        // ���Ԃ�������܂��̓v���C���[�̈ӎv�ŉ��
        if(iSecond == 0 || Input.GetKeyDown(KeyCode.Return)) {
            // �Q�[�g����o��̂�h��
            if (GameObject.Find(gGate.name + "(Clone)") == null) {
                List<GameObject> list = csObjectManager.Get_gObjectList();

                if (list.Count > 0) {
                    OBJECT_SHAPE order = csOrderManager.Get_Order(0);

                    // �w�߂̍ŏ��̃I�u�W�F�N�g�̈ʒu�ɃQ�[�g���o��
                    GameObject first = new GameObject();

                    int i;
                    for (i = 0; i < list.Count; i++) {
                        if (list[i].GetComponent<CRotateObject>().Get_Shape() == order) {
                            first = list[i];
                            break;
                        }
                    }

                    if (i < list.Count) {
                        Vector3 pos = first.transform.position;

                        GameObject gate = Instantiate(gGate, pos, Quaternion.Euler(0, 0, 90));
                        CGate cs = gate.GetComponent<CGate>();
                        CRotateObject cro = list[i].GetComponent<CRotateObject>();

                        cs.Set_State(cro.Get_RotateState());
                        cs.Set_fDegree(cro.Get_fDegree());

                        fTotalTime = 0;
                    }
                }
            }
        }
    }

    // �^�C�}�[���Z�b�g�֐�
    public void Reset() {
        fTotalTime = 30;
    }
}
