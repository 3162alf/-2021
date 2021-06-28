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
    float fFadeSpeed = 0.002f;
    float fRed, fGreen, fBlue, fAlpha;
    public bool isFadeIn = false;
    Image IMG_Fade;


    void Start() {
        // カメラ(SE用)取得
        gCamera = Camera.main.gameObject;

        // コンポーネント取得
        aAudioSource = gCamera.GetComponent<AudioSource>();

        // Fade用
        IMG_Fade = GetComponent<Image>();
        fRed = IMG_Fade.color.r;
        fGreen = IMG_Fade.color.g;
        fBlue = IMG_Fade.color.b;
        fAlpha = IMG_Fade.color.a;
    }

    //ゲーム画面に遷移
    public void OnChangeScene_Game(){
        isFadeIn = true;
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameScene");
    }
    //チュートリアル画面に遷移
    public void OnChangeScene_Tutorial(){
        isFadeIn = true;
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TutorialScene");
    }
    //タイトル画面に遷移
    public void OnChangeScene_Title() {
        isFadeIn = true;
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("TitleScene");
    }
    //ランキング画面に遷移
    public void OnChangeScene_Ranking(){
        isFadeIn = true;
        aAudioSource.PlayOneShot(aSE01);
        stRecentlyScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("RankingScene");
    }

    public static string GetRecently(){
        return stRecentlyScene;
    }

    public static void SetRecently(string name) { 
        stRecentlyScene = name;
    }


  
}
