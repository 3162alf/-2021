/*==============================================================================
    Project_
    [CGateTimerController.cs]
    ・ゲートタイマー制御
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        
            
/*============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class CGateTimerController : MonoBehaviour {
    public Text tTimer;          // テキストを入れる箱

    [SerializeField]
    private float fTotalTime;   // 制限時間の総合時間
    private int iSecond = 0;    // 秒数

    [SerializeField] GameObject gGate; // ゲート
    private CObjectManager csObjectManager;
    private COrderManager1 csOrderManager;

    void Start() {
        csObjectManager = GameObject.Find("PFB_ObjectManager").GetComponent<CObjectManager>();
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager1>();
    }

    void Update() {
        // フレームごとに総合時間から減算
        fTotalTime -= Time.deltaTime;

        // キャストした総合時間を秒数に代入
        iSecond = (int)fTotalTime;

        // テキストに秒数を表示
        tTimer.text = iSecond.ToString("00");

        /* 下限値の設定
        if (iSecond <= 0) {
            tTimer.text = "00";
            fTotalTime = 0.0f;
        }*/

        if(iSecond == 0) {
            List<GameObject> list = csObjectManager.Get_gObjectList();
            OBJECT_SHAPE order = csOrderManager.Get_Order(0);

            GameObject first = new GameObject();
            CRotateObject cro = new CRotateObject();

            for(int i = 0; i < list.Count; i++) {
                cro = list[i].GetComponent<CRotateObject>();
                if (cro.Get_Shape() == order) {
                    first = list[i];
                    break;
                }
            }

            Vector3 pos = first.transform.position;

            GameObject gate = Instantiate(gGate, pos, Quaternion.Euler(0, 0, 90));
            CGate1 cs = gate.GetComponent<CGate1>();
            cs.Set_State(cro.Get_RotateState());
            cs.Set_fDegree(cro.Get_fDegree());

            fTotalTime = -1;
        }

    }

    public void Reset() {
        fTotalTime = 5;
    }
}
