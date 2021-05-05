/*==============================================================================
    Project
    [CSpriteFont.cs]
    �E�X�v���C�g�����̃t�H���g����
--------------------------------------------------------------------------------
    2021.04.26 @Author Yusuke Tamura
================================================================================
    History
          
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpriteFont : MonoBehaviour
{
    private static string _decode = "0123456789"; // �ϊ��p������e�[�u��
    const float FONT_SIZE = 100.0f; // �t�H���g�T�C�Y�B�t�H���g�̃X�P�[����J�����̐ݒ�ɂ���ėv����

    // Use this for initialization
    void Start()
    {
        // �e�X�g�p
        SetText("5813497620");
    }

    // �w��̕����ŃX�v���C�g�t�H���g���쐬����
    public void SetText(string text)
    {
        int i = 0;

        foreach (char c in text)
        {
            GameObject obj = null;
            if (i < transform.childCount)
            {
                // �쐬�ς݂ł���΂�����g��
                obj = transform.GetChild(i).gameObject;
            }
            else
            {
                // SpriteChar���v���n�u����擾
                GameObject prefab = Resources.Load("Ranking/Prefabs/SpriteChar") as GameObject;
                Vector3 pos = new Vector3(i * transform.localScale.x * FONT_SIZE, 0, 0);
                obj = Instantiate(prefab) as GameObject;
                // �q�ɐݒ肷��
                obj.transform.parent = transform;
                obj.transform.localPosition = pos;
                obj.transform.localScale = new Vector3(1, 1, 1);
            }

            // ������Ή�����X�v���C�g�ԍ��ɕϊ�����
            int idx = _decode.IndexOf(c);

            // SpriteChar���擾���ăX�v���C�g��ύX����
            CSpriteChar sc = obj.GetComponent<CSpriteChar>();
            sc.SetChar(idx);
            i++;
        }
    }
}
