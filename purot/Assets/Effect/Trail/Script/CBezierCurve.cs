/*==============================================================================
    PROJECT NAME
    [CBezierCurve.cs]
    ・ベジェ曲線を利用して、終点までカーブを描いてオブジェクトを移動させる
--------------------------------------------------------------------------------
    2021.05.17 @Author Hirano
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBezierCurve : MonoBehaviour
{
    [SerializeField] GameObject gTrail;         // 移動させるオブジェクト
    [SerializeField] GameObject gRelayObject1;  // 1つ目の中継点
    [SerializeField] GameObject gRelayObject2;  // 2つ目の中継点
    [SerializeField] GameObject gTarget;        // 終点

    private Vector3 vStartPos;                  // 初期位置
    private Vector3 vMovePoint;                 // 移動座標格納変数

    [SerializeField] private int iMiddlePoints = 30;    // 終点に行くまでの時間(1sec = 60flame)
    private int iTotalPoints;                           // 中継点を加えたすべての点
    private int iCnt;                                   // カウント用

    void Start() {

        // 初期位置を設定
        vStartPos = gTrail.transform.position;
        // 調点数を設定
        iTotalPoints = iMiddlePoints + 2;
        // カウントを0に
        iCnt = 0;
    }

    void Update() {
        // 終点までの移動割合を計算(Min:0 ～　Max:1)
        float fT = (float)iCnt / (float)(iTotalPoints - 1);

        // GetPoint関数を使い、移動座標の計算(ベジェ曲線)
        vMovePoint = GetPoint(vStartPos,
                        gRelayObject1.transform.position,
                        gRelayObject2.transform.position,
                        gTarget.transform.position, fT);

        // オブジェクト移動
        gTrail.transform.position = vMovePoint;

        // カウントが規定値になるまで加算
        if (iCnt <= iMiddlePoints)
            iCnt++;
    }

    // (始点、中継点1、中継点2、終点、移動割合)を引数に、補間後の移動座標を取得する関数
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }
}
