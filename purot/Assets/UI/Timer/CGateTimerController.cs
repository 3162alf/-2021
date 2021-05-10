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
    private COrderManager csOrderManager;

    void Start() {
        csObjectManager = GameObject.Find("PFB_ObjectManager").GetComponent<CObjectManager>();
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();
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

        // 時間が来たらまたはプレイヤーの意思で回収
        if(iSecond == 0 || Input.GetKeyDown(KeyCode.Return)) {
            // ゲートが二つ出るのを防ぐ
            if (GameObject.Find(gGate.name + "(Clone)") == null) {
                List<GameObject> list = csObjectManager.Get_gObjectList();

                if (list.Count > 0) {
                    OBJECT_SHAPE order = csOrderManager.Get_Order(0);

                    // 指令の最初のオブジェクトの位置にゲートを出す
                    GameObject first = new GameObject();

                    int i;
                    for (i = 0; i < list.Count; i++) {
                        if (list[i].GetComponent<CRotateObject>().Get_Shape() == order) {
                            first = list[i];
                            break;
                        }
                    }

                    if (i < list.Count) {
                        Vector3 pos = first.transform.position;

                        GameObject gate = Instantiate(gGate, pos, Quaternion.Euler(0, 0, 90));
                        CGate cs = gate.GetComponent<CGate>();
                        CRotateObject cro = list[i].GetComponent<CRotateObject>();

                        cs.Set_State(cro.Get_RotateState());
                        cs.Set_fDegree(cro.Get_fDegree());

                        fTotalTime = 0;
                    }
                }
            }
        }
    }

    // タイマーリセット関数
    public void Reset() {
        fTotalTime = 30;
    }
}
