/*==============================================================================
    PROJECT ???
    [CSceneManager.cs]
    ・シーン遷移をまとめたやつ
--------------------------------------------------------------------------------
    2021.04.22 @Author Tsubasa Ono
================================================================================
    History
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager : MonoBehaviour{
    //タイトル画面からゲーム画面に遷移
    public void OnTitletoGame() {
        SceneManager.LoadScene("GameScene");
    }

    // タイトル画面からランキング画面に遷移
    public void OnTitletoRanking() {
        SceneManager.LoadScene("RankingScene");
    }

    // リザルト画面からタイトル画面に遷移
    public void OnResulttoTitle() {
        SceneManager.LoadScene("TitleScene");
    }

    // リザルト画面からゲーム画面に遷移
    public void OnResulttoRetly() {
        SceneManager.LoadScene("GameScene");
    }

    // リザルト画面からランキング画面に遷移
    public void OnResulttoRanking() {
        SceneManager.LoadScene("RankingScene");
    }

    //ランキング画面からリザルト画面に遷移
    public void OnRankingtoResult() {
        SceneManager.LoadScene("ResultScene");
    }
}
