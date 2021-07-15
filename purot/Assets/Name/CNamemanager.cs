using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CNameManager : MonoBehaviour{
    
    public int[] iSavename = new int[3];
    //private string sName = null;
    private int iMojiCount = 0;

    private GameObject canvas;

    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameA = "Xbox_A";    // ホームボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameB = "Xbox_B";    // Bボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameLB = "Xbox_LB";    // LBボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameRB = "Xbox_RB";    // RBボタン

    private bool bIsEnd;
    private bool bIsUse;

    [SerializeField] GameObject gPanel;

    private CScoreManager csmScript;

    // CScoreManagerにstring型にした状態で名前を引き渡すのに使用
    public int[] GetName(){
        return iSavename;
    }

    public void SetName(int[] iName){

        for (int j = 0; j < 3; j++){

            iSavename[j] = iName[j];
        }
    }
    void Start()
    {
        //iSavename[0] = 0;
        //iSavename[1] = 0;
        //iSavename[2] = 0;

        
        bIsEnd = false;  // 終了フラグ
        bIsUse = true;   // 使っているフラグ
        

        gPanel = GameObject.Find("NamePanel");
        /*
        if (CSceneManager.GetRecently() != "ResultScene")
        {
            gPanel.SetActive(false);
            bIsUse = false;
            bIsEnd = true;
        }
        */
       // else
       
        

        csmScript = GameObject.Find("ScoreDisplay").GetComponent<CScoreManager>();
    }
    
    void Update(){

        // 名前入力を決定終了したら無駄にUpdateの処理をおこなわないようにreturnする。
        if (bIsEnd)
        {
            if (bIsUse)
            {      
                bIsUse = false;
                //gPanel.SetActive(false);
            }            
            return;
        }
        if (bIsUse && !bIsEnd)
        {

            if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown(stButtonNameRB))
            {

                iSavename[iMojiCount] += 1;

                if (iSavename[iMojiCount] > 69)
                {

                    iSavename[iMojiCount] = 0;

                }
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown(stButtonNameLB))
            {

                iSavename[iMojiCount] -= 1;

                if (iSavename[iMojiCount] < 0)
                {

                    iSavename[iMojiCount] = 69;

                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown(stButtonNameB))
            {
                iMojiCount++;

                if (iMojiCount >= 3)
                {
                    bIsEnd = true;
                    gPanel.SetActive(false);
                }

            }
        }

    }

    public bool GetbIsEnd()
    {
        return bIsEnd;
    }
}
