/*==============================================================================
    PROJECT ???
    [CTitleController.cs]
    ・ボタン入力でゲームスタートする処理
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

    // ゲーム画面に遷移
    public void OnClickStartButtun(){
        SceneManager.LoadScene("SampleScene");
    }
}
