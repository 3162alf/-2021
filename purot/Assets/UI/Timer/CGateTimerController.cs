/*==============================================================================
    Project_
    [CGateTimerController.cs]
    ・ゲートタイマー制御
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        20210511 Hirano XBoxコントローラー入力処理追加

        20210628 Ota Kaname タイマーの見た目色々追加
            
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

    // 20210511 追加分---------------------------------

    [SerializeField, TooltipAttribute("Aボタンの登録名")]
    private string stButton0Name = "joystickbutton0";  // Aボタン
    //[SerializeField, TooltipAttribute("Bボタンの登録名")]
    //private string stButton1Name = "joystickbutton1";  // Bボタン
    //[SerializeField, TooltipAttribute("Xボタンの登録名")]
    //private string stButton2Name = "joystickbutton2";    // Xボタン
    //[SerializeField, TooltipAttribute("Yボタンの登録名")]
    //private string stButton3Name = "joystickbutton3";  // Yボタン

    // ------------------------------------------------


    // 時計の画像回転用
    private float fRotationSpeed = 0.0f;
    private GameObject gSecondHand;
    private int iCalcTime = 30;

    void Start() {
        csObjectManager = GameObject.Find("PFB_ObjectManager").GetComponent<CObjectManager>();
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();

        gSecondHand = GameObject.Find("PFB_SecondHand");
    }

    void Update() {
        // フレームごとに総合時間から減算
        fTotalTime -= Time.deltaTime;

        // キャストした総合時間を秒数に代入
        iSecond = (int)fTotalTime;

        // テキストに秒数を表示
        //tTimer.text = iSecond.ToString("00");

        fRotationSpeed = (iCalcTime - fTotalTime) * (360 / iCalcTime);
        Debug.Log(fRotationSpeed);

        gSecondHand.transform.eulerAngles = new Vector3(90.0f,0.0f,-fRotationSpeed);

        //if (GameObject.Find(gGate.name + "(Clone)")) {
        //tTimer.text = "00";
        //}
        /* 下限値の設定
        if (iSecond <= 0) {
            tTimer.text = "00";
            fTotalTime = 0.0f;
        }*/

        // 時間が来たらまたはプレイヤsss.ーの意思で回収
        if (fTotalTime <= 0 || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown(stButton0Name)) {
            // ゲートが二つ出るのを防ぐ
            if (GameObject.Find(gGate.name + "(Clone)") == null) {
                List<GameObject> list = csObjectManager.Get_gObjectList();

                if (list.Count > 0) {
                    OBJECT_SHAPE order = csOrderManager.Get_Order(0);

                    // 指令の最初のオブジェクトの位置にゲートを出す
                    GameObject first = null;

                    int i;
                    for (i = 0; i < list.Count; i++) {
                        if (list[i].GetComponent<CRotateObject>().Get_Shape() == order) {
                            first = list[i];
                            break;
                        }
                    }

                    if (i < list.Count) {
                        float deg = first.GetComponent<CRotateObject>().Get_fDegree();
                        float subdeg = first.GetComponent<CRotateObject>().Get_fDegreeSub();
                        deg += 30;
                        Vector3 pos;

                        if (deg >= 330 && deg <= 690) {
                            subdeg = 180;
                        }
                        else {
                            subdeg = 0.0f; 
                        }
                        if (deg >= 330 && deg <= 690) {

                            if (deg <= 400 && deg >= 330) {
                                subdeg = 180 + (400 - deg) * 3.0f;
                            }
                        }
                        else {
                            if (deg <= 760 && deg >= 690) {
                                subdeg = (760 - deg) * 3.0f;
                            }
                        }

                        pos.x = 7 * Mathf.Sin((deg + 180) * Mathf.Deg2Rad)
                        + 2 * Mathf.Cos(subdeg * Mathf.Deg2Rad) * Mathf.Sin((deg + 180) * Mathf.Deg2Rad);

                        pos.y = 2 * Mathf.Sin(subdeg * Mathf.Deg2Rad);

                        pos.z = 7 * Mathf.Cos((deg + 180) * Mathf.Deg2Rad)
                        + 2 * Mathf.Cos(subdeg * Mathf.Deg2Rad) * Mathf.Cos((deg + 180) * Mathf.Deg2Rad);

                        GameObject gate = Instantiate(gGate, pos, Quaternion.Euler(0, 90, 0));

                        CGate cs = gate.GetComponent<CGate>();
                        CRotateObject cro = first.GetComponent<CRotateObject>();

                        if(deg >= 330 && deg <= 690) {
                            cs.Set_State(RotateState.INSIDE);

                            if(deg <= 400 && deg >= 330) {
                                cs.Set_isInverse(true);
                                cs.Set_fDegreeSub(180 + (400 - deg) * 3.0f);
                            }
                        }
                        else {
                            cs.Set_State(RotateState.OUTSIDE);
                            if(deg <= 760 && deg >= 690) {
                                cs.Set_isInverse(true);
                                cs.Set_fDegreeSub((760 - deg) * 3.0f);
                            }
                        }

                        cs.Set_fDegree(deg);

                        fTotalTime = 0;
                    }
                }
            }


            fTotalTime = 0;
        }
    }

    // タイマーリセット関数
    public void Reset() {
        fTotalTime = 30;
    }
}
