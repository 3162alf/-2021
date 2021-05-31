/*==============================================================================
    Priject_Beta
    [CCursorManager.cs]
    ・Cursorでの選択システム

--------------------------------------------------------------------------------
    2021.04.26 @Author Hirano Tomoki
================================================================================
    History
        210525 Sasaki
            ポーズ画面の時にオブジェクトが回転しないような処理追加(69~71行目)

/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCursorManager : MonoBehaviour {

    //スティック入力用変数------------------------------------------------------
    [SerializeField, TooltipAttribute("水平方向用のスティックの登録名")]
    private string stHorStickName = "Horizontal";
    [SerializeField, TooltipAttribute("垂直方向用のスティックの登録名")]
    private string stVerStickName = "Vertical";

    [SerializeField] private float fDeadZone = 0.01f;

    private float fHorizontal;                      // 水平方向のスティックの傾き格納変数
    private float fVertivcal;                       // 

    private float fDeg;                             // スティックの傾きから算出した角度格納用変数
    private float fRad;                             // スティックの傾きから算出したラジアン角格納用変数
    private float fOldDeg;                          // 1フレーム前の角度記憶用変数

    //--------------------------------------------------------------------------

    //--------------------------------------------------------------------------
    [SerializeField] private GameObject gCursor;              // 生成するオブジェクト
    [SerializeField] private float fCreateRad = 0f;         // "Lamp"オブジェクト生成に使用する半径

    private float fPosX = 0f;       // 
    private float fPosZ = 0f;       // 

    private Vector3 vCreatePos;     // 

    //--------------------------------------------------------------------------

    void Start() {
        fDeg = 0;
        fOldDeg = fDeg;

        // オブジェクトを生成する位置を計算する
        fPosX = Mathf.Cos(Mathf.PI / 2) * fCreateRad;
        fPosZ = Mathf.Sin(Mathf.PI / 2) * fCreateRad;

        vCreatePos = new Vector3(fPosX, 1.0f, fPosZ);

        // Lampオブジェクトを生成
        gCursor = Instantiate(
            gCursor,
            vCreatePos,
            Quaternion.identity,
            transform
        ) as GameObject;
    }

    void Update() {
        if (Mathf.Approximately(Time.timeScale, 0f)){
            return;
        }
        // 水平方向と垂直方向のスティックの傾きを取得
        fHorizontal = Input.GetAxis(stHorStickName);
        fVertivcal = Input.GetAxis(stVerStickName);

        // スティックの検知範囲を調整
        if (fHorizontal < fDeadZone &&
            fHorizontal > -fDeadZone &&
            fVertivcal < fDeadZone &&
            fVertivcal > -fDeadZone) {
            fDeg = fOldDeg;
            return;
        }

        // スティックの傾きをラジアン角に変換
        fDeg = Mathf.Atan2(fVertivcal, fHorizontal) * Mathf.Rad2Deg - 90;

        fOldDeg = fDeg;
        fRad = fDeg * Mathf.Deg2Rad;
    }

    // 変換後のラジアン角のゲッター
    public float Get_fRad() {
        return fRad;
    }

    // 変換後の角度のゲッター
    public int Get_iDeg() {
        return (int)fDeg;
    }

    public float Get_fCreateRad() {
        return fCreateRad;
    }
}
