using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSetting : MonoBehaviour
{
    /*
    [SerializeField] AudioMixer audioMixer = default;

    [SerializeField] GameObject setting_buttonObj;

    private string scene_name;

    [SerializeField] GameObject vol1 = default;
    [SerializeField] GameObject vol2 = default;
    [SerializeField] GameObject vol3 = default;
    [SerializeField] GameObject vol4 = default;

    [SerializeField] public static int NowVol = 2;


    // Start is called before the first frame update
    void Start()
    {
        vol1 = GameObject.Find("vol1");
        vol2 = GameObject.Find("vol2");
        vol3 = GameObject.Find("vol3");
        vol4 = GameObject.Find("vol4");

        scene_name = script.GetCurrentSceneName();

        if (scene_name != "SELECT STAGE")
        {
            setting_buttonObj = GameObject.Find("設定画面");
            setting_buttonObj.SetActive(false);
        }

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

    // Update is called once per frame
    void Update()
    {
        if (volSettingObj.activeSelf)
        {
            if (Vol != prevVol)
            {
                Vol = slider.value;
                audioMixer.SetFloat("volMASTER", Vol);
                prevVol = Vol;
            }
        }
    }

    //設定ボタンのパネルの表示オフ
    public void OnDisappearSettingButton()
    {
        volSettingObj.SetActive(false);
    }
    //設定ボタンのパネルの表示オン
    public void OnClickSettingButton()
    {
        volSettingObj.SetActive(true);
    }
    */
}
