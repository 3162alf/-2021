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

//リザルト画面からゲーム画面に遷移
public class CRetlyButton : MonoBehaviour {
    public void OnRetry() {
        SceneManager.LoadScene("SampleScene");
    }
}
