/*==============================================================================
    Project_
    [CGateTimerController.cs]
    �E�Q�[�g�^�C�}�[����
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        20210511 Hirano XBox�R���g���[���[���͏����ǉ�
            
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

    // 20210511 �ǉ���---------------------------------

    [SerializeField, TooltipAttribute("A�{�^���̓o�^��")]
    private string stButton0Name = "joystickbutton0";  // A�{�^��
    //[SerializeField, TooltipAttribute("B�{�^���̓o�^��")]
    //private string stButton1Name = "joystickbutton1";  // B�{�^��
    //[SerializeField, TooltipAttribute("X�{�^���̓o�^��")]
    //private string stButton2Name = "joystickbutton2";    // X�{�^��
    //[SerializeField, TooltipAttribute("Y�{�^���̓o�^��")]
    //private string stButton3Name = "joystickbutton3";  // Y�{�^��

    // ------------------------------------------------

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

        if (GameObject.Find(gGate.name + "(Clone)")) {
            tTimer.text = "00";
        }
        /* �����l�̐ݒ�
        if (iSecond <= 0) {
            tTimer.text = "00";
            fTotalTime = 0.0f;
        }*/

        // ���Ԃ�������܂��̓v���C���[�̈ӎv�ŉ��
        if (iSecond == 0 || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown(stButton0Name)) {
            // �Q�[�g����o��̂�h��
            if (GameObject.Find(gGate.name + "(Clone)") == null) {
                List<GameObject> list = csObjectManager.Get_gObjectList();

                if (list.Count > 0) {
                    OBJECT_SHAPE order = csOrderManager.Get_Order(0);

                    // �w�߂̍ŏ��̃I�u�W�F�N�g�̈ʒu�ɃQ�[�g���o��
                    GameObject first = null;

                    int i;
                    for (i = 0; i < list.Count; i++) {
                        if (list[i].GetComponent<CRotateObject>().Get_Shape() == order) {
                            first = list[i];
                            break;
                        }
                    }

                    if (i < list.Count) {
                        float deg = first.GetComponent<CRotateObject>().Get_fDegree();
                        float subdeg = first.GetComponent<CRotateObject>().Get_fDegreeSub();
                        deg += 30;
                        Vector3 pos;

                        if (deg >= 330 && deg <= 690) {
                            subdeg = 180;
                        }
                        else {
                            subdeg = 0.0f; 
                        }

                        pos.x = 7 * Mathf.Sin((deg + 180) * Mathf.Deg2Rad)
                        + 2 * Mathf.Cos(subdeg * Mathf.Deg2Rad) * Mathf.Sin((deg + 180) * Mathf.Deg2Rad);

                        pos.y = 2 * Mathf.Sin(subdeg * Mathf.Deg2Rad);

                        pos.z = 7 * Mathf.Cos((deg + 180) * Mathf.Deg2Rad)
                        + 2 * Mathf.Cos(subdeg * Mathf.Deg2Rad) * Mathf.Cos((deg + 180) * Mathf.Deg2Rad);

                        GameObject gate = Instantiate(gGate, pos, Quaternion.Euler(0, 90, 0));
                        //transform) as GameObject;

                        CGate cs = gate.GetComponent<CGate>();
                        CRotateObject cro = first.GetComponent<CRotateObject>();

                        if(deg >= 330 && deg < 690) {
                            cs.Set_State(RotateState.INSIDE);

                            if(deg <= 400 && deg >= 320) {
                                cs.Set_isInverse(true);
                                cs.Set_fDegreeSub(180 + (400 - deg) * 4);
                            }
                        }
                        else {
                            cs.Set_State(RotateState.OUTSIDE);
                            if(deg <= 760 && deg >= 690) {
                                cs.Set_isInverse(true);
                                cs.Set_fDegreeSub((760 - deg) * 4);
                            }
                        }

                        cs.Set_fDegree(deg);

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
