/*==============================================================================
    PROJECT ???
    [CTimer.cs]
    �������Ԃ̐�������鏈��
--------------------------------------------------------------------------------
    2021.06.20 @Author Kaname Ota
================================================================================
    History
        2021.06.20
            �������Ԃ��X�v���C�g�ŕ\��
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CTimer : MonoBehaviour
{
    // �\���ʒu
    Vector3 vInitPos;

    // �\���֘A
    private int iPoint;                  // �\������l
    private float fSize = 1;             // �\���T�C�Y
    private static int iSort = 0;        // �����̕\����
    private const int SORT_MAX = 30000;  // �\�[�g���鐔���̍ő吔

    // �^�C�}�[�֘A
    [SerializeField]
    private float fMinute;               // �����p�ϐ�

    [SerializeField]
    private float fSeconds;             // �b���p�ϐ�
    private float fOldSeconds;          // ��O�̕���
    private float fTotalTime;           // ��������
    private float fCountTime;           // �^�C�}�[�؂�ւ��悤


    void Start() {
        // �������Ԃ̎Z�o�Ə�����
        fTotalTime = fMinute * 60 + fSeconds;
        fOldSeconds = 0.0f;
        fCountTime = 0.0f;

        Init((int)fTotalTime, new Vector3(0, 0, 0));

    }

    public void Init(int point, Vector3 pos) {
        // �K�v�ȏ����i�[
        this.iPoint = point;

        // �\���p�̃_���[�W�����
        CreateNum(point,0.0f);

        vInitPos = pos;

        // �\��������ԏ��
        GetComponent<SortingGroup>().sortingOrder = iSort;
        
        iSort++;
        if (iSort > SORT_MAX) {
            iSort = 0;
        }
    }

    void Update() {
        // �������Ԃ��Ȃ��Ȃ����珈�����΂�
        if (fTotalTime <= 0.0f) {
            return;
        }

        // �������Ԃ̍X�V
        fTotalTime = fMinute * 60 + fSeconds;

        // �������Ԃ̌��Z
        fTotalTime -= Time.deltaTime;

        // �����̎Z�o
        fMinute = (int)fTotalTime / 60;

        // �b���̎Z�o
        fSeconds = fTotalTime - fMinute * 60;

        if ((int)fSeconds != (int)fOldSeconds) {
            Debug.Log("�ʂ���");
        
            // �q�I�u�W�F�N�g���^�C�}�[�̃X�v���C�g�Ȃ̂ł��������
            GameObject obj = GameObject.Find("PFB_TimerController");
        
            foreach (Transform child in obj.transform) {
                Destroy(child.gameObject);
            }

            // ��ԍ��̃[��
            CreateNum(0, 3.5f);

            // �R�������艟������
            CreateColon(0.0f);

            // ����
            CreateNum((int)fMinute, 2.0f);

            // �b���̕������p
            if((int)fSeconds >= 10) {
                CreateNum((int)fSeconds, -1.3f);
            }

            if ((int)fSeconds < 10) {
                CreateNum((int)fSeconds, -2.0f);
                CreateNum(0, -0.4f);
            }
        }

        // ��O�̕b���Ɍ��݂̎��Ԃ���
        fOldSeconds = fSeconds;


        // �������Ԃ��Ȃ��Ȃ����烊�U���g��ʂɑJ�ڂ���
        if (fTotalTime <= 0.0f) {
            SceneManager.LoadScene("ResultScene");
        }
    }

    //�`��p�̐��������
    private void CreateNum(int point, float X) {

        //��������o��
        int iDigit = ChkDigit(point);

        // �����v���n�u��ǂݍ��ށA�e�X�g�p�̃t�H���_�ƃt�@�C����
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/UI//Timer/PFB_Timer.prefab");


        //���̕������I�u�W�F�N�g�����o�^���Ă���
        for (int i = 0; i < iDigit; i++) {

            GameObject numObj = Instantiate(obj) as GameObject;

            Debug.Log(numObj);

            // �q���Ƃ��ēo�^
            numObj.transform.parent = transform;

            // ���݃`�F�b�N���Ă��錅�̐���������o��
            int digNum = GetPointDigit(point, i + 1);

            // �|�C���g���琔����؂�ւ���
            numObj.GetComponent<CTimerController>().ChangeSprite(digNum);

            // �T�C�Y���Q�b�g����
            float size_w = numObj.GetComponent<SpriteRenderer>().bounds.size.x;

            // �ʒu�����炷 0.75f�͐����̊Ԋu�̒���
            float ajs_x = (size_w * i - (size_w * iDigit) / 2) * 1.0f + X;
            Vector3 pos = new Vector3(numObj.transform.position.x - ajs_x, numObj.transform.position.y, numObj.transform.position.z);
            numObj.transform.position = pos;

            numObj = null;
        }

    }

    private void CreateColon(float X) {
        // �����v���n�u��ǂݍ��ށA�e�X�g�p�̃t�H���_�ƃt�@�C����
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/UI/Timer/PFB_Timer.prefab");

        GameObject numObj = Instantiate(obj) as GameObject;
        Debug.Log("�����");
        Debug.Log(numObj);

        // �q���Ƃ��ēo�^
        numObj.transform.parent = transform;

        // �|�C���g���琔����؂�ւ���
        numObj.GetComponent<CTimerController>().ChangeSprite(10);

        // �ʒu�����炷
        Vector3 pos = new Vector3(numObj.transform.position.x - X, numObj.transform.position.y, numObj.transform.position.z);
        numObj.transform.position = pos;

        numObj = null;
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


