/*==============================================================================
    Project
    [CScore.cs]
    �E�X�R�A�Ŏg�������Ȃ�}篗p�ӂ��܂����B
--------------------------------------------------------------------------------
    2021.05.09 Sasaki Misaki
================================================================================
    History
        2021/06/21 Ota Kaname
            
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEditor;

public class CScore : MonoBehaviour {
    public Text tScore;

    private static int iScore;
    private int iScoreParam;
    private static float fScore = 0.0f;
    public static GameObject gScoreObj; // Text�I�u�W�F�N�g�����Ƃ�Find���ē����A���U���g�ł����g��


    // �X�v���C�g�\���֘A
    Vector3 vInitPos;                    // �\���ʒu
    private int iPoint;                  // �\������l
    private float fSize = 1;             // �\���T�C�Y
    private static int iSort = 0;        // �����̕\����
    private const int SORT_MAX = 30000;  // �\�[�g���鐔�l�̍ő吔
    private int iOldScore = 0;           // �X�R�A�̍����m�F�p
    private int iOldScoreParam = 0;      // �X�R�A�p�����[�^�̍����m�F�p


    void Start() {
        // ��񂾂����s����������X�^�[�g�ɏ����܂����B����͂��Ă��܂��B
        if (SceneManager.GetActiveScene().name == "GameScene") {
            iScore = 0;
        }
        if (SceneManager.GetActiveScene().name == "ResultScene") {
            DispScore();
        }

        // �X�R�A�\��������
        Init(iScore, new Vector3(0, 0, 0));
        CreateScoreSprite(iScoreParam, -4.0f);
    }

    void Update() {
        iScore = (int)fScore;
        fScore = iScore;

        // �X���b�V���̂ݕ\��
        if (tScore != null) {
            tScore.text = "/";
        }

        // ���O�̃X�R�A������Ă�����\����؂�ւ���
        if(iScore != iOldScore || iScoreParam != iOldScoreParam) {
            Debug.Log("�ʂ���");

            // �q�I�u�W�F�N�g���^�C�}�[�̃X�v���C�g�Ȃ̂ł��������
            GameObject obj = GameObject.Find("PFB_ScoreObj");

            // ���݂̃X�R�A�\�����폜
            foreach (Transform child in obj.transform) {
                Destroy(child.gameObject);
            }

            // �X�R�A�̕\��
            CreateScoreSprite(iScore, 0.0f);
            CreateScoreSprite(iScoreParam, -4.0f);
        }

        // ���O�̃X�R�A�B���X�V
        iOldScore = iScore;
        iOldScoreParam = iScoreParam;
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
        fScore = 0.0f;
    }


    public void Init(int point, Vector3 pos) {
        // �K�v�ȏ����i�[
        this.iPoint = point;

        // �\���p�̃_���[�W�����
        CreateScoreSprite(point, 0.0f);

        vInitPos = pos;

        // �\��������ԏ��
        GetComponent<SortingGroup>().sortingOrder = iSort;

        iSort++;
        if (iSort > SORT_MAX) {
            iSort = 0;
        }

    }

    // �`��p�̐��������
    private void CreateScoreSprite(int point, float X) {

        // ��������o��
        int iDigit = ChkDigit(point);

        // �����v���n�u��ǂݍ��ށA�e�X�g�p�̃t�H���_�ƃt�@�C����
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Score/PFB_Score.prefab");

        // ���̕������I�u�W�F�N�g�����o�^���Ă���
        for (int i = 0; i < iDigit; i++) {

            GameObject numObj = Instantiate(obj) as GameObject;

            Debug.Log(numObj);

            // �q���Ƃ��ēo�^
            numObj.transform.parent = transform;

            // ���݃`�F�b�N���Ă��錅�̐���������o��
            int digNum = GetPointDigit(point, i + 1);

            // �|�C���g���琔����؂�ւ���
            numObj.GetComponent<CScoreController>().ChangeSprite(digNum);

            // �T�C�Y���Q�b�g����
            float size_w = numObj.GetComponent<SpriteRenderer>().bounds.size.x;

            // �ʒu�����炷 0.75f�͐����̊Ԋu�̒���
            float ajs_x = (size_w * i - (size_w * iDigit) / 2) * 1.0f + X;
            Vector3 pos = new Vector3(numObj.transform.position.x - ajs_x, numObj.transform.position.y, numObj.transform.position.z);
            numObj.transform.position = pos;

            numObj = null;
        }

    }

    // �����̌�����Ԃ�
    public static int ChkDigit(int num) {

        //0�̏ꍇ1���Ƃ��ĕԂ�
        if (num == 0) return 1;

        //�ΐ��Ƃ����g���ĕԂ�
        return (num == 0) ? 1 : ((int)Mathf.Log10(num) + 1);

    }

    // ���l�̒�����w�肵�����̒l��������
    public static int GetPointDigit(int num, int digit) {

        int res = 0;
        int pow_dig = (int)Mathf.Pow(10, digit);
        if (digit == 1) {
            res = num - (num / pow_dig) * pow_dig;
        }
        else {
            res = (num - (num / pow_dig) * pow_dig) / (int)Mathf.Pow(10, (digit - 1));
        }

        return res;
    }
}
