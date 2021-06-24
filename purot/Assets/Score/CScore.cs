/*==============================================================================
    Project
    [CScore.cs]
    ・スコアで使いそうなやつ急遽用意しました。
--------------------------------------------------------------------------------
    2021.05.09 Sasaki Misaki
================================================================================
    History
        2021/06/21 Ota Kaname
            
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEditor;

public class CScore : MonoBehaviour {
    public Text tScore;

    private static int iScore;
    private int iScoreParam;
    private static float fScore = 0.0f;
    public static GameObject gScoreObj; // TextオブジェクトをあとでFindして入れる、リザルトでだけ使う


    // スプライト表示関連
    Vector3 vInitPos;                    // 表示位置
    private int iPoint;                  // 表示する値
    private float fSize = 1;             // 表示サイズ
    private static int iSort = 0;        // 数字の表示順
    private const int SORT_MAX = 30000;  // ソートする数値の最大数
    private int iOldScore = 0;           // スコアの差分確認用
    private int iOldScoreParam = 0;      // スコアパラメータの差分確認用


    void Start() {
        // 一回だけ実行したいからスタートに書きました。後悔はしています。
        if (SceneManager.GetActiveScene().name == "GameScene") {
            iScore = 0;
        }
        if (SceneManager.GetActiveScene().name == "ResultScene") {
            DispScore();
        }

        // スコア表示初期化
        Init(iScore, new Vector3(0, 0, 0));
        CreateScoreSprite(iScoreParam, -4.0f);
    }

    void Update() {
        iScore = (int)fScore;
        fScore = iScore;

        // スラッシュのみ表示
        if (tScore != null) {
            tScore.text = "/";
        }

        // 直前のスコアが違っていたら表示を切り替える
        if(iScore != iOldScore || iScoreParam != iOldScoreParam) {
            Debug.Log("通った");

            // 子オブジェクトがタイマーのスプライトなのでそれを消す
            GameObject obj = GameObject.Find("PFB_ScoreObj");

            // 現在のスコア表示を削除
            foreach (Transform child in obj.transform) {
                Destroy(child.gameObject);
            }

            // スコアの表示
            CreateScoreSprite(iScore, 0.0f);
            CreateScoreSprite(iScoreParam, -4.0f);
        }

        // 直前のスコア達を更新
        iOldScore = iScore;
        iOldScoreParam = iScoreParam;
    }

    public static void AddScore() {
        iScore += 1;
    }

    public static int GetScore() {
        return iScore;
    }

    public void Set_iScoreParam(int i) {
        iScoreParam = i;
    }

    public void AddFScore() {
        fScore += 0.34f;
        Debug.Log(fScore);
    }

    public static void DispScore() {
        // オブジェクトからTextコンポーネントを取得
        gScoreObj = GameObject.Find("TextScore");
        Text ScoreText = gScoreObj.GetComponent<Text>();

        // テキストの表示を入れ替える
        ScoreText.text = iScore.ToString();
    }

    public static void ResetScore() {
        fScore = 0.0f;
    }


    public void Init(int point, Vector3 pos) {
        // 必要な情報を格納
        this.iPoint = point;

        // 表示用のダメージを作る
        CreateScoreSprite(point, 0.0f);

        vInitPos = pos;

        // 表示順を一番上に
        GetComponent<SortingGroup>().sortingOrder = iSort;

        iSort++;
        if (iSort > SORT_MAX) {
            iSort = 0;
        }

    }

    // 描画用の数字を作る
    private void CreateScoreSprite(int point, float X) {

        // 桁を割り出す
        int iDigit = ChkDigit(point);

        // 数字プレハブを読み込む、テスト用のフォルダとファイル名
        //GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Score/PFB_Score.prefab");
        GameObject obj = (GameObject)Resources.Load("PFB_Score");

        // 桁の分だけオブジェクトを作り登録していく
        for (int i = 0; i < iDigit; i++) {

            GameObject numObj = Instantiate(obj) as GameObject;

            Debug.Log(numObj);

            // 子供として登録
            numObj.transform.parent = transform;

            // 現在チェックしている桁の数字を割り出す
            int digNum = GetPointDigit(point, i + 1);

            // ポイントから数字を切り替える
            numObj.GetComponent<CScoreController>().ChangeSprite(digNum);

            // サイズをゲットする
            float size_w = numObj.GetComponent<SpriteRenderer>().bounds.size.x;

            // 位置をずらす 0.75fは数字の間隔の調整
            float ajs_x = (size_w * i - (size_w * iDigit) / 2) * 1.0f + X;
            Vector3 pos = new Vector3(numObj.transform.position.x - ajs_x, numObj.transform.position.y, numObj.transform.position.z);
            numObj.transform.position = pos;

            numObj = null;
        }

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
