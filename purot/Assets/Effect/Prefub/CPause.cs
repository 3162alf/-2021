using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CPause : MonoBehaviour
{
    private GameObject gCanvas; // �p�l���̐e�ɂ������L�����o�X

    [SerializeField] private GameObject gPFB_PausePanel;
    private GameObject gPausePanelInstance;

    [SerializeField, TooltipAttribute("�z�[���{�^���̓o�^��")]
    private string stButtonName = "Xbox_Home";    // HOME�{�^��

    void Start()
    {
        gCanvas = GameObject.Find("PFB_Canvas");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(stButtonName) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (gPausePanelInstance == null)
            {
                Quaternion rot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                gPausePanelInstance = (GameObject)Instantiate(gPFB_PausePanel, new Vector3(500.0f, 150.0f, 0.0f), rot);
                gPausePanelInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(3000.0f, 2000.0f);
                gPausePanelInstance.transform.SetParent(gCanvas.transform);
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(gPausePanelInstance);
                Time.timeScale = 1f;
            }
        }
    }
}