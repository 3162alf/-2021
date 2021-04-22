/*==============================================================================
    PROJECT ???
    [CTimerController.cs]
    制限時間の制御をする処理
--------------------------------------------------------------------------------
    2021.03.25 @Author Kaname Ota
================================================================================
    History
        2021.04.10 @Tsubasa Ono
            制限時間が0になったらリザルト画面に遷移するように変更
            
/*============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CTimerController : MonoBehaviour {
    public Text tTimer;          // テキストを入れる箱

    [SerializeField]
    private float fTotalTime;   // 制限時間の総合時間
    private int iSecond = 0;    // 秒数

    void Start() {
        // 制限時間の上限値設定
        if (fTotalTime >120.0f) {
            fTotalTime = 119.9f;
        }
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

        // 制限時間が0になったらリザルト画面へ遷移
        if (iSecond == 0) {
            SceneManager.LoadScene("Result");
        }

    }
}
