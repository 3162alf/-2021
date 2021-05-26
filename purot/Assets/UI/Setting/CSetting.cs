using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CSetting : MonoBehaviour
{
    /* ���ʐݒ�֘A */
    [SerializeField] AudioMixer mAudioMixer = default;

    [SerializeField] GameObject gVol_1 = default;   // ���ʂ�\���Ă���摜���i�[�i1�j
    [SerializeField] GameObject gVol_2 = default;   // ���ʂ�\���Ă���摜���i�[�i2�j
    [SerializeField] GameObject gVol_3 = default;   // ���ʂ�\���Ă���摜���i�[�i3�j
    [SerializeField] GameObject gVol_4 = default;   // ���ʂ�\���Ă���摜���i�[�i4�j

    [SerializeField] public static int iNowVol = 2; // ����Volume

    /* �ݒ�p�l���\���E��\���֘A */
    [SerializeField] GameObject gSettingObj;        // �ݒ�p�l�����̂��i�[
    private bool bPanelOn;                          // �p�l���̕\���E��\���̃t���O

    void Start()
    {
        bPanelOn = false;
        gSettingObj = GameObject.Find("PFB_SettingButton");
        gSettingObj.SetActive(false);

        if (iNowVol == 1)
        {
            gVol_1.SetActive(true);
            gVol_2.SetActive(false);
            gVol_3.SetActive(false);
            gVol_4.SetActive(false);
        }
        else if (iNowVol == 2)
        {
            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(false);
            gVol_4.SetActive(false);
        }
        else if (iNowVol == 3)
        {
            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(true);
            gVol_4.SetActive(false);
        }
        else if (iNowVol == 4)
        {
            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(true);
            gVol_4.SetActive(true);
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Q) && !bPanelOn)
        {
            OnClickSettingButton();
        }
        else if (Input.GetKey(KeyCode.Q) && bPanelOn)
        {
            OnDisappearSettingButton();
        }

    }
    // ����UP
    public void OnVolUp()
    {
        if (iNowVol == 1)
        {
            Debug.Log("���ʂQ��");
            mAudioMixer.SetFloat("SE", 0.0f);
            mAudioMixer.SetFloat("BGM", 0.0f);

            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(false);
            gVol_4.SetActive(false);

            iNowVol++;
        }
        else if (iNowVol == 2)
        {
            Debug.Log("���ʂR��");
            mAudioMixer.SetFloat("SE", 15.0f);
            mAudioMixer.SetFloat("BGM", 15.0f);

            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(true);
            gVol_4.SetActive(false);

            iNowVol++;
        }
        else if (iNowVol == 3)
        {
            Debug.Log("���ʂS��");
            mAudioMixer.SetFloat("SE", 30.0f);
            mAudioMixer.SetFloat("BGM", 30.0f);

            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(true);
            gVol_4.SetActive(true);

            iNowVol++;
        }
    }
    // ����DOWN
    public void OnVolDown()
    {
        if (iNowVol == 2)
        {
            Debug.Log("���ʂP��");
            mAudioMixer.SetFloat("SE", -15.0f);
            mAudioMixer.SetFloat("BGM", -15.0f);
            gVol_1.SetActive(true);
            gVol_2.SetActive(false);
            gVol_3.SetActive(false);
            gVol_4.SetActive(false);

            iNowVol--;
        }
        else if (iNowVol == 3)
        {
            Debug.Log("���ʂQ��");
            mAudioMixer.SetFloat("SE", 0.0f);
            mAudioMixer.SetFloat("BGM", 0.0f);

            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(false);
            gVol_4.SetActive(false);

            iNowVol--;
        }
        else if (iNowVol == 4)
        {
            Debug.Log("���ʂR��");
            mAudioMixer.SetFloat("SE", 15.0f);
            mAudioMixer.SetFloat("BGM", 15.0f);

            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(true);
            gVol_4.SetActive(false);

            iNowVol--;
        }
    }

    //�ݒ�{�^���̃p�l���̕\���I�t
    public void OnDisappearSettingButton()
    {
        gSettingObj.SetActive(false);
    }
    //�ݒ�{�^���̃p�l���̕\���I��
    public void OnClickSettingButton()
    {
        gSettingObj.SetActive(true);
    }
}
