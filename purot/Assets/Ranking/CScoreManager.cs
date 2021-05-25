/*==============================================================================
     Project
    [CScoreManager.cs]
    �E�����L���O�\�������邽�߂̃X�R�A�����B
--------------------------------------------------------------------------------
    2021.05.06 Tamura Yusuke
==============================================================================
    History    
        2021.05.09 @Author Sasaki Misaki
            25�s�ڂ����Ă��������B�����ƁA�X�R�A�̐��l���Ԃ����ޏ����ǉ����Ă܂��B
============================================================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CScoreManager : MonoBehaviour
{
    public class CPlayerscore
    {
        public string name;
        public int score;
    }

    private List<CPlayerscore> PlayerScore = new List<CPlayerscore>();
    private CPlayerscore OverwriteScore = new CPlayerscore();
    public int iDigits = 3;  // �\�����錅��

    private void Start()
    {
        
        Load();

        if (CSceneManager.GetRecently() == "ResultScene")
        {
            //========== 2021/5/09
            // �X�R�A��\������̂ɕK�v�Ȃ̂� = �̌�돑�������܂����B�@by���X��
            OverwriteScore.score = CScore.GetScore();
            OverwriteRecord();
        }

        SaveScore();

        ScoreDisplay();
    }

    public void SaveScore()
    {
        //3
        for (int i = 0; i < PlayerScore.Count; i++)
        {
            int saveNum = i + 1;

            PlayerPrefs.SetInt("Score" + saveNum.ToString(), PlayerScore[i].score);
            PlayerPrefs.SetString("Name" + saveNum.ToString(), PlayerScore[i].name);
        }
        PlayerPrefs.Save();
    }

    void OverwriteRecord()
    {
        
        int i = 0;
        for (i = 0; i < PlayerScore.Count; i++)
        {
            if (PlayerScore[i].score < OverwriteScore.score)
            {
                PlayerScore.Insert(i, OverwriteScore);
                i = 10;
                break;
            }
        }
        if (i < 10)
        {
            PlayerScore.Add(OverwriteScore);
        }
        if (PlayerScore.Count > 10)
        {
            PlayerScore.RemoveAt(PlayerScore.Count - 1);
        }

        if (PlayerScore.Count == 0)
        {
            PlayerScore.Add(OverwriteScore);
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

            if (PlayerPrefs.HasKey("Score" + loadNum.ToString()))
            {
                CPlayerscore playerscore = new CPlayerscore();
                playerscore.score = PlayerPrefs.GetInt("Score" + loadNum.ToString());

                PlayerPrefs.SetInt("Score" + saveNum, playerscore.score);

                if (PlayerPrefs.HasKey("Name" + loadNum.ToString()))
                {
                    playerscore.name = PlayerPrefs.GetString("Name" + loadNum.ToString());

                    PlayerPrefs.SetString("Name" + saveNum, playerscore.name);

                    PlayerScore.Add(playerscore);

                    saveNum += 1;
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

        for (int i = 0; i < PlayerScore.Count; i++)
        {
            int scorenumber = 0;
            string stock = "";
            int score = PlayerScore[i].score;

            // �J���X�g�p�̍ő吔�l�����
            int count_stop_score = 1;
            for (int j = 0; j < iDigits; j++)
            {
                count_stop_score *= 10;
            }
            //�ő�l�̕␳����
            if (PlayerScore[i].score >= count_stop_score)
            {
                PlayerScore[i].score = count_stop_score - 1;
            }

            // �����\��
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
