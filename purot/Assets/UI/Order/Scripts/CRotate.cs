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

    [SerializeField] private Vector3 vRotate;
    [SerializeField] private float fSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vRotate * fSpeed);
    }
}
