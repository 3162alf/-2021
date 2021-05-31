/*==============================================================================
     Project
    [CScoreManager.cs]
    ・ランキング表示をするためのスコア処理。
--------------------------------------------------------------------------------
    2021.05.06 Tamura Yusuke
==============================================================================
    History    
        2021.05.09 @Author Sasaki Misaki
            25行目を見てください。そっと、スコアの数値をぶちこむ処理追加してます。
============================================================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CScoreManager : MonoBehaviour
{
    public class CPlayer
    {
        public int[] name = new int[3];
        public string sName = null;
        public int score;
    }

    private List<CPlayer> lPlayer = new List<CPlayer>();
    private CPlayer OverwritePlayer = new CPlayer();
    public int iDigits = 3;  // 表示する桁数

    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameHome = "Xbox_Home";    // ホームボタン
    [SerializeField] private AudioClip aSE01;
    AudioSource aAudioSource;

    // CNameManagerの読み込み
    public GameObject gNameManagerObj;
    private CNameManager cnmScript;

    bool bIs;
    private void Start()
    {
        aAudioSource = GetComponent<AudioSource>();

        // CNameManagerの取得
        gNameManagerObj = GameObject.Find("PFB_Words");
        cnmScript = gNameManagerObj.GetComponent<CNameManager>();

        Load();

        bIs = false;
    }
    void Update()
    {
        if(!bIs)
        {
            // リザルトから飛んできた場合は名前とスコアを登録する
            if (CSceneManager.GetRecently() == "ResultScene")
            {
                OverwritePlayer.name = cnmScript.GetName();
                OverwritePlayer.score = CScore.GetScore();
                OverwriteRecord();
            }

            SaveScore();
            ScoreDisplay();

            bIs = true;
        }
        // ホームボタンを押したらタイトルに戻るように遷移（自動でできるようにしたかったの。。。）
        if (Input.GetButtonDown(stButtonNameHome))
        {
            aAudioSource.PlayOneShot(aSE01);
            CSceneManager.SetRecently("TitleScene");
            SceneManager.LoadScene("TitleScene");
        }
    }
    public void SaveScore()
    {
        //3
        for (int i = 0; i < lPlayer.Count; i++)
        {
            int saveNum = i + 1;

            PlayerPrefs.SetInt("SCORE", lPlayer[i].score);

            for (int j = 0; j < 0; j++)
            {
                // カンマ区切りで一つに変える
                lPlayer[i].sName = lPlayer[i].sName + lPlayer[i].name[j].ToString() + ",";
            }
            PlayerPrefs.SetString("NAME", lPlayer[i].sName);
            Debug.Log(lPlayer[i].sName);

        }
        PlayerPrefs.Save();
    }

    void OverwriteRecord()
    {
        
        int i = 0;
        for (i = 0; i < lPlayer.Count; i++)
        {
            if (lPlayer[i].score < OverwritePlayer.score)
            {
                lPlayer.Insert(i, OverwritePlayer);
 
                i = 10;
                break;
            }
        }
        if (i < 10)
        {
            lPlayer.Add(OverwritePlayer);
        }
        if (lPlayer.Count > 10)
        {
            lPlayer.RemoveAt(lPlayer.Count - 1);
        }

        if (lPlayer.Count == 0)
        {
            lPlayer.Add(OverwritePlayer);
        }
    }

    public void Load()
    {
        //2
        int saveNum = 0;
        //3
        while (saveNum < 10)
        {
            int loadNum = saveNum + 1;

            if (PlayerPrefs.HasKey("SCORE" + loadNum.ToString()))
            {
                CPlayer playerscore = new CPlayer();
                playerscore.score = PlayerPrefs.GetInt("SCORE" + loadNum.ToString());

                PlayerPrefs.SetInt("SCORE" + saveNum, playerscore.score);

                if (PlayerPrefs.HasKey("NAME" + loadNum.ToString()))
                {
                    playerscore.sName = PlayerPrefs.GetString("NAME" + loadNum.ToString());

                    string str = playerscore.sName;
                    string[] strArray = str.Split(',');
                    playerscore.name = Array.ConvertAll(strArray, int.Parse);

                    lPlayer.Add(playerscore);

                    saveNum += 1;


                    Debug.Log("セットネ～ム");
                    cnmScript.SetName(playerscore.name);
                }
            }
            else
            {
                saveNum += 1;
            }

        }
    }

    public void ScoreDisplay()
    {
        Stack<string> stack = new Stack<string>();

        for (int i = 0; i < lPlayer.Count; i++)
        {
            int scorenumber = 0;
            string stock = "";
            int score = lPlayer[i].score;

            // カンスト用の最大数値を作る
            int count_stop_score = 1;
            for (int j = 0; j < iDigits; j++)
            {
                count_stop_score *= 10;
            }
            //最大値の補正処理
            if (lPlayer[i].score >= count_stop_score)
            {
                lPlayer[i].score = count_stop_score - 1;
            }

            // 文字表示
            for (int k = 0; k < iDigits; k++)
            {
                scorenumber = score % 10;
                score /= 10;

                stack.Push(Convert.ToString(scorenumber));
            }
            for (int l = 0; l < iDigits; l++)
                stock += stack.Pop();

            GameObject.Find("score" + (i + 1).ToString()).GetComponent<Text>().text = stock;
        }
    }
}
