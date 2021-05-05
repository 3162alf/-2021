using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScoreManager : MonoBehaviour
{
    public class CPlayerscore
    {
        public string name;
        public int score;
    }

    private List<CPlayerscore> PlayerScore = new List<CPlayerscore>();
    private CPlayerscore OverwriteScore = new CPlayerscore();
    
    private void Start()
    {

        OverwriteScore.score = 10;
        Load();
    
        OverwriteRecord();

        SaveScore();
        ScoreDisplay();
    }

    public void SaveScore()
    { 
    
        //3
        for (int i=0; i < PlayerScore.Count; i++)
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
        for (i=0; i < PlayerScore.Count; i++)
        {
            if(PlayerScore[i].score < OverwriteScore.score)
            {
                PlayerScore.Insert(i, OverwriteScore);
                i = 10;
                break;
            }

        }
        if(i < 10)
        {
            PlayerScore.Add(OverwriteScore);
        }
        if(PlayerScore.Count > 10)
        {
            PlayerScore.RemoveAt(PlayerScore.Count - 1);
        }

        if(PlayerScore.Count == 0)
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
        for(int i=0; i< PlayerScore.Count; i++)
        {
            GameObject.Find("score" + (i+1).ToString()).GetComponent<Text>().text = PlayerScore[i].score.ToString();
        }
    }
}
