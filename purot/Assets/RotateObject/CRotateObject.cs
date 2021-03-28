/*==============================================================================
    Priject_
    [CRotateObject.cs]
    ・回転オブジェクト制御
--------------------------------------------------------------------------------
    2021.03.24 @Author Hirano Tomoki
================================================================================
    History
        210328 Suzuki Hayase
            リファクタリング
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotateObject : MonoBehaviour {
    public enum OBJECT_SHAPE {                     // オブジェクトの形状
        CUBE,                                      // キューブ
        SPHERE,                                    // 球
        CYLINDER                                   // 円柱
    }

    [SerializeField] private float fRadius;        // 回転半径
    [SerializeField] private float fSpeed;         // 回転速度
    [SerializeField] private float fStartDegree;   // 初期角度
    
    [SerializeField] private OBJECT_SHAPE Shape;   // オブジェクトの形状

    private float fDegree;                         // 角度

    // Start is called before the first frame update
    void Start() {
        fDegree = fStartDegree;

        // 位置更新
        Vector3 pos;
        pos.x = fRadius * Mathf.Sin(fDegree * Mathf.Deg2Rad);
        pos.y = 0.0f;
        pos.z = fRadius * Mathf.Cos(fDegree * Mathf.Deg2Rad);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update() {
        // 角度加算
        fDegree += fSpeed;

        // 位置更新
        Vector3 pos;
        pos.x = fRadius * Mathf.Sin(fDegree * Mathf.Deg2Rad);
        pos.y = 0.0f;
        pos.z = fRadius * Mathf.Cos(fDegree * Mathf.Deg2Rad);
        transform.position = pos;
    }

    void OnTriggerEnter(Collider col) {
        // 内回り
        if (col.gameObject.tag == "switch1") {
            fRadius = 3.0f;

            // 角度加算
            fDegree += 15;
            // 位置更新
            Vector3 pos;
            pos.x = fRadius * Mathf.Sin(fDegree * Mathf.Deg2Rad);
            pos.y = 0.0f;
            pos.z = fRadius * Mathf.Cos(fDegree * Mathf.Deg2Rad);
            transform.position = pos;
        }
        // 外回り
        if (col.gameObject.tag == "switch2") {
            fRadius = 9.0f;

            // 角度加算
            fDegree += 15;
            // 位置更新
            Vector3 pos;
            pos.x = fRadius * Mathf.Sin(fDegree * Mathf.Deg2Rad);
            pos.y = 0.0f;
            pos.z = fRadius * Mathf.Cos(fDegree * Mathf.Deg2Rad);
            transform.position = pos;
        }
    }

    // オブジェクトの形状getter
    public OBJECT_SHAPE Get_Shape() {
        return Shape;
    }
}



