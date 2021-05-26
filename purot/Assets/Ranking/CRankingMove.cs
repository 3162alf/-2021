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
    
    private GameObject[] RankObject = new GameObject[10];
    private GameObject[] NameObject = new GameObject[10];
    private GameObject[] ScoreObject = new GameObject[10];
    
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
                RankObject[i] = GameObject.Find("top" + (i + 1));
                RankObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.3f, 0.3f);
                ScoreObject[i] = GameObject.Find("score" + (i + 1));
                ScoreObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);
                // 名前後で追加
            }
            else
            {
                RankObject[i] = GameObject.Find("top" + (i + 1));
                RankObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.225f, 0.225f);
                ScoreObject[i] = GameObject.Find("score" + (i + 1));
                ScoreObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.1f, 0.1f);
                // 名前後で追加
            }
        }
    }

    void SetPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 3)
            {
                RankObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(-15.5f, 11.6f + i * -7.0f, -2.0f);
                ScoreObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(25.0f, 42.0f + i * -7.0f, -2.0f);
            }
            else
            {
                RankObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(-15.5f, 11.6f + i * -7.0f, -2.0f);
                ScoreObject[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(25.0f, 42.0f + i * -7.0f, 2.0f);
            }
        }
    }
}
