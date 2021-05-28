/*==============================================================================
    PROJECT ???
    [CSceneManager.cs]
    ・シーン遷移をまとめたやつ
--------------------------------------------------------------------------------
    2021.04.22 @Author Tsubasa Ono
================================================================================
    History
        2021.05.27 Ota
            SEように色々追加
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager : MonoBehaviour{

    private static string stRecentlyScene = "";

    // SE用
    [SerializeField] private AudioClip aSE01;           // SE格納するやつ
    private GameObject gCamera;                         // AudioSource取得用
    AudioSource aAudioSource;                           // コンポーネント取得用

    void Start() {
        // カメラ(SE用)取得
        gCamera = Camera.main.gameObject;

        // コンポーネント取得
        aAudioSource = gCamera.GetComponent<AudioSource>();
    }

    //タイトル画面からゲーム画面に遷移
    public void OnTitletoGame() {
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameScene");
    }

    // タイトル画面からチュートリアル画面に遷移
    public void OnTitletoTutorial() {
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TutorialScene");
    }

    // タイトル画面からランキング画面に遷移
    public void OnTitletoRanking() {
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("RankingScene");
    }

    // リザルト画面からタイトル画面に遷移
    public void OnResulttoTitle() {
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TitleScene");
    }

    // リザルト画面からゲーム画面に遷移
    public void OnResulttoRetly() {
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameScene");
    }

    // リザルト画面からランキング画面に遷移
    public void OnResulttoRanking() {
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("RankingScene");
    }

    //ランキング画面からタイトル画面に遷移
    public void OnRankingtoTitle() {
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TitleScene");
    }


    public static string GetRecently()
    {
        return stRecentlyScene;
    }

    public static void SetRecently(string name)
    {
        stRecentlyScene = name;
    }
}
