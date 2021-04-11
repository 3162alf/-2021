/*==============================================================================
    PROJECT NAME
    [CRetlyButton.cs]
    ・リトライボタンを押すとゲーム画面に遷移する
--------------------------------------------------------------------------------
    2021.04.07 @Author Tsubasa Ono
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class CRetlyButton : MonoBehaviour {
    public void OnRetry() {
        SceneManager.LoadScene("SampleScene");
    }
}
