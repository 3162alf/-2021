/*==============================================================================
    Priject_
    [CRotateObject.cs]
    ・回転オブジェクト制御
--------------------------------------------------------------------------------
    2021.03.24 @Author Suzuki Hayase
================================================================================
    History
        210328 Suzuki Hayase
            リファクタリング
        210404 Hirano
            回転処理書き換え
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回転状態の列挙型
public enum RotateState {
    INSIDE,
    OUTSIDE
}

public class CRotateObject : MonoBehaviour {
    public enum OBJECT_SHAPE {                     // オブジェクトの形状
        CUBE,                                      // キューブ
        SPHERE,                                    // 球
        CYLINDER                                   // 円柱
    }

    [SerializeField] private float fRadius = 9f;        // 回転半径
    [SerializeField] private float fSpeed = 0.15f;       // 回転速度
    [SerializeField] private float fStartDegree = 0f;   // 初期角度

    [SerializeField] private OBJECT_SHAPE Shape;   // オブジェクトの形状
    [SerializeField] private RotateState State;    // オブジェクトの回転状態

    private Vector3 vPos;


    private float fDegree;                         // 角度

    // Start is called before the first frame update
    void Start() {

        fDegree = fStartDegree;
        // ステートの初期状態(外側)
        Set_State(RotateState.OUTSIDE);
    }

    // Update is called once per frame
    void Update() {
        // 角度加算
        fDegree += fSpeed;

        // 位置更新
        vPos.x = fRadius * Mathf.Sin(fDegree * Mathf.Deg2Rad);
        vPos.y = 0.0f;
        vPos.z = fRadius * Mathf.Cos(fDegree * Mathf.Deg2Rad);
        transform.position = vPos;

        // 一周したら内側、外側を入れ替える処理
        // 角度が360を超えたら裏表を変えて、角度変数を0にする
        if (fDegree >= 360 && State == RotateState.OUTSIDE) {
            Set_State(RotateState.INSIDE);
            fDegree = 0;
        }else if(fDegree >= 360 && State == RotateState.INSIDE) {
            Set_State(RotateState.OUTSIDE);
            fDegree = 0;
        }
    }

    // 回転ステート変更関数setter
    public void Set_State(RotateState newState) {
        State = newState;
        switch (State) {
            case RotateState.INSIDE:
                //内側回転用の処理
                fRadius = 3.0f;

                break;
            case RotateState.OUTSIDE:
                //外側回転用処理
                fRadius = 9.0f;

                break;
            default: break;
        }
    }

    // 回転ステートgetter
    public RotateState Get_RotateState() {
        return State;
    }

    // オブジェクトの形状getter
    public OBJECT_SHAPE Get_Shape() {
        return Shape;
    }
}