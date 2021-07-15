/*==============================================================================
     Project
    [CScoreManager.cs]
    �E�����L���O�\�������邽�߂̃X�R�A�Ɩ��O�������B
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

    // CNameManager�̓ǂݍ���
    public GameObject gNameManagerObj;
    private CNameManager cnmScript;

    private bool bDispName = false;

    private string stOldScene;

    public GameObject[] gScoreObject = new GameObject [10];
    public GameObject[] gNameObject = new GameObject [10];

    private static int iNameSize = 3; //�S�̕�����
    private static int iMaxPlayer = 10;


    void Start()
    {

        // CNameManager�̎擾
        //gNameManagerObj = GameObject.Find("PFB_Words");
        cnmScript = gNameManagerObj.GetComponent<CNameManager>();

        Load();

        if (CSceneManager.GetRecently() == "ResultScene")
        {
            OverwritePlayer.score = CScore.GetScore();
            OverwritePlayer.score = 3;
            Debug.Log("�X�R�A" + OverwritePlayer.score);

            //OverwritePlayer.name = cnmScript.GetName();
            //Debug.Log(OverwritePlayer.name[0]);
            //Debug.Log(OverwritePlayer.name[1]);
            //Debug.Log(OverwritePlayer.name[2]);
        }
        // ���㏑��
        OverwriteRecord();  

        // �X�V�����f�[�^��PlayerPrefs
        SaveScore();
        // �����L���O�\��
        ScoreDisplay();
        NameDisplay();

        stOldScene = CSceneManager.GetRecently();

    }

    void Update()
    {
        //NameOverwrite();

        // �z�[���{�^����M�L�[����������^�C�g���ɖ߂�悤�ɑJ�ځi�����łł���悤�ɂ����������́B�B�B�j
        if (Input.GetButtonDown(stButtonNameHome) || Input.GetKeyDown(KeyCode.M))
        {
            CSceneManager CSM = GameObject.Find("FadeCanvas").GetComponent<CSceneManager>();
            CSM.OnChangeScene_Title();
        }
    }

    public void SaveScore()
    {
        //3
        for (int i = 0; i < lPlayer.Count; i++)
        {
            Debug.Log(lPlayer.Count);
            int saveNum = i + 1;

            PlayerPrefs.SetInt("SCORE" + saveNum.ToString(), lPlayer[i].score);
            
            //for (int j = 0; j < 3; j++)
            //{
                // �J���}��؂�ň�ɕς���
            //    lPlayer[i].sName = lPlayer[i].sName + lPlayer[i].name[j].ToString() + ",";
            //}
            for(int j=0; j< iNameSize; j++)
            { 
                PlayerPrefs.SetInt("NAME" + saveNum.ToString() + j, lPlayer[i].name[j]);
            }
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

                PlayerPrefs.SetInt("SCORE" + loadNum.ToString(), playerscore.score);
                Debug.Log("�Z�b�g�X�R�`�A");

                lPlayer.Add(playerscore);

                for(int i = 0; i < iNameSize; i++)
                {
                    if (PlayerPrefs.HasKey("NAME" + loadNum.ToString() + i))
                    {
                        playerscore.name[i] = PlayerPrefs.GetInt("NAME" + loadNum.ToString() + i);

                        //string str = playerscore.sName;
                        //string[] strArray = str.Split(',');
                        //playerscore.name = Array.ConvertAll(strArray, int.Parse);
                        //Debug.Log(playerscore.name);
                        //lPlayer.Add(playerscore);
                        //saveNum += 1;
                        lPlayer.Add(playerscore);
                        Debug.Log("�Z�b�g�l�`��");
                        //cnmScript.SetName(playerscore.name);
                    }
                }
            }

            saveNum += 1;
        }
    }

    public void ScoreDisplay()
    {
        Stack<string> stack = new Stack<string>();

        for (int i = 0; i < iMaxPlayer; i++)
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
            {
                stock += stack.Pop();
            }

            //Debug.Log(gTopObject.GetComponent<Text>().text);
            //GameObject.Find("score" + (i + 1).ToString()).GetComponent<Text>().text = stock;
            gScoreObject[i].GetComponent<Text>().text = stock;

            if (lPlayer.Count <= iMaxPlayer)
                break;
        }
    }

    public void NameDisplay()
    {
        for (int i = 0; i < iMaxPlayer; i++)
        {
            gNameObject[i].GetComponent<CNameManager>().SetName(lPlayer[i].name);

            if (lPlayer.Count <= iMaxPlayer)
                break;
        }  
    }

    public void NameOverwrite()
    {
        // �ŐV��word_PFB�ɍX�V
        cnmScript = gNameManagerObj.GetComponent<CNameManager>();

        if (cnmScript.GetbIsEnd() && !bDispName)
        {

            Load();

            if (CSceneManager.GetRecently() == "ResultScene")
            {
                //OverwritePlayer.score = CScore.GetScore();
                //Debug.Log(OverwritePlayer.score);

                OverwritePlayer.name = cnmScript.GetName();
                Debug.Log(OverwritePlayer.name[0]);
                Debug.Log(OverwritePlayer.name[1]);
                Debug.Log(OverwritePlayer.name[2]);
            }

            OverwriteRecord();

            SaveScore();

            bDispName = true;

            NameDisplay();
        }
    }
}
