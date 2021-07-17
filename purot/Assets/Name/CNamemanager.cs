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

    private GameObject gPanel;

    private CScoreManager csmScript;

    // SE用
    [SerializeField] AudioClip aSE01;               // SE格納するやつ
    [SerializeField] AudioClip aSE02;               // SE格納するやつ
    private GameObject gCamera;                     // AudioSource取得用
    AudioSource aAudioSource;                       // コンポーネント取得用

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
        

        csmScript = GameObject.Find("ScoreDisplay").GetComponent<CScoreManager>();

        // カメラ(SE用)取得
        gCamera = Camera.main.gameObject;

        // コンポーネント取得
        aAudioSource = gCamera.GetComponent<AudioSource>();


    }
    
    void Update(){

        // 名前入力を決定終了したら無駄にUpdateの処理をおこなわないようにreturnする。
        if (csmScript.GetNameIn())
        {
            if (bIsUse)
            {                
                bIsUse = false;
                //gPanel.SetActive(false);
            }            
            return;
        }
        if (bIsUse && !csmScript.GetNameIn())
        {

            if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown(stButtonNameRB))
            {

                iSavename[iMojiCount] += 1;

                if (iSavename[iMojiCount] > 69)
                {

                    iSavename[iMojiCount] = 0;

                }
                aAudioSource.PlayOneShot(aSE01);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown(stButtonNameLB))
            {

                iSavename[iMojiCount] -= 1;

                if (iSavename[iMojiCount] < 0)
                {

                    iSavename[iMojiCount] = 69;

                }
                aAudioSource.PlayOneShot(aSE01);
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown(stButtonNameB))
            {
                iMojiCount++;

                if (iMojiCount >= 3)
                {
                    bIsEnd = true;
                    gPanel.SetActive(false);
                    csmScript.SetNameIn(true);
                    aAudioSource.PlayOneShot(aSE02);
                }
                aAudioSource.PlayOneShot(aSE01);
            }
        }
        if(CSceneManager.GetRecently() == "TitleScene")
        {
            bIsEnd = true;
            gPanel.SetActive(false);
            csmScript.SetNameIn(true);
            aAudioSource.PlayOneShot(aSE02);
        }

    }

    public bool GetbIsEnd()
    {
        return bIsEnd;
    }
}
