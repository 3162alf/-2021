/*==============================================================================
    PROJECT ???
    [CRankingController.cs]
    �E�{�^�����͂Ń��U���g�߂鏈��
--------------------------------------------------------------------------------
    2021.04.9 @Author Yusuke Tamura
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CRankingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �Q�[����ʂɑJ��
    public void OnClickStartButtun()
    {
        SceneManager.LoadScene("Result");
    }
}
