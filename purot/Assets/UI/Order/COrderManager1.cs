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

public class COrderManager1 : MonoBehaviour {
    [SerializeField] private GameObject[] gOrderSource;              // 指令オブジェクト配列
    [SerializeField] private GameObject gClearLamp;

    private List<OBJECT_SHAPE> OrderList = new List<OBJECT_SHAPE>();      // 指令リスト
    private List<GameObject> gOrderList = new List<GameObject>();         // 指令オブジェクト
    private List<GameObject> gClearLampList = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        CreateOrder(3);
    }

    // Update is called once per frame
    void Update() {

    }

    public int Get_iOrderNum() {
        return gOrderList.Count;
    }

    public OBJECT_SHAPE Get_Order(int id) {
        return OrderList[id];
    }

    public GameObject Get_gClearLamp(int id) {
        return gClearLampList[id];
    }

    // 指令生成関数
    public void CreateOrder(int n) {
        int kind = gOrderSource.Length;

        for (int i = 0; i < gOrderList.Count; i++) {
            Destroy(gOrderList[i]);
            Destroy(gClearLampList[i]);
        }

        gOrderList.Clear();
        gClearLampList.Clear();
        OrderList.Clear();

        List<int> nums = new List<int>();

        for(int i = 0; i < kind; i++) {
            nums.Add(i);
        }

        for (int i = 0; i < n; i++) {
            int rand = Random.Range(0, nums.Count);

            OrderList.Add((OBJECT_SHAPE)nums[rand]);
            nums.RemoveAt(rand);

            gOrderList.Add(Instantiate(gOrderSource[(int)OrderList[i]],
                new Vector3(-18, 0, 5 - i * 5), Quaternion.Euler(0, 180, 0)));

            gClearLampList.Add(Instantiate(gClearLamp,
                new Vector3(-20, 0, 5 - i * 5), Quaternion.Euler(0, 0, 0)));
        }

        for (int i = gClearLampList.Count - 1; i > n - 1; i--) {
            gClearLampList.RemoveAt(i);
        }
    }
}
