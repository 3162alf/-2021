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
    public static int Score;
    public static GameObject ScoreObj; // Text�I�u�W�F�N�g�����Ƃ�Find���ē����A���U���g�ł����g��

    void Start()
    {
        // ��񂾂����s����������X�^�[�g�ɏ����܂����B����͂��Ă��܂��B
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
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        ScoreObj = GameObject.Find("Text");
        Text ScoreText = ScoreObj.GetComponent<Text>();

        // �e�L�X�g�̕\�������ւ���
        ScoreText.text = Score.ToString();
    }
    public static void ResetScore()
    {
        Score = 0;
    }
}
