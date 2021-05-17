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

    private static string stRecentlyScene = "";

    //タイトル画面からゲーム画面に遷移
    public void OnTitletoGame() {
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameScene");
    }

    // タイトル画面からチュートリアル画面に遷移
    public void OnTitletoTutorial() {
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TutorialScene");
    }

    // タイトル画面からランキング画面に遷移
    public void OnTitletoRanking() {
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("RankingScene");
    }

    // リザルト画面からタイトル画面に遷移
    public void OnResulttoTitle() {
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TitleScene");
    }

    // リザルト画面からゲーム画面に遷移
    public void OnResulttoRetly() {
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameScene");
    }

    // リザルト画面からランキング画面に遷移
    public void OnResulttoRanking() {
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("RankingScene");
    }

    //ランキング画面からタイトル画面に遷移
    public void OnRankingtoTitle() {
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TitleScene");
    }

    public static string GetRecently()
    {
        return stRecentlyScene;
    }
}
