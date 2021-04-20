/*==============================================================================
    Project_
    [CGate.cs]
    �E�Q�[�g����
--------------------------------------------------------------------------------
    2021.04.19 @Author Suzuki Hayase
================================================================================
    History
        
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGate : MonoBehaviour {
    private int[] iOrder;                             // �w��
    private int iOrderNum;                            // �w�ߐ�
    private int iPassNum;                             // �ʉߐ�
    private int iMatchNum;                            // ��v��
    private int iClearNum;                            // �N���A��
    private COrderManager csOrderManager;             // OrderManager�X�N���v�g

    [SerializeField] private GameObject[] gClearLamp; // �N���A�����v

    [SerializeField] private GameObject gClear;       // �N���A�X�^���v

    // Start is called before the first frame update
    void Start() {
        iMatchNum = 0;
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();

        // �w�ߐ���
        iOrderNum = 3;
        iOrder = csOrderManager.CreateOrder(iOrderNum);      
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider col) {
        // �ʉ߃I�u�W�F�N�g����
        if (col.gameObject.tag == "RotateObject") {
            // �w�߂ƈ�v
            if(iOrder[iPassNum] == col.gameObject.GetComponent<CRotateObject>().Get_Shape()) {
                iMatchNum++;
                gClearLamp[iPassNum].GetComponent<Renderer>().material.color = Color.green;
            }
            else {
                gClearLamp[iPassNum].GetComponent<Renderer>().material.color = Color.red;
            }
            iPassNum++;

            // �w�ߐ��̃I�u�W�F�N�g���ʉ߂����烊�Z�b�g
            if (iPassNum == iOrderNum) {
                if(iMatchNum == iOrderNum) {
                    // �N���A�X�^���v����
                    Instantiate(gClear, new Vector3(20, 0, -10 + iClearNum * 5), Quaternion.Euler(0, 0, 0));
                    
                    // �w�ߐ���
                    iOrder = csOrderManager.CreateOrder(iOrderNum);
                    iClearNum++;
                }

                iMatchNum = 0;
                iPassNum = 0;

                gClearLamp[0].GetComponent<Renderer>().material.color = Color.white;
                gClearLamp[1].GetComponent<Renderer>().material.color = Color.white;
                gClearLamp[2].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
