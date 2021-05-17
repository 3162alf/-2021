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
using UnityEngine.SceneManagement;


public class CRankingMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
