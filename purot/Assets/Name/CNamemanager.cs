using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CNameManager : MonoBehaviour{
    
    public int[] iSavename = new int[3];
    public string sName = null;
    public int i = 0;

    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameA = "Xbox_A";    // ホームボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameB = "Xbox_B";    // Bボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameLB = "Xbox_LB";    // LBボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameRB = "Xbox_RB";    // RBボタン

    public bool bIsEnd;
    public bool bIsUse;

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

        bIsEnd = false;
        bIsUse = true;

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
        {
            gPanel.SetActive(true);
        }


        csmScript = GameObject.Find("ScoreDisplay").GetComponent<CScoreManager>();
    }
    
    void Update(){

        // 名前入力を決定終了したら無駄にUpdateの処理をおこなわないようにreturnする。
        if (bIsEnd)
        {
            if (bIsUse)
            {      
                bIsUse = false;
                gPanel.SetActive(false);
            }            
            return;
        }
        if (bIsUse && !bIsEnd)
        {

            if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown(stButtonNameRB))
            {

                iSavename[i] += 1;

                if (iSavename[i] > 69)
                {

                    iSavename[i] = 0;

                }

            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown(stButtonNameLB))
            {

                iSavename[i] -= 1;

                if (iSavename[i] < 0)
                {

                    iSavename[i] = 69;

                }

            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown(stButtonNameB))
            {

                i += 1;

            }
        }

        if(Input.GetButtonDown(stButtonNameA) || Input.GetKeyDown(KeyCode.Z))
        {
            bIsEnd = true;
            csmScript.Set_bIs(false);
        }
    }
}
