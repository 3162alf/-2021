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

    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameA = "Xbox_A";    // �z�[���{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameB = "Xbox_B";    // B�{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameLB = "Xbox_LB";    // LB�{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameRB = "Xbox_RB";    // RB�{�^��

    private bool bIsEnd;
    private bool bIsUse;

    private GameObject gPanel;

    private CScoreManager csmScript;

    // SE�p
    [SerializeField] AudioClip aSE01;               // SE�i�[������
    [SerializeField] AudioClip aSE02;               // SE�i�[������
    private GameObject gCamera;                     // AudioSource�擾�p
    AudioSource aAudioSource;                       // �R���|�[�l���g�擾�p

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

        
        bIsEnd = false;  // �I���t���O
        bIsUse = true;   // �g���Ă���t���O
        

        gPanel = GameObject.Find("NamePanel");
        

        csmScript = GameObject.Find("ScoreDisplay").GetComponent<CScoreManager>();

        // �J����(SE�p)�擾
        gCamera = Camera.main.gameObject;

        // �R���|�[�l���g�擾
        aAudioSource = gCamera.GetComponent<AudioSource>();


    }
    
    void Update(){

        // ���O���͂�����I�������疳�ʂ�Update�̏����������Ȃ�Ȃ��悤��return����B
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
