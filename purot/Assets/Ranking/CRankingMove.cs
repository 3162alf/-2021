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


public class CRankingMove : MonoBehaviour
{
    
    private GameObject[] gRankObject = new GameObject[10];
    private GameObject[] gNameObject = new GameObject[10];
    private GameObject[] gScoreObject = new GameObject[10];
    private GameObject[] gWakuObject = new GameObject[10];

    [SerializeField] private float fTopPosY;
    [SerializeField] private float fScorePosY;

    // Start is called before the first frame update
    void Start()
    {
        SetGameObject();

        SetPosition();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetGameObject()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 3)
            {
                gRankObject[i] = GameObject.Find("top" + (i + 1));
                gRankObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.3f, 0.3f);
                gScoreObject[i] = GameObject.Find("score" + (i + 1));
                gScoreObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.15f, 0.15f);
                // 名前後で追加
            }
            else
            {
                gRankObject[i] = GameObject.Find("top" + (i + 1));
                gRankObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.225f, 0.24f);
                gScoreObject[i] = GameObject.Find("score" + (i + 1));
                gScoreObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.135f, 0.135f);
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
                gRankObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(-15.5f, fTopPosY + i * -7.5f, -2.0f);
                gScoreObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(25.0f, fScorePosY + i * -9.6f, -2.0f);
            }
            else
            {
                if(i == 3)
                {
                    fTopPosY += -4.0f;
                    fScorePosY += -3.9f;
                }
                gRankObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(-15.5f, fTopPosY + i * -5.8f, -2.0f);
                gScoreObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(24.0f, fScorePosY + i * -7.0f, -2.0f);
            }
        }
    }
}
