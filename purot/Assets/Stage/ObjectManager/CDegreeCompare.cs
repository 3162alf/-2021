/*==============================================================================
    Project_HOGE
    [CDegreeCompare.cs]
    ・角度比較クラス
--------------------------------------------------------------------------------
    2021.04.29 @Author Suzuki Hayase
================================================================================
    History

        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDegreeCompare : IComparer<GameObject> {

    public int Compare(GameObject a, GameObject b) {
        // オブジェクトA,Bの角度取得
        float deg_a = a.GetComponent<CRotateObject>().Get_fDegree();
        float deg_b = b.GetComponent<CRotateObject>().Get_fDegree();

        // 角度が同じ
        if(deg_a == deg_b) {
            return 0;
        }


        GameObject g = CObjectManager.Instance.Get_gObject(0);

        float deg_s = 0;
        if (g != null) {
            // オブジェクトリストの先頭のオブジェクトの角度取得
            deg_s = g.GetComponent<CRotateObject>().Get_fDegree();
        }

        // 先頭オブジェクトの差
        float dis_a = deg_a - deg_s;
        float dis_b = deg_b - deg_s;

        // 比較
        if(dis_a * dis_b < 0) {
            if(dis_a < dis_b) {
                return -1;
            }
            else {
                return 1;
            }
        }
        else {
            if (dis_a > dis_b) {
                return -1;
            }
            else {
                return 1;
            }
        }
    }
}
