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
    public class CPlayer
    {
        public int[] name = new int[3];
        public string sName = null;
        public int score;
    }

    private List<CPlayer> lPlayer = new List<CPlayer>();
    private CPlayer OverwritePlayer = new CPlayer();
    public int iDigits = 3;  // �\�����錅��

    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameHome = "Xbox_Home";    // �z�[���{�^��
    [SerializeField] private AudioClip aSE01;
    AudioSource aAudioSource;

    // CNameManager�̓ǂݍ���
    public GameObject gNameManagerObj;
    private CNameManager cnmScript;

    bool bIs;
    private void Start()
    {
        aAudioSource = GetComponent<AudioSource>();

        // CNameManager�̎擾
        gNameManagerObj = GameObject.Find("PFB_Words");
        cnmScript = gNameManagerObj.GetComponent<CNameManager>();

        Load();

        bIs = false;
    }
    void Update()
    {
        if(!bIs)
        {
            // ���U���g������ł����ꍇ�͖��O�ƃX�R�A��o�^����
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
        // �z�[���{�^������������^�C�g���ɖ߂�悤�ɑJ�ځi�����łł���悤�ɂ����������́B�B�B�j
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
                // �J���}��؂�ň�ɕς���
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


                    Debug.Log("�Z�b�g�l�`��");
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

            // �J���X�g�p�̍ő吔�l�����
            int count_stop_score = 1;
            for (int j = 0; j < iDigits; j++)
            {
                count_stop_score *= 10;
            }
            //�ő�l�̕␳����
            if (lPlayer[i].score >= count_stop_score)
            {
                lPlayer[i].score = count_stop_score - 1;
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
