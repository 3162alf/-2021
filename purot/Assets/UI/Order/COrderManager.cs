/*==============================================================================
    Project_HOGE
    [COrderManager.cs]
    �E�w�ߐ���
--------------------------------------------------------------------------------
    2021.04.19 @Author Suzuki Hayase
================================================================================
    History

        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COrderManager : MonoBehaviour {
    [SerializeField] private GameObject[] gOrderSource;            // �w�߃I�u�W�F�N�g�z��
    private int[] iOrder = {-1, -1, -1, -1, -1 };                  // �w�ߔz��
    private GameObject[] gOrder = { null, null, null, null, null}; // �w��

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    // �w�ߐ����֐�
    public int[] CreateOrder(int n) {
        int kind = gOrderSource.Length;

        for(int i = 0; i < kind; i++) {
            if (gOrder[i]) {
                Destroy(gOrder[i]);
            }
        }

        for (int i = 0; i < n; i++) {
            iOrder[i] = Random.Range(0, kind - 1);
            Debug.Log(iOrder[i]);
            gOrder[i] = Instantiate(gOrderSource[iOrder[i]], new Vector3(-18, 0, 5 - i * 5), Quaternion.Euler(0, 180, 0));
        }

        return iOrder;
    }
}
