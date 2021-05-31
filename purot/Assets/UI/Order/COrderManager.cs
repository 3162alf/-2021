/*==============================================================================
    Project_HOGE
    [COrderManager.cs]
    ・指令制御
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        
        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COrderManager : CSingletonMonoBehaviour<COrderManager> {
    [SerializeField] private GameObject[] gOrderSource;              // 指令オブジェクト配列
    [SerializeField] private GameObject gClearLamp;                  // クリアランプ

    private List<OBJECT_SHAPE> OrderList = new List<OBJECT_SHAPE>();      // 指令リスト
    private List<GameObject> gOrderList = new List<GameObject>();         // 指令オブジェクト
    private List<GameObject> gClearLampList = new List<GameObject>();     // クリアランプスタンプ

    // Start is called before the first frame update
    void Start() {
        //CreateOrder(3);
    }

    // Update is called once per frame
    void Update() {

    }

    // 指令数getter
    public int Get_iOrderNum() {
        return gOrderList.Count;
    }

    // 指定した番号の指令getter
    public OBJECT_SHAPE Get_Order(int id) {
        return OrderList[id];
    }

    // 指定した番号のクリアランプgetter
    public GameObject Get_gClearLamp(int id) {
        return gClearLampList[id];
    }

    // 指令生成関数(引数:指令数)
    public void CreateOrder(int n) {
        // 指令の種類
        int kind = CLevelManager.Instance.Get_iObjectNum();

        // 前回の指令を破棄
        for (int i = 0; i < gOrderList.Count; i++) {
            Destroy(gOrderList[i]);
            Destroy(gClearLampList[i]);
        }

        // リスト初期化
        gOrderList.Clear();
        gClearLampList.Clear();
        OrderList.Clear();

        List<int> nums = new List<int>();

        for(int i = 0; i < kind; i++) {
            nums.Add(i);
        }

        // 指令を乱数で生成
        for (int i = 0; i < n; i++) {
            // 乱数生成(0 ～ nums.Count-1)
            int rand = Random.Range(0, nums.Count);

            OrderList.Add((OBJECT_SHAPE)nums[rand]);

            // 指令の重複をなくす
            nums.RemoveAt(rand);

            float st = 0;

            if(n % 2 == 0) {
                st = 2.5f + 5 * (n / 2.0f - 1);
            }
            else {
                st = 5 * (n / 2);
            }

            // 指令生成
            gOrderList.Add(Instantiate(gOrderSource[(int)OrderList[i]],
                new Vector3(-18, 0, st - i * 5), Quaternion.Euler(0, 180, 0)));

            // クリアランプ生成
            gClearLampList.Add(Instantiate(gClearLamp,
                new Vector3(-20, 0, st - i * 5), Quaternion.Euler(0, 0, 0)));
        }
    }
}
