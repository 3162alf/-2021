/*==============================================================================
    Project
    [CScore.cs]
    �E�X�R�A�Ŏg�������Ȃ�}篗p�ӂ��܂����B
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
    public static int iScore;
    public static GameObject gScoreObj; // Text�I�u�W�F�N�g�����Ƃ�Find���ē����A���U���g�ł����g��

    void Start()
    {
        // ��񂾂����s����������X�^�[�g�ɏ����܂����B����͂��Ă��܂��B
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            iScore = 0;
        }
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            DispScore();
        }

    }

    public static void AddScore()
    {
        iScore+= 10;
    }

    public static int GetScore()
    {
        return iScore;
    }

    public static void DispScore()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        gScoreObj = GameObject.Find("Text");
        Text ScoreText = gScoreObj.GetComponent<Text>();

        // �e�L�X�g�̕\�������ւ���
        ScoreText.text = iScore.ToString();
    }
    public static void ResetScore()
    {
        iScore = 0;
    }
}
