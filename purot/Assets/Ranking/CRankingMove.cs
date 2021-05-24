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
        for(int i = 0; i < 10; i++)
        {
            RankObject[i] = GameObject.Find("top" + (i + 1));
            RankObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.2f, 0.2f);
            ScoreObject[i] = GameObject.Find("score" + (i + 1));
            ScoreObject[i].GetComponent<RectTransform>().localScale = new Vector2(0.2f, 0.2f);
            // 名前後で追加
        }
    }

    void SetPosition()
    {
        for(int i = 0; i < 10; i++)
        {
            RankObject[i].GetComponent<RectTransform>().position = new Vector3(-65.0f, 42.0f + i * -11.0f, 100.0f);
            ScoreObject[i].GetComponent<RectTransform>().position = new Vector3(-10.0f, 41.0f + i * -11.0f, 100.0f);
        }
    }
}
