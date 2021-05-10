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

public class CScore : MonoBehaviour {
    public Text tScore;

    private static int iScore;
    public static GameObject gScoreObj; // Text�I�u�W�F�N�g�����Ƃ�Find���ē����A���U���g�ł����g��

    void Start() {
        // ��񂾂����s����������X�^�[�g�ɏ����܂����B����͂��Ă��܂��B
        if (SceneManager.GetActiveScene().name == "GameScene") {
            iScore = 0;
        }
        if (SceneManager.GetActiveScene().name == "ResultScene") {
            DispScore();
        }
    }

    void Update() {
        if (tScore != null) {
            // �e�L�X�g�ɃX�R�A�\��
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
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        gScoreObj = GameObject.Find("TextScore");
        Text ScoreText = gScoreObj.GetComponent<Text>();

        // �e�L�X�g�̕\�������ւ���
        ScoreText.text = iScore.ToString();
    }

    public static void ResetScore() {
        iScore = 0;
    }
}
