using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPanel : MonoBehaviour
{
    private int iPanelDispCnt;

    // Start is called before the first frame update
    void Start()
    {
        iPanelDispCnt = 0;
        Debug.Log("�p�l������");
    }

    // Update is called once per frame
    void Update()
    {
        // �\������Ă���ꍇ
        if(this.gameObject.activeSelf)
        {
            iPanelDispCnt++;
            if(iPanelDispCnt >= 15)
            {
                Destroy(this.gameObject);
                Debug.Log("�p�l���j��");
            }
        }
    }
}
