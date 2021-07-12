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
        210517 Ono
            入れ替え処理を滑らかに
        210525 Sasaki
            ポーズ画面の時にオブジェクトが回転しないような処理追加(48~51行目)
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CRotateObject : MonoBehaviour {
    [SerializeField] private float fRadius;        // 回転半径

    [SerializeField] private OBJECT_SHAPE Shape;   // オブジェクトの形状
    [SerializeField] private RotateState State;    // オブジェクトの回転状態

    [SerializeField] private float fDegree;         // 角度
    [SerializeField] private float fDegreeSub;      // 角度サブ
    [SerializeField] private float fDegreeSub2;     // 角度サブ2

    [SerializeField] private Vector3 vRotate;       // 初期姿勢

    //[SerializeField] private float fSubSpeed = 10.0f;                // サブスピード

    [SerializeField] private bool isInverse = false;                 // 角度
    [SerializeField] private bool isInverse2 = false;                 // 角度
    [SerializeField] private bool isSort = false;                    // ソートフラグ
    [SerializeField] private bool isAccele = false;                  // 加速フラグ

    //private bool isChange = false;                //入れ替え時のソートブール

    // Start is called before the first frame update
    void Start() {
        transform.eulerAngles = vRotate;
    }

    // Update is called once per frame
    public void UpdateObject() {
        // ポーズ画面の時にオブジェクトが回転しないような処理
        if (Mathf.Approximately(Time.timeScale, 0f)) {
            return;
        }

        // 現在の位置を取得
        Vector3 pos = this.gameObject.transform.position;

        // 角度のブール処理
        if ((fDegree >= 330 && fDegree <= 340) ||
            (fDegree >= 690 && fDegree <= 700)) {
            isInverse2 = true;
        }

        if (isInverse2) {
            float sp = 1.0f;
            if (isAccele)
                sp *= 10;

            fDegreeSub2 -= sp;

            if (State == RotateState.OUTSIDE) {
                fRadius -= 4 / (180.0f / sp);

                if (fDegreeSub2 <= -180) {
                    isInverse2 = false;
                    Set_State(RotateState.INSIDE);
                    fDegreeSub2 = 0;
                }
            }
            else if (State == RotateState.INSIDE) {
                fRadius += 4 / (180.0f / sp);

                if (fDegreeSub2 <= -180) {
                    isInverse2 = false;
                    Set_State(RotateState.OUTSIDE);
                    fDegreeSub2 = 0.0f;
                }
            }
        }
        if (fDegreeSub <= -360) {
            fDegreeSub += 360;
        }

        // 一周したら内側、外側を入れ替える処理
        if (isInverse) {
            fDegreeSub -= 10;

            if (State == RotateState.OUTSIDE) {
                fRadius -= 4 / (180.0f / 10);

                if (fDegreeSub <= -180) {
                    isInverse = false;
                    Set_State(RotateState.INSIDE);
                    fDegreeSub = -180.0f;
                    fDegreeSub2 = 0;
                    if (isSort) {
                        isAccele = true;
                        isSort = false;
                    }
                }
            }
            else if (State == RotateState.INSIDE) {
                fRadius += 4 / (180.0f / 10);

                if (fDegreeSub <= -360) {
                    isInverse = false;
                    Set_State(RotateState.OUTSIDE);
                    fDegreeSub = 0.0f;
                    fDegreeSub2 = 0;

                    if (isSort) {
                        isAccele = true;
                        isSort = false;
                    }
                }
            }
        }

        // 加速時
        if (isAccele) {
            if (Shape != COrderManager.Instance.Get_Order(0)) {
                // オブジェクトリスト取得
                List<GameObject> list = CObjectManager.Instance.Get_gObjectList();
                int last = list.Count - 1;

                if (last >= 0) {
                    //for (int i = 0; i < list.Count; i++) {
                    if (!list[last].GetComponent<CRotateObject>().Get_isInverse()) {
                        float deg = list[last].GetComponent<CRotateObject>().Get_fDegree();
                        float diff = deg - fDegree;
                        if (diff < 0) {
                            diff += 720;
                        }
                        //Debug.Log(Shape);
                        //Debug.Log(list[i].GetComponent<CRotateObject>().Get_Shape());
                        //Debug.Log(diff);
                        //Debug.Log(deg);
                        //Debug.Log(fDegree);
                        if (diff < 180) {
                            float len = diff / 360 * 2 * Mathf.PI * fRadius;
                            if (len <= 3.0f) {
                                if (!list[last].GetComponent<CRotateObject>().Get_isAccele()) {
                                    isAccele = false;
                                }
                                fDegree = deg - 3.0f * 360 / 2 / Mathf.PI / fRadius;
                                CObjectManager.Instance.Decelerate(this.gameObject);
                            }
                        }
                    }
                }
                //}
            }
        }

        float speed = CObjectManager.Instance.Get_fSpeed();
        if (isAccele) {
            speed = CObjectManager.Instance.Get_fAcceleSpeed();
        }

        float s = 180 * speed / Mathf.PI / fRadius;

        // 角度加算
        fDegree += s;

        float rad = (9 - 5) / 2;
        float rad2 = (9 + 5) / 2;

        // 位置更新
        pos.x = rad2 * Mathf.Sin((fDegree + 180) * Mathf.Deg2Rad)
            + rad * Mathf.Cos((fDegreeSub + fDegreeSub2) * Mathf.Deg2Rad) * Mathf.Sin((fDegree + 180) * Mathf.Deg2Rad);

        pos.y = rad * Mathf.Sin((fDegreeSub + fDegreeSub2) * Mathf.Deg2Rad);

        pos.z = rad2 * Mathf.Cos((fDegree + 180) * Mathf.Deg2Rad)
            + rad * Mathf.Cos((fDegreeSub + fDegreeSub2) * Mathf.Deg2Rad) * Mathf.Cos((fDegree + 180) * Mathf.Deg2Rad);

        transform.position = pos;

        if (isAccele) {
            if (Shape == COrderManager.Instance.Get_Order(0)) {
                CObjectManager.Instance.Decelerate(this.gameObject);
                isAccele = false;
            }
        }
        if (fDegree >= 720) {
            fDegree -= 720;
        }
        if (fDegree < 0) {
            fDegree += 720;
        }
    }


    // 回転ステート変更関数setter
    // 外側が0～360度、内側が360～720度
    public void Set_State(RotateState newState) {
        State = newState;
        switch (State) {
            case RotateState.INSIDE:
                //内側回転用の処理
                fRadius = 5.0f;
                fDegreeSub = -180;
                if (fDegree <= 360.0f) {
                    fDegree += 360.0f;
                }
                break;
            case RotateState.OUTSIDE:
                //外側回転用処理
                fRadius = 9.0f;
                fDegreeSub = 0;
                if (fDegree >= 360.0f) {
                    fDegree -= 360.0f;
                }
                break;
            default: break;
        }
    }

    public void InvDegree() {
        fDegree += 360.0f;
        if (fDegree >= 720.0f) {
            fDegree -= 720.0f;
        }
    }

    // 角度getter
    public float Get_fDegree() {
        return fDegree;
    }

    // 角度getter
    public float Get_fDegreeSub() {
        return fDegreeSub;
    }

    // 回転ステートgetter
    public RotateState Get_State() {
        return State;
    }

    // オブジェクトの形状getter
    public OBJECT_SHAPE Get_Shape() {
        return Shape;
    }

    public bool Get_isAccele() {
        return isAccele;
    }

    public bool Get_isInverse() {
        return isInverse;
    }

    public bool Get_isInverse2() {
        return isInverse2;
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

    // 加速フラグsetter
    public void Set_isSort(bool b) {
        isSort = b;
    }

    // ソートフラグsetter
    public void Set_isInverse(bool b) {
        isInverse = b;
    }
}