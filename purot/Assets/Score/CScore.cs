/*==============================================================================
    Project
    [CScore.cs]
    ・スコアで使いそうなやつ急遽用意しました。
--------------------------------------------------------------------------------
    2021.05.09 Sasaki Misaki
================================================================================
    History
        
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CScore : MonoBehaviour {
    public Text tScore;

    private static int iScore;
    public static GameObject gScoreObj; // TextオブジェクトをあとでFindして入れる、リザルトでだけ使う

    void Start() {
        // 一回だけ実行したいからスタートに書きました。後悔はしています。
        if (SceneManager.GetActiveScene().name == "GameScene") {
            iScore = 0;
        }
        if (SceneManager.GetActiveScene().name == "ResultScene") {
            DispScore();
        }
    }

    void Update() {
        if (tScore != null) {
            // テキストにスコア表示
            tScore.text = iScore.ToString() + "/100";
        }
    }

    public static void AddScore() {
        iScore += 1;
    }

    public static int GetScore() {
        return iScore;
    }

    public static void DispScore() {
        // オブジェクトからTextコンポーネントを取得
        gScoreObj = GameObject.Find("TextScore");
        Text ScoreText = gScoreObj.GetComponent<Text>();

        // テキストの表示を入れ替える
        ScoreText.text = iScore.ToString();
    }

    public static void ResetScore() {
        iScore = 0;
    }
}
