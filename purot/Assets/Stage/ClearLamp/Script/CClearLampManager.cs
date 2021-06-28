/*==============================================================================
    Project_HOGE
    [CClearLampManager.cs]
    �E�N���A�����v����
--------------------------------------------------------------------------------
    2021.06.24 @Author Suzuki Hayase
================================================================================
    History
        
        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CClearLampManager : CSingletonMonoBehaviour<CClearLampManager> {
    [SerializeField] private GameObject gClearLamp;                  // �N���A�����v

    private List<GameObject> gClearLampList = new List<GameObject>();     // �N���A�����v���X�g

    [SerializeField] private float fBloomStrength;

    private bool bOff = false;               // ���C�g�I�t�t���O
    private int iOffTimer = 0;               // ���C�g�I�t�܂ł̎���

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // �|�[�Y��ʂ̎��ɃI�u�W�F�N�g����]���Ȃ��悤�ȏ���
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

    // �����v�����֐�(����:�w�ߐ�)
    public void CreateLamp(int n) {
        if (n != gClearLampList.Count) {
            // �O��̃����v��j��
            for (int i = 0; i < gClearLampList.Count; i++) {
                Destroy(gClearLampList[i]);
            }

            // ���X�g������
            gClearLampList.Clear();

            // �w�߂𗐐��Ő���
            for (int i = 0; i < n; i++) {
                float st = 0;

                if (n % 2 == 0) {
                    st = 2.5f + 5 * (n / 2.0f - 1);
                }
                else {
                    st = 5 * (n / 2);
                }

                // �N���A�����v����
                gClearLampList.Add(Instantiate(gClearLamp,
                    new Vector3(-20, 0, st - i * 5), Quaternion.Euler(0, 0, 0)));
            }
        }
    }
}
