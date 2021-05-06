/*==============================================================================
    Project_
    [CRotateObject.cs]
    ・回転オブジェクト制御
--------------------------------------------------------------------------------
    2021.03.24 @Author Suzuki Hayase
================================================================================
    History
        210328 Suzuki
            リファクタリング
        210404 Hirano
            回転処理書き換え
        210422 Suzuki
            セッター追加
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotateObject : MonoBehaviour {
    [SerializeField] private float fRadius = 9f;        // 回転半径
    [SerializeField] private float fSpeed = 0.15f;      // 回転速度
    [SerializeField] private float fAcceleSpeed = 0.5f; // 加速時回転速度

    [SerializeField] private OBJECT_SHAPE Shape;   // オブジェクトの形状
    [SerializeField] private RotateState State;    // オブジェクトの回転状態

    private float fDegree;                         // 角度

    private bool isAccele = false;                 // 加速フラグ
    private int iRanking;                          // 順番

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // 角度加算
        if (!isAccele) {
            fDegree += fSpeed;
        }
        else {
            fDegree += fAcceleSpeed;
        }

        Vector3 pos;

        // 位置更新
        pos.x = fRadius * Mathf.Sin((fDegree + 180) * Mathf.Deg2Rad);
        pos.y = 0.0f;
        pos.z = fRadius * Mathf.Cos((fDegree + 180) * Mathf.Deg2Rad);
        transform.position = pos;

        // 一周したら内側、外側を入れ替える処理
        // 角度が360を超えたら裏表を変えて、角度変数を0にする
        if (fDegree >= 360 && State == RotateState.OUTSIDE) {
            Set_State(RotateState.INSIDE);
        }
        else if (fDegree >= 720 && State == RotateState.INSIDE) {
            fDegree = 0.0f;
            Set_State(RotateState.OUTSIDE);
        }

        // 加速中
        if (isAccele) {
            if (CObjectManager.Instance.Get_iObjectNum() == 0) {
                CObjectManager.Instance.Add(this.gameObject);
                isAccele = false;
            }
            else {
                float deg = CObjectManager.Instance.Get_fFirstDegree();
                float goal = deg - CObjectManager.Instance.Get_fInterval() * iRanking;

                if (goal < 0) {
                    goal += 720.0f;
                }

                // 定位置まできたら停止
                if (goal - fDegree >= 0 && goal - fDegree <= fAcceleSpeed) {
                    fDegree = goal;
                    isAccele = false;
                    CObjectManager.Instance.Add(this.gameObject);
                }
            }
        }
    }

    // 回転ステート変更関数setter
    public void Set_State(RotateState newState) {
        State = newState;
        switch (State) {
            case RotateState.INSIDE:
                //内側回転用の処理
                fRadius = 3.0f;
                if (fDegree < 360.0f) {
                    fDegree += 360.0f;
                }
                break;
            case RotateState.OUTSIDE:
                //外側回転用処理
                fRadius = 9.0f;
                if (fDegree > 360.0f) {
                    fDegree -= 360.0f;
                }
                break;
            default: break;
        }
    }

    // 角度getter
    public float Get_fDegree() {
        return fDegree;
    }

    // 回転ステートgetter
    public RotateState Get_RotateState() {
        return State;
    }

    // オブジェクトの形状getter
    public OBJECT_SHAPE Get_Shape() {
        return Shape;
    }

    public bool Get_isAccele() {
        return isAccele;
    }

    // スピードsetter
    public void Set_fSpeed(float s) {
        fSpeed = s;
    }

    // 加速スピードsetter
    public void Set_fAcceleSpeed(float s) {
        fAcceleSpeed = s;
    }

    // 初期位置setter
    public void Set_fDegree(float d) {
        fDegree = d;
    }

    // 形状setter
    public void Set_Shape(OBJECT_SHAPE s) {
        Shape = s;
    }

    // 加速フラグsetter
    public void Set_isAccele(bool b) {
        isAccele = b;
    }

    // 順番setter
    public void Set_iRanking(int r) {
        iRanking = r;
    }
}