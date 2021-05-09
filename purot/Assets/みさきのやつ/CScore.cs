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

public class CScore : MonoBehaviour
{
    public static int Score;
    public static GameObject ScoreObj; // TextオブジェクトをあとでFindして入れる、リザルトでだけ使う

    void Start()
    {
        // 一回だけ実行したいからスタートに書きました。後悔はしています。
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Score = 0;
        }
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            DispScore();
        }

    }

    public static void AddScore()
    {
        Score++;
    }

    public static int GetScore()
    {
        return Score;
    }

    public static void DispScore()
    {
        // オブジェクトからTextコンポーネントを取得
        ScoreObj = GameObject.Find("Text");
        Text ScoreText = ScoreObj.GetComponent<Text>();

        // テキストの表示を入れ替える
        ScoreText.text = Score.ToString();
    }
    public static void ResetScore()
    {
        Score = 0;
    }
}
