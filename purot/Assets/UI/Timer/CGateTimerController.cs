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
    private COrderManager1 csOrderManager;

    void Start() {
        csObjectManager = GameObject.Find("PFB_ObjectManager").GetComponent<CObjectManager>();
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager1>();
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

        if(iSecond == 0) {
            List<GameObject> list = csObjectManager.Get_gObjectList();
            OBJECT_SHAPE order = csOrderManager.Get_Order(0);

            GameObject first = new GameObject();
            CRotateObject cro = new CRotateObject();

            for(int i = 0; i < list.Count; i++) {
                cro = list[i].GetComponent<CRotateObject>();
                if (cro.Get_Shape() == order) {
                    first = list[i];
                    break;
                }
            }

            Vector3 pos = first.transform.position;

            GameObject gate = Instantiate(gGate, pos, Quaternion.Euler(0, 0, 90));
            CGate1 cs = gate.GetComponent<CGate1>();
            cs.Set_State(cro.Get_RotateState());
            cs.Set_fDegree(cro.Get_fDegree());

            fTotalTime = -1;
        }

    }

    public void Reset() {
        fTotalTime = 5;
    }
}
