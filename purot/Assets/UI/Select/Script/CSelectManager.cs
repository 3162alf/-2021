/*==============================================================================
    Priject_
    [CLampController.cs]
    ・最初に選択するボタンの設定

--------------------------------------------------------------------------------
    2021.05.11 @Author Hirano Tomoki
================================================================================
    History
        

/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSelectManager : MonoBehaviour {
    [SerializeField] private Button[] uiButton;
    [SerializeField] private int iNum;

    void Start() {
        uiButton[iNum].Select();
    }
}
