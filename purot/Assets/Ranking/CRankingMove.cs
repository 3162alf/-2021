/*==============================================================================
     Project
    [CRrankingMove.cs]
    ・ランキング表示の演出処理。
--------------------------------------------------------------------------------
    2021.05.15 Tamura Yusuke
==============================================================================
    History    
        
============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CRankingMove : MonoBehaviour
{

    [SerializeField] private GameObject[] gRankObjects = new GameObject[10];
    [SerializeField] private GameObject[] gNameObject = new GameObject[10];
    [SerializeField] private GameObject[] gScoreObjects = new GameObject[10];
    //private GameObject[] gWakuObject = new GameObject[10];
    public GameObject[] gImg_WAKU = new GameObject[2];
    public GameObject[] gImg_RankingFrame = new GameObject[2];

    [SerializeField] private float fTopPosY;
    [SerializeField] private float fScorePosY;

    private CNameManager cnmScript;
    private CScoreManager csmSccript;
    private GameObject gPanel;
   

    // Start is called before the first frame update
    void Start()
    {
        //SetGameObject();
        //SetPosition();

        cnmScript = GameObject.Find("PFB_Words").GetComponent<CNameManager>();
       
        //gImg_WAKU = GameObject.Find("IMG_WAKU").gameObject;
        //Img_RankingFrame = GameObject.Find("RankingFrame").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (cnmScript.GetbIsEnd())
        {
            for (int i = 0; i < 10; i++)
            {
                gRankObjects[i].SetActive(true);
                gScoreObjects[i].SetActive(true);
                gNameObject[i].SetActive(true);
            }
            for(int i=0; i<2; i++)
            {
                gImg_WAKU[i].SetActive(true);
                gImg_RankingFrame[i].SetActive(true);
            }
        }
        
        
    }

    void SetGameObject()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 3)
            {
                gRankObjects[i] = GameObject.Find("top" + (i + 1));
                gRankObjects[i].GetComponent<RectTransform>().localScale = new Vector2(0.3f, 0.3f);
                gScoreObjects[i] = GameObject.Find("score" + (i + 1));
                gScoreObjects[i].GetComponent<RectTransform>().localScale = new Vector2(0.15f, 0.15f);
                // 名前後で追加
            }
            else
            {
                gRankObjects[i] = GameObject.Find("top" + (i + 1));
                gRankObjects[i].GetComponent<RectTransform>().localScale = new Vector2(0.225f, 0.24f);
                gScoreObjects[i] = GameObject.Find("score" + (i + 1));
                gScoreObjects[i].GetComponent<RectTransform>().localScale = new Vector2(0.135f, 0.135f);
                // 名前後で追加
            }
        }
    }

    void SetPosition()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < 3)
            {
                gRankObjects[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(-15.5f, fTopPosY + i * -7.5f, -2.0f);
                gScoreObjects[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(45.0f, fScorePosY + i * -9.6f, -2.0f);
            }
            else
            {
                if(i == 3)
                {
                    fTopPosY += -4.0f;
                    fScorePosY += -4.5f;
                }
                gRankObjects[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(-15.5f, fTopPosY + i * -5.8f, -5.0f);
                gScoreObjects[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(44.0f, fScorePosY + i * -7.0f, -2.0f);
            }
        }
    }

}
