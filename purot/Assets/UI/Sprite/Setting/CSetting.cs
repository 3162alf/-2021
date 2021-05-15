using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CSetting : MonoBehaviour
{
    /* 音量設定関連 */
    [SerializeField] AudioMixer mAudioMixer = default;

    [SerializeField] GameObject gVol_1 = default;   // 音量を表している画像を格納（1）
    [SerializeField] GameObject gVol_2 = default;   // 音量を表している画像を格納（2）
    [SerializeField] GameObject gVol_3 = default;   // 音量を表している画像を格納（3）
    [SerializeField] GameObject gVol_4 = default;   // 音量を表している画像を格納（4）

    [SerializeField] public static int iNowVol = 2; // 初期Volume

    /* 設定パネル表示・非表示関連 */
    [SerializeField] GameObject gSettingObj;        // 設定パネル自体を格納
    private bool bPanelOn;                          // パネルの表示・非表示のフラグ

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
    // 音量UP
    public void OnVolUp()
    {
        if (iNowVol == 1)
        {
            Debug.Log("音量２に");
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
            Debug.Log("音量３に");
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
            Debug.Log("音量４に");
            mAudioMixer.SetFloat("SE", 30.0f);
            mAudioMixer.SetFloat("BGM", 30.0f);

            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(true);
            gVol_4.SetActive(true);

            iNowVol++;
        }
    }
    // 音量DOWN
    public void OnVolDown()
    {
        if (iNowVol == 2)
        {
            Debug.Log("音量１に");
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
            Debug.Log("音量２に");
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
            Debug.Log("音量３に");
            mAudioMixer.SetFloat("SE", 15.0f);
            mAudioMixer.SetFloat("BGM", 15.0f);

            gVol_1.SetActive(true);
            gVol_2.SetActive(true);
            gVol_3.SetActive(true);
            gVol_4.SetActive(false);

            iNowVol--;
        }
    }

    //設定ボタンのパネルの表示オフ
    public void OnDisappearSettingButton()
    {
        gSettingObj.SetActive(false);
    }
    //設定ボタンのパネルの表示オン
    public void OnClickSettingButton()
    {
        gSettingObj.SetActive(true);
    }
}
