/*==============================================================================
    Project_HOGE
    [CRotate.cs]
    ・回転スクリプト
--------------------------------------------------------------------------------
    2021.07.12 @Author Suzuki Hayase
================================================================================
    History
        
        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.1f, 0.5f, 0.3f) * 2);
    }
}
