/*==============================================================================
    PROJECT NAME
    [CTitleButton.cs]
    ・タイトルボタンを押すとタイトル画面に遷移する
--------------------------------------------------------------------------------
    2021.04.07 @Author Tsubasa Ono
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class CTitleButton : MonoBehaviour {
    public void OnTitle() {
        SceneManager.LoadScene("TitleScene");
    }
}
