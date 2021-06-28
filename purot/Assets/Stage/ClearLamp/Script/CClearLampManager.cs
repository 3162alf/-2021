/*==============================================================================
    Project_HOGE
    [CClearLampManager.cs]
    ・クリアランプ制御
--------------------------------------------------------------------------------
    2021.06.24 @Author Suzuki Hayase
================================================================================
    History
        
        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CClearLampManager : CSingletonMonoBehaviour<CClearLampManager> {
    [SerializeField] private GameObject gClearLamp;                  // クリアランプ

    private List<GameObject> gClearLampList = new List<GameObject>();     // クリアランプリスト

    [SerializeField] private float fBloomStrength;

    private bool bOff = false;               // ライトオフフラグ
    private int iOffTimer = 0;               // ライトオフまでの時間

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // ポーズ画面の時にオブジェクトが回転しないような処理
        if (Mathf.Approximately(Time.timeScale, 0f)) {
            return;
        }

        if (bOff) {
            iOffTimer++;
            if(iOffTimer > 60) {
                for(int i = 0; i < gClearLampList.Count; i++) {
                    gClearLampList[i].transform.Find("PFB_Lamp").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1, 1, 1));
                    gClearLampList[i].transform.Find("PFB_Lamp").gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
                iOffTimer = 0;
                bOff = false;
            }
        }
    }

    public void Lighting(int i, Color color) {
        gClearLampList[i].transform.Find("PFB_Lamp").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color * fBloomStrength);
        gClearLampList[i].transform.Find("PFB_Lamp").gameObject.GetComponent<Renderer>().material.color = color;
    }

    public void LightingOff() {
        bOff = true;
    }

    // ランプ生成関数(引数:指令数)
    public void CreateLamp(int n) {
        if (n != gClearLampList.Count) {
            // 前回のランプを破棄
            for (int i = 0; i < gClearLampList.Count; i++) {
                Destroy(gClearLampList[i]);
            }

            // リスト初期化
            gClearLampList.Clear();

            // 指令を乱数で生成
            for (int i = 0; i < n; i++) {
                float st = 0;

                if (n % 2 == 0) {
                    st = 2.5f + 5 * (n / 2.0f - 1);
                }
                else {
                    st = 5 * (n / 2);
                }

                // クリアランプ生成
                gClearLampList.Add(Instantiate(gClearLamp,
                    new Vector3(-20, 0, st - i * 5), Quaternion.Euler(0, 0, 0)));
            }
        }
    }
}
