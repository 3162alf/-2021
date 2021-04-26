/*==============================================================================
    Project_HOGE
    [CObjectManager.cs]
    ・オブジェクト管理
--------------------------------------------------------------------------------
    2021.04.22 @Author Suzuki Hayase
================================================================================
    History

        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回転状態の列挙型
public enum RotateState {
    INSIDE,
    OUTSIDE
}

public enum OBJECT_SHAPE {                     // オブジェクトの形状
    CUBE = 0,                                  // キューブ
    SPHERE = 1,                                // 球
    CYLINDER = 2                               // 円柱
}

public class CObjectManager : MonoBehaviour {
    private List<GameObject> gObjectList = new List<GameObject>(); // オブジェクトリスト

    [SerializeField] private GameObject[] gObjectSource;  // オブジェクトソース
    [SerializeField] int iObjectNum;                      // オブジェクト数
    [SerializeField] float fStartRad;                     // 配置スタート角度
    [SerializeField] float fInterval;                     // 配置間隔
    [SerializeField] float fSpeed;                        // 回転スピード

    // Start is called before the first frame update
    void Start() {
        // オブジェクトをランダムな順序で配置
        List<int> num = new List<int>();
        for(int i = 0; i < iObjectNum; i++) {
            num.Add(i);
        }

        for (int i = 0; i < iObjectNum; i++) {
            int rand = Random.Range(0, num.Count);
            Create(fStartRad + i * fInterval, (OBJECT_SHAPE)num[rand], RotateState.OUTSIDE);
            num.RemoveAt(rand);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    // オブジェクト生成関数(引数:初期位置、形状、内か外か)
    public void Create(float srad, OBJECT_SHAPE shape, RotateState rs) {
        float radius = (rs == RotateState.INSIDE ? 3.0f : 9.0f);

        // 初期位置
        Vector3 pos = new Vector3(radius * Mathf.Sin(srad * Mathf.Deg2Rad),
                                  0.0f,
                                  radius * Mathf.Cos(srad * Mathf.Deg2Rad));


        // 生成
        GameObject tmp = Instantiate(gObjectSource[(int)shape], pos,
            Quaternion.Euler(0, 0, 0));

        // パラメータセット
        CRotateObject cs = tmp.GetComponent<CRotateObject>();
        cs.Set_fSpeed(fSpeed);
        cs.Set_fStartDegree(srad);
        cs.Set_Shape(shape);

        gObjectList.Add(tmp);
    }

    // 全オブジェクトのスピード変更関数
    public void Set_fSpeed(float s) {
        for(int i = 0; i < gObjectList.Count; i++) {
            CRotateObject cs = gObjectList[i].GetComponent<CRotateObject>();
            cs.Set_fSpeed(s);
        }
    }

    // オブジェクトリストgetter
    public List<GameObject> Get_gObjectList() {
        return gObjectList;
    }
}
