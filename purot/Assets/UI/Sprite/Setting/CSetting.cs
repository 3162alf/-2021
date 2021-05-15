using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class CSetting : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer = default;
    [SerializeField] GameObject setting_buttonObj;

    [SerializeField] GameObject vol1 = default;
    [SerializeField] GameObject vol2 = default;
    [SerializeField] GameObject vol3 = default;
    [SerializeField] GameObject vol4 = default;

    [SerializeField] public static int NowVol = 2;
    private bool bPanelOn;

    // Start is called before the first frame update
    void Start()
    {
        bPanelOn = false;

        vol1 = GameObject.Find("vol1");
        vol2 = GameObject.Find("vol2");
        vol3 = GameObject.Find("vol3");
        vol4 = GameObject.Find("vol4");

        setting_buttonObj = GameObject.Find("PFB_SettingButton");
        setting_buttonObj.SetActive(false);

        if (NowVol == 1)
        {
            vol1.SetActive(true);
            vol2.SetActive(false);
            vol3.SetActive(false);
            vol4.SetActive(false);
        }
        else if (NowVol == 2)
        {
            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(false);
            vol4.SetActive(false);
        }
        else if (NowVol == 3)
        {
            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(true);
            vol4.SetActive(false);
        }
        else if (NowVol == 4)
        {
            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(true);
            vol4.SetActive(true);
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
        if (NowVol == 1)
        {
            Debug.Log("���ʂQ��");
            audioMixer.SetFloat("SE", 0.0f);
            audioMixer.SetFloat("BGM", 0.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(false);

            NowVol++;
        }
        else if (NowVol == 2)
        {
            Debug.Log("���ʂR��");
            audioMixer.SetFloat("SE", 15.0f);
            audioMixer.SetFloat("BGM", 15.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(true);

            NowVol++;
        }
        else if (NowVol == 3)
        {
            Debug.Log("���ʂS��");
            audioMixer.SetFloat("SE", 30.0f);
            audioMixer.SetFloat("BGM", 30.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(true);
            vol4.SetActive(true);

            NowVol++;
        }
    }
    // ����DOWN
    public void OnVolDown()
    {
        if (NowVol == 2)
        {
            Debug.Log("���ʂP��");
            audioMixer.SetFloat("SE", -15.0f);
            audioMixer.SetFloat("BGM", -15.0f);
            vol1.SetActive(true);
            vol2.SetActive(false);
            vol3.SetActive(false);

            NowVol--;
        }
        else if (NowVol == 3)
        {
            Debug.Log("���ʂQ��");
            audioMixer.SetFloat("SE", 0.0f);
            audioMixer.SetFloat("BGM", 0.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(false);

            NowVol--;
        }
        else if (NowVol == 4)
        {
            Debug.Log("���ʂR��");
            audioMixer.SetFloat("SE", 15.0f);
            audioMixer.SetFloat("BGM", 15.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(true);

            NowVol--;
        }
    }

    //�ݒ�{�^���̃p�l���̕\���I�t
    public void OnDisappearSettingButton()
    {
        setting_buttonObj.SetActive(false);
    }
    //�ݒ�{�^���̃p�l���̕\���I��
    public void OnClickSettingButton()
    {
        setting_buttonObj.SetActive(true);
    }
}
