/*==============================================================================
    Project_HOGE
    [CObjectManager.cs]
    ・オブジェクト管理
--------------------------------------------------------------------------------
    2021.04.22 @Author Suzuki Hayase
================================================================================
    History
        210426 Suzuki
            オブジェクトが隙間を詰めるように
        
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
    CUBE,
    SPHERE,
    TORUS,
    RAMIEL,
    STELLA,
    HOURGLASS,                
    SATURN,
    ATOM,
    MAX
}

public class CObjectManager : CSingletonMonoBehaviour<CObjectManager> {
    private List<GameObject> gObjectList = new List<GameObject>();     // オブジェクトリスト
    private List<OBJECT_SHAPE> CreateList = new List<OBJECT_SHAPE>();  // 生成リスト
    private List<GameObject> gAcceleList = new List<GameObject>();     // 加速中のオブジェクトリスト

    [SerializeField] private GameObject[] gObjectSource;          // オブジェクトソース
    [SerializeField] private float fFirstDegree;                  // 先頭の角度
    [SerializeField] private float fInterval;                     // 配置間隔
    [SerializeField] private float fSpeed;                        // 回転スピード
    [SerializeField] private float fAcceleSpeed;                  // 加速時回転スピード
    [SerializeField] private float fInRadius;                     // 内回り半径
    [SerializeField] private float fOutRadius;                    // 外回り半径

    private int iTimer = 0;                                       // 生成間隔タイマー

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // リストにあるオブジェクトを生成
        if (CreateList.Count > 0) {
            // 一定の間隔を開けて生成する
            if (iTimer >= fInterval / fAcceleSpeed) {
                // 重ならないように生成位置を調整
                float sd = 180.0f;
                float rad = 9.0f;
                RotateState rs = RotateState.OUTSIDE;
                if (gObjectList.Count > 0) {
                    float ld = gObjectList[gObjectList.Count - 1].GetComponent<CRotateObject>().Get_fDegree();

                    if (ld >= 360 && ld < 720 + fInterval) {
                        sd = 180;
                        rad = fOutRadius;
                        rs = RotateState.OUTSIDE;
                    }
                    else {
                        sd = 540;
                        rad = fInRadius;
                        rs = RotateState.INSIDE;
                    }
                }
                Vector3 pos = new Vector3(rad * Mathf.Sin((sd + 180) * Mathf.Deg2Rad),
                                          0.0f,
                                          rad * Mathf.Cos((sd + 180) * Mathf.Deg2Rad));

                // 生成
                GameObject tmp = Instantiate(gObjectSource[(int)CreateList[0]], pos,
                    Quaternion.Euler(0, 0, 0));

                // パラメータセット
                CRotateObject cs = tmp.GetComponent<CRotateObject>();
                cs.Set_fSpeed(fSpeed);
                cs.Set_fAcceleSpeed(fAcceleSpeed);
                cs.Set_fDegree(sd);
                cs.Set_Shape(CreateList[0]);
                cs.Set_State(rs);

                Accele(tmp);
                CreateList.RemoveAt(0);

                iTimer = 0;
            }
            iTimer++;
        }
        else {
            // 次生成するときの時間を短縮
            //iTimer = (int)(fInterval / fSpeed) + 10;
        }

        // 指令の最初のオブジェクトが先頭になるように
        //if (gAcceleList.Count == 0 && CreateList.Count == 0) {
        OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
        Sort(first);
        //}

        if(gObjectList.Count > 0) {
            fFirstDegree = gObjectList[0].GetComponent<CRotateObject>().Get_fDegree();
        }
    }

    // オブジェクト生成関数(引数:初期位置、形状、内か外か)
    public void Create(int n) {
        // オブジェクトをランダムな順序で配置
        List<int> nums = new List<int>();
        for (int i = 0; i < CLevelManager.Instance.Get_iObjectNum(); i++) {
            nums.Add(i);
        }

        // 重複がないように
        for (int i = 0; i < CreateList.Count; i++) {
            nums.Remove((int)CreateList[i]);
        }

        for (int i = 0; i < gObjectList.Count; i++) {
            CRotateObject cro = gObjectList[i].GetComponent<CRotateObject>();
            nums.Remove((int)cro.Get_Shape());
        }

        for (int i = 0; i < gAcceleList.Count; i++) {
            CRotateObject cro = gAcceleList[i].GetComponent<CRotateObject>();
            nums.Remove((int)cro.Get_Shape());
        }

        // 生成リストに追加
        for (int i = 0; i < n; i++) {
            int rand = Random.Range(0, nums.Count);
            CreateList.Add((OBJECT_SHAPE)nums[rand]);
            nums.RemoveAt(rand);
        }
    }

    // オブジェクト追加関数
    public void Add(GameObject g) {
        gObjectList.Add(g);
        gAcceleList.Remove(g);

        CDegreeCompare dc = new CDegreeCompare();
        gObjectList.Sort(dc);
    }

    // オブジェクト削除関数
    public void Remove(GameObject g) {
        gObjectList.Remove(g);
        gAcceleList.Remove(g);
        for (int i = 0; i < gAcceleList.Count; i++) {
            gAcceleList[i].GetComponent<CRotateObject>().Set_iRanking(gObjectList.Count + i);
        }
    }

    public void AddCreateList(OBJECT_SHAPE first) {
        CreateList.Add(first);
    }

    // 指定したオブジェクトが先頭に来るように並べ替える
    public void Sort(OBJECT_SHAPE first) {
        // 先頭にくるオブジェクトのindex取得
        int firstid = 0;
        for (int i = 0; i < gObjectList.Count; i++) {
            if (gObjectList[i].GetComponent<CRotateObject>().Get_Shape() == first) {
                firstid = i;
                break;
            }
        }

        if (firstid == 0) {
            return;
        }

        // 邪魔なオブジェクトを後方に
        for (int i = 0; i < firstid; i++) {
            GameObject g = gObjectList[0];
            gObjectList.Remove(g);
            gAcceleList.Add(g);

            g.GetComponent<CRotateObject>().Set_isAccele(true);
        }

        for (int i = 0; i < gAcceleList.Count; i++) {
            gAcceleList[i].GetComponent<CRotateObject>().Set_iRanking(gObjectList.Count + i);
        }
    }

    public void RankSort() {
        CDegreeCompare dc = new CDegreeCompare();
        gAcceleList.Sort(dc);

        for (int i = 0; i < gAcceleList.Count; i++) {
            gAcceleList[i].GetComponent<CRotateObject>().Set_iRanking(gObjectList.Count + i);
        }
    }

    // オブジェクトを加速させる
    public void Accele(GameObject g) {
        // オブジェクトリストから削除
        int i;
        for (i = 0; i < gObjectList.Count; i++) {
            if (gObjectList[i] == g) {
                gObjectList.RemoveAt(i);
                break;
            }
        }

        // 加速させる
        g.GetComponent<CRotateObject>().Set_isAccele(true);
        gAcceleList.Add(g);

        // 後ろのオブジェクトを加速させて詰める
        for (int j = i; j < gObjectList.Count;) {
            gObjectList[j].GetComponent<CRotateObject>().Set_isAccele(true);
            gAcceleList.Add(gObjectList[j]);
            gObjectList.RemoveAt(j);
        }

        RankSort();
    }

    // 間隔getter
    public float Get_fInterval() {
        return fInterval;
    }

    // 全オブジェクトのスピード変更関数
    public void Set_fSpeed(float s) {
        for (int i = 0; i < gObjectList.Count; i++) {
            CRotateObject cs = gObjectList[i].GetComponent<CRotateObject>();
            cs.Set_fSpeed(s);
        }
    }

    // オブジェクトリストgetter
    public List<GameObject> Get_gObjectList() {
        return gObjectList;
    }

    // 特定のオブジェクトgetter
    public GameObject Get_gObject(int id) {
        if (gObjectList.Count <= id) {
            return null;
        }
        else {
            return gObjectList[id];
        }
    }

    // 順番getter
    public int Get_iRanking(GameObject g) {
        for (int i = 0; i < gObjectList.Count; i++) {
            if (gObjectList[i] == g) {
                return i;
            }
        }
        return -1;
    }

    // 先頭の角度
    public float Get_fFirstDegree() {
        return fFirstDegree;
    }

    public int Get_iObjectNum() {
        return gObjectList.Count;
    }
}
