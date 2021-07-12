/*==============================================================================
    PROJECT ???
    [CTimer.cs]
    制限時間の制御をする処理
--------------------------------------------------------------------------------
    2021.06.20 @Author Kaname Ota
================================================================================
    History
        2021.06.20
            制限時間をスプライトで表示
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CTimer : MonoBehaviour
{
    // 表示位置
    Vector3 vInitPos;

    // 表示関連
    private int iPoint;                  // 表示する値
    //private float fSize = 1;             // 表示サイズ
    private static int iSort = 0;        // 数字の表示順
    private const int SORT_MAX = 30000;  // ソートする数字の最大数

    // タイマー関連
    [SerializeField]
    private float fMinute;               // 分数用変数

    [SerializeField]
    private float fSeconds;             // 秒数用変数
    private float fOldSeconds;          // 一個前の分数
    private float fTotalTime;           // 総合時間
    //private float fCountTime;           // タイマー切り替えよう


    void Start() {
        // 総合時間の算出と初期化
        fTotalTime = fMinute * 60 + fSeconds;
        fOldSeconds = 0.0f;
        //fCountTime = 0.0f;

        Init((int)fTotalTime, new Vector3(0, 0, 0));

    }

    public void Init(int point, Vector3 pos) {
        // 必要な情報を格納
        this.iPoint = point;

        // 表示用のダメージを作る
        CreateNum(point,0.0f);

        vInitPos = pos;

        // 表示順を一番上に
        GetComponent<SortingGroup>().sortingOrder = iSort;
        
        iSort++;
        if (iSort > SORT_MAX) {
            iSort = 0;
        }
    }

    void Update() {
        // 総合時間がなくなったら処理を飛ばす
        if (fTotalTime <= 0.0f) {
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

        if ((int)fSeconds != (int)fOldSeconds) {
            Debug.Log("通った");
        
            // 子オブジェクトがタイマーのスプライトなのでそれを消す
            GameObject obj = GameObject.Find("PFB_TimerController");
        
            foreach (Transform child in obj.transform) {
                Destroy(child.gameObject);
            }

            // 一番左のゼロ
            CreateNum(0, 3.5f);

            // コロンごり押し生成
            CreateColon(0.0f);

            // 分割
            CreateNum((int)fMinute, 2.0f);

            // 秒数の幅調整用
            if((int)fSeconds >= 10) {
                CreateNum((int)fSeconds, -1.3f);
            }

            if ((int)fSeconds < 10) {
                CreateNum((int)fSeconds, -2.0f);
                CreateNum(0, -0.4f);
            }
        }

        // 一個前の秒数に現在の時間を代入
        fOldSeconds = fSeconds;


        // 総合時間がなくなったらリザルト画面に遷移する
        if (fTotalTime <= 0.0f) {
            SceneManager.LoadScene("ResultScene");
        }
    }

    //描画用の数字を作る
    private void CreateNum(int point, float X) {

        //桁を割り出す
        int iDigit = ChkDigit(point);

        // 数字プレハブを読み込む、テスト用のフォルダとファイル名
        //GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/UI//Timer/PFB_Timer.prefab");
        GameObject obj = (GameObject)Resources.Load("PFB_Timer");

        //桁の分だけオブジェクトを作り登録していく
        for (int i = 0; i < iDigit; i++) {

            GameObject numObj = Instantiate(obj) as GameObject;

            Debug.Log(numObj);

            // 子供として登録
            numObj.transform.parent = transform;

            // 現在チェックしている桁の数字を割り出す
            int digNum = GetPointDigit(point, i + 1);

            // ポイントから数字を切り替える
            numObj.GetComponent<CTimerController>().ChangeSprite(digNum);

            // サイズをゲットする
            float size_w = numObj.GetComponent<SpriteRenderer>().bounds.size.x;

            // 位置をずらす 0.75fは数字の間隔の調整
            float ajs_x = (size_w * i - (size_w * iDigit) / 2) * 1.0f + X;
            Vector3 pos = new Vector3(numObj.transform.position.x - ajs_x, numObj.transform.position.y, numObj.transform.position.z);
            numObj.transform.position = pos;

            numObj = null;
        }

    }

    private void CreateColon(float X) {
        // 数字プレハブを読み込む、テスト用のフォルダとファイル名
        //GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/UI/Timer/PFB_Timer.prefab");
        GameObject obj = (GameObject)Resources.Load("PFB_Timer");

        GameObject numObj = Instantiate(obj) as GameObject;
        Debug.Log("ころん");
        Debug.Log(numObj);

        // 子供として登録
        numObj.transform.parent = transform;

        // ポイントから数字を切り替える
        numObj.GetComponent<CTimerController>().ChangeSprite(10);

        // 位置をずらす
        Vector3 pos = new Vector3(numObj.transform.position.x - X, numObj.transform.position.y, numObj.transform.position.z);
        numObj.transform.position = pos;

        numObj = null;
    }

    // 整数の桁数を返す
    public static int ChkDigit(int num) {

        //0の場合1桁として返す
        if (num == 0) return 1;

        //対数とやらを使って返す
        return (num == 0) ? 1 : ((int)Mathf.Log10(num) + 1);

    }
    
    // 数値の中から指定した桁の値をかえす
    public static int GetPointDigit(int num, int digit) {

        int res = 0;
        int pow_dig = (int)Mathf.Pow(10, digit);
        if (digit == 1) {
            res = num - (num / pow_dig) * pow_dig;
        }
        else {
            res = (num - (num / pow_dig) * pow_dig) / (int)Mathf.Pow(10, (digit - 1));
        }

        return res;
    }
}


