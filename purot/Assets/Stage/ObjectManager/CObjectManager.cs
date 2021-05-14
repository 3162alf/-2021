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
    private List<GameObject> gAcceleList = new List<GameObject>();     // オブジェクトリスト
    private List<OBJECT_SHAPE> CreateList = new List<OBJECT_SHAPE>();  // 生成リスト

    [SerializeField] private GameObject[] gObjectSource;          // オブジェクトソース
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
            //if (iTimer >= fInterval / fAcceleSpeed + 30) {
            if (iTimer >= 60) {
                // 重ならないように生成位置を調整
                float sd = 180.0f;
                float rad = 9.0f;
                RotateState rs = RotateState.OUTSIDE;
                if (gObjectList.Count > 0) {
                    float ld = gObjectList[gObjectList.Count - 1].GetComponent<CRotateObject>().Get_fDegree();

                    if (ld >= 180 && ld <= 520) {
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
                cs.Set_fSpeed(fAcceleSpeed);
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
        if (GameObject.Find("PFB_Gate(Clone)") == null) {
            OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
            Sort(first);
        }
        //}

        Debug.Log(gObjectList.Count);
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
        if (!gObjectList.Contains(g)) {
            gObjectList.Add(g);
        }
    }

    // オブジェクト削除関数
    public void Remove(GameObject g) {
        if (gObjectList.Contains(g)) {
            gObjectList.Remove(g);
        }
    }

    // オブジェクト削除関数
    public void AcceleRemove(GameObject g) {
        if (gAcceleList.Contains(g)) {
            gAcceleList.Remove(g);
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

            g.GetComponent<CRotateObject>().Set_isAccele(true);
            g.GetComponent<CRotateObject>().Set_fSpeed(fAcceleSpeed);
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
        gAcceleList.Add(g);
        g.GetComponent<CRotateObject>().Set_isAccele(true);
        g.GetComponent<CRotateObject>().Set_fSpeed(fAcceleSpeed);

        // 後ろのオブジェクトを加速させて詰める
        for (int j = i; j < gObjectList.Count; j++) {
            gObjectList[j].GetComponent<CRotateObject>().Set_fSpeed(fAcceleSpeed);
        }
    }

    // 間隔getter
    public float Get_fInterval() {
        return fInterval;
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
        return gObjectList.IndexOf(g);
    }

    public int Get_iObjectNum() {
        return gObjectList.Count;
    }

    public float Get_fSpeed() {
        return fSpeed;
    }

    public float Get_fAcceleSpeed() {
        return fAcceleSpeed;
    }
}
