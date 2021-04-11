/*==============================================================================
    PROJECT NAME
    [CRankingButton.cs]
    ・ランキングボタンを押すとランキング画面に遷移する
--------------------------------------------------------------------------------
    2021.04.07 @Author Tsubasa Ono
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class CRankingButton : MonoBehaviour {
    public void OnRanking() {
        SceneManager.LoadScene("RankingScene");
    }
}
