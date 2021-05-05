/*==============================================================================
    Project
    [CSpriteChar.cs]
    �E�X�v���C�g��������
--------------------------------------------------------------------------------
    2021.04.26 @Author Yusuke Tamura
================================================================================
    History
          
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpriteChar : MonoBehaviour
{
    public void SetChar(int idx)
    {
        // �w��̃X�v���C�g�ԍ��ŃX�v���C�g�𓮓I�ɕύX����
        string name = "number_" + idx;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Ranking/number");  // �����������摜
        Sprite sp = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(name));
        sr.sprite = sp;
    }
}
