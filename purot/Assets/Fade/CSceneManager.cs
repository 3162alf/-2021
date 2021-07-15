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
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CSceneManager : MonoBehaviour{

    private static string stRecentlyScene = "";

    // SE用
    [SerializeField] private AudioClip aSE01;           // SE格納するやつ
    private GameObject gCamera;                         // AudioSource取得用
    AudioSource aAudioSource;                           // コンポーネント取得用

    // Fade用
    public Fade fade;              //フェードキャンバス取得

    void Start() {

        // カメラ(SE用)取得
        gCamera = Camera.main.gameObject;

        // コンポーネント取得
        aAudioSource = gCamera.GetComponent<AudioSource>();
        fade = this.GetComponent<Fade>();

        fade.FadeOut(1f);
    }

    //ゲーム画面に遷移
    public void OnChangeScene_Game(){
        
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        //トランジションを掛けてシーン遷移する
        fade.FadeIn(1f, () =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }
    //チュートリアル画面に遷移
    public void OnChangeScene_Tutorial(){

        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        fade.FadeIn(1f, () =>
        {
            SceneManager.LoadScene("TutorialScene");
        });
    }
    //タイトル画面に遷移
    public void OnChangeScene_Title() {
      
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        fade.FadeIn(1f, () =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }
    //ランキング画面に遷移
    public void OnChangeScene_Ranking(){
  
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        fade.FadeIn(1f, () =>
        {
            SceneManager.LoadScene("RankingScene");
        });
    }
    //リザルト画面に遷移
    public void OnChangeScene_Result() {

        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        //トランジションを掛けてシーン遷移する
        fade.FadeIn(1f, () =>
        {
            SceneManager.LoadScene("ResultScene");
        });
    }

    public static string GetRecently(){
        return stRecentlyScene;
    }

    public static void SetRecently(string name) { 
        stRecentlyScene = name;
    }
}
