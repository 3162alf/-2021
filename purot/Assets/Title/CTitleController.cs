/*==============================================================================
    PROJECT ???
    [CTitleController.cs]
    �E�{�^�����͂ŃQ�[���X�^�[�g���鏈��
--------------------------------------------------------------------------------
    2021.04.04 @Author Kaname Ota
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CTitleController : MonoBehaviour{

    void Start(){

    }

    void Update(){
        
    }

    // �Q�[����ʂɑJ��
    public void OnClickStartButtun(){
        SceneManager.LoadScene("SampleScene");
    }
}
