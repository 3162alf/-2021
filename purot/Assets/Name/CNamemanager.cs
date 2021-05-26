using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CNamemanager : MonoBehaviour{
    
    public  int[] iSavename = new int[3];
    public int i = 0;

    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameHome = "Xbox_Home";    // �z�[���{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameA = "Xbox_A";    // �z�[���{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameB = "Xbox_B";    // B�{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameLB = "Xbox_LB";    // LB�{�^��
    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonNameRB = "Xbox_RB";    // RB�{�^��
    bool bIsEnd;
    [SerializeField] bool bIsYou;   // ����̖��O�������͂��������̂ł��̃t���O��true�̏ꍇ�͓��͉\�ɂ���
    [SerializeField] private AudioClip aSE01;
    AudioSource aAudioSource;

    void Start(){

        iSavename[0] = 0;
        iSavename[1] = 0;
        iSavename[2] = 0;

        bIsEnd = false;
       // bIsYou = false;
        aAudioSource = GetComponent<AudioSource>();
    }
    
    void Update(){

        // ���O���͂�����I�������疳�ʂ�Update�̏����������Ȃ�Ȃ��悤��return����B
        if (bIsEnd)
        {
            if(bIsYou)
            {
                // �z�[���{�^������������^�C�g���ɖ߂�悤�ɑJ�ځi�����łł���悤�ɂ����������́B�B�B�j
                if (Input.GetButtonDown(stButtonNameHome))
                {
                    aAudioSource.PlayOneShot(aSE01);
                    CSceneManager.SetRecently("TitleScene");
                    SceneManager.LoadScene("TitleScene");
                }
            }
            return;
        }

        if (bIsYou && !bIsEnd)
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

        if(Input.GetButtonDown(stButtonNameA))
        {
            bIsEnd = true;
        }
    }
}
