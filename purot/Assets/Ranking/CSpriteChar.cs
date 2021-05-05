/*==============================================================================
    Project
    [CSpriteChar.cs]
    ・スプライト文字生成
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
        // 指定のスプライト番号でスプライトを動的に変更する
        string name = "number_" + idx;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Ranking/number");  // 生成したい画像
        Sprite sp = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(name));
        sr.sprite = sp;
    }
}
