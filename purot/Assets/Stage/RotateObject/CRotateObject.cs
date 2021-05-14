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
    [SerializeField] private float fRadius;        // 回転半径
    [SerializeField] private float fSpeed;         // 回転速度

    [SerializeField] private OBJECT_SHAPE Shape;   // オブジェクトの形状
    [SerializeField] private RotateState State;    // オブジェクトの回転状態

    private float fDegree;                         // 角度

    private bool isAccele = false;                 // 加速フラグ

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float s = 180 * fSpeed / Mathf.PI / fRadius;

        // 角度加算
        fDegree += s;

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

        if (Shape == COrderManager.Instance.Get_Order(0)) {
            CObjectManager.Instance.Add(this.gameObject);
            fSpeed = CObjectManager.Instance.Get_fSpeed();
            isAccele = false;
        }
        //if (fSpeed > CObjectManager.Instance.Get_fSpeed()) {
        //    if (CObjectManager.Instance.Get_iObjectNum() == 0) {
        //        CObjectManager.Instance.Add(this.gameObject);
        //        fSpeed = CObjectManager.Instance.Get_fSpeed();
        //        isAccele = false;
        //    }
        //}

        // 加速中
        if (isAccele) {
            List<GameObject> list = CObjectManager.Instance.Get_gObjectList();
            GameObject last = list[list.Count - 1];
            float deg = last.GetComponent<CRotateObject>().Get_fDegree();

            float diff = deg - fDegree;
            if (diff < 0) {
                diff += 720;
            }

            float len = diff / 360 * 2 * Mathf.PI * fRadius;

            if (len < 3.0f) {
                isAccele = false;
                fSpeed = CObjectManager.Instance.Get_fSpeed();
                CObjectManager.Instance.Add(this.gameObject);
                CObjectManager.Instance.AcceleRemove(this.gameObject);
            }
        }
        else {
            int ranking = CObjectManager.Instance.Get_iRanking(this.gameObject);
            if (ranking > 0) {
                GameObject obj = CObjectManager.Instance.Get_gObject(ranking - 1);
                if (State == obj.GetComponent<CRotateObject>().Get_RotateState()) {
                    float deg = obj.GetComponent<CRotateObject>().Get_fDegree();

                    float diff = deg - fDegree;
                    if (diff < 0) {
                        diff += 720;
                    }

                    if (diff <= 3.0f * 360 / 2 / Mathf.PI / fRadius)
                        fSpeed = CObjectManager.Instance.Get_fSpeed();

                    fDegree = deg - 3.0f * 360 / 2 / Mathf.PI / fRadius;
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
                fRadius = 5.0f;
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
}