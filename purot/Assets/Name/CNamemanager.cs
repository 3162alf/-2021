using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CNameManager : MonoBehaviour{
    
    public int[] iSavename = new int[3];
    public string sName = null;
    public int i = 0;

    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameA = "Xbox_A";    // �z�[���{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameB = "Xbox_B";    // B�{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameLB = "Xbox_LB";    // LB�{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameRB = "Xbox_RB";    // RB�{�^��

    public bool bIsEnd;
    public bool bIsUse;

    [SerializeField] GameObject gPanel;

    private CScoreManager csmScript;

    // CScoreManager��string�^�ɂ�����ԂŖ��O�������n���̂Ɏg�p
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

        // ���O���͂�����I�������疳�ʂ�Update�̏����������Ȃ�Ȃ��悤��return����B
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
