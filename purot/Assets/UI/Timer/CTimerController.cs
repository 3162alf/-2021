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

        2021.04.26 @Kaname Ota
            制限時間を2:00表記にした。
            
/*============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CTimerController : MonoBehaviour {
    public Text tTimer;          // テキストを入れる箱

    [SerializeField]
    private float fMinute;      // 分数用変数

    [SerializeField]
    private float fSeconds;      // 秒数用変数
    private float fOldSeconds;   // 一個前の分数
    private float fTotalTime;   // 総合時間



    void Start() {
        // 総合時間の算出と初期化
        fTotalTime = fMinute * 60 + fSeconds;
        fOldSeconds = 0.0f;      
    }

    void Update() {
        // 総合時間がなくなったら処理を飛ばす
        if(fTotalTime <= 0.0f){
            return;
        }

        // 総合時間の更新
        fTotalTime = fMinute * 60 + fSeconds;

        // 総合時間の減算
        fTotalTime -= Time.deltaTime;

        // 分数の算出
        fMinute = (int)fTotalTime / 60;

        // 秒数の算出
        fSeconds = fTotalTime - fMinute * 60;

        // UIテキストに時間を表示
        if((int)fSeconds != (int)fOldSeconds){
            tTimer.text = fMinute.ToString("00") + ":" + ((int)fSeconds).ToString("00");
        }

        // 一個前の秒数に現在の時間を代入
        fOldSeconds = fSeconds;

        // 総合時間がなくなったらリザルト画面に遷移する
        if(fTotalTime <= 0.0f){
            SceneManager.LoadScene("ResultScene");
        }
    }

    public float Get_fTotalTime() {
        return fTotalTime;
    }
}
