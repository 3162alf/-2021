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
    private int iScoreParam;
    private float fScore;
    public static GameObject gScoreObj; // Text�I�u�W�F�N�g�����Ƃ�Find���ē����A���U���g�ł����g��

    void Start() {
        // ��񂾂����s����������X�^�[�g�ɏ����܂����B����͂��Ă��܂��B
        if (SceneManager.GetActiveScene().name == "GameScene") {
            iScore = 0;
        }
        if (SceneManager.GetActiveScene().name == "ResultScene") {
            DispScore();
        }

        fScore = 0.0f;
    }

    void Update() {

        iScore = (int)fScore;
        fScore = iScore;

        if (tScore != null) {
            // �e�L�X�g�ɃX�R�A�\��
            tScore.text = iScore.ToString() + "/" + iScoreParam.ToString();
        }
    }

    public static void AddScore() {
        iScore += 1;
    }

    public static int GetScore() {
        return iScore;
    }

    public void Set_iScoreParam(int i) {
        iScoreParam = i;
    }

    public void AddFScore() {
        fScore += 0.34f;
        Debug.Log(fScore);
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
