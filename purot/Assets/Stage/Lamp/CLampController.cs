/*==============================================================================
    Priject_
    [CLampController.cs]
    ・PFB_LampManagerオブジェクトとLampオブジェクトとの2点間の角度を求める
    ・CLampManager からスティックの傾きに対応したラジアン角を取得する
    ・2点間の角度と取得したラジアン角を比較して、対応したLampの色を変える
    ・rayを飛ばす処理
    ・rayと指定したtagとの衝突判定

--------------------------------------------------------------------------------
    2021.03.24 @Author Hirano Tomoki
================================================================================
    History
        210404 Hirano
            Rayを飛ばす関数を作成(回転オブジェクトに衝突したらlogを流す)
            SphereCastを飛ばす関数を作成(回転オブジェクトに衝突したらlogを流す)

/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLampController : MonoBehaviour {

    private GameObject gLampManager;    // LampManagerのオブジェクトを格納
    private Vector3 vDt;

    private int iControllerAngle = 0;
    private float fAngle = 0f;
    private float fRad = 0f;
    private float fDeg = 0f;

    // Rayを飛ばす処理用変数---------------------------------------------------

    private Ray rRay;                   // 通常のray
    private Ray rSphereRay;             // SphereCast用のray
    private RaycastHit[] rhHits;        // 通常のray用の当たり判定オブジェクト格納用変数
    private RaycastHit[] rhSphereHits;  // SphereCast用の当たり判定オブジェクト格納用変数

    [SerializeField] private int iDistance = 10;                // ray、SphereCastの最大距離(※ステージを広くした場合はそれに合わせて大きくしてください)
    [SerializeField] private string stTagName = "RotateObject"; // ray、SphereCastと衝突処理を行うオブジェクトのtag(※回転オブジェクトすべてに設定してください)
    [SerializeField] private LayerMask lmLayerMask;             // ray、SphereCastと衝突処理を行うオブジェクトを配置するレイヤー(※衝突処理判定を行いたいオブジェクトすべてをこのレイヤーにおいてください)

    // SphereCastの大きさを変える変数
    // ※当たり判定が厳しすぎる場合は少しずつ大きく、緩すぎる場合は少しずつ小さくしてください。基本は1.5でちょうどいいと思います、多分。
    [SerializeField] private float fSphereCastRadius = 1.5f;



    // 以下では、[□、×、〇、△]ボタンの入力用変数を用意していますが、
    // 現状では、〇ボタン以外の入力処理は必要ないので、コメントアウトしています。
    // 必要になったらその都度外します。

    //[SerializeField, TooltipAttribute("□ボタンの登録名")]
    //private string stButton0Name = "joystickbutton0";  // □ボタン
    //[SerializeField, TooltipAttribute("×ボタンの登録名")]
    //private string stButton1Name = "joystickbutton1";  // ×ボタン
    [SerializeField, TooltipAttribute("〇ボタンの登録名")]
    private string stButton2Name = "joystickbutton2";    // 〇ボタン
    //[SerializeField, TooltipAttribute("△ボタンの登録名")]
    //private string stButton3Name = "joystickbutton3";  // △ボタン

    //-------------------------------------------------------------------------

    void Start() {
        // "PFB_LampManager"を取得してgLampManagerに格納
        gLampManager = GameObject.Find("PFB_LampManager");

        // "Lamp"オブジェクトの初期色をGrayに設定
        GetComponent<Renderer>().material.color = Color.gray;
        // fAngle変数にPFB_LampManagerとこのオブジェクトの2点間の角度を格納
        fAngle = Get_fAngle(gLampManager.transform.position, this.transform.position);
    }

    void Update() {
        // コントローラーの傾きから算出したラジアン角を格納
        iControllerAngle = gLampManager.GetComponent<CLampManager>().Get_iRad();

        // 指定範囲内にある"Lamp"オブジェクトを黄色に、それ以外を灰色にする
        if (iControllerAngle - 10 < fAngle && iControllerAngle + 10 > fAngle) {

            // 色を黄色にする
            GetComponent<Renderer>().material.color = Color.yellow;

            // rayを飛ばす
            // 本実装用(押した瞬間だけrayを飛ばす)
            // ※DS4を接続している場合は"〇ボタン"、キーボード入力の場合は"Space"キーで入力してください
            if (Input.GetButtonDown(stButton2Name) || Input.GetKeyDown(KeyCode.Space)) {
                CreateSphereCast(gLampManager.transform.position, vDt);
            }

            // debug用(押している間、rayを飛ばし続ける)
            // rayの長さなどを確認したい場合は入力処理をこちらに切り替えてください。
            //if (Input.GetButton(stButton2Name) || Input.GetKey(KeyCode.Return)) {
            //    CreateSphereCast(gLampManager.transform.position, vDt);
            //}
        }
        else {
            // 色を灰色に変更する。
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    // 20210404_Hirano
    // rayを飛ばす関数(debug用に可視化)
    private void CreateRay(Vector3 vPos, Vector3 vDir) {

        // rayの生成位置と方向を指定
        rRay = new Ray(vPos, vDir);
        // rayの可視化
        Debug.DrawRay(rRay.origin, rRay.direction * iDistance, Color.red);

        // rayを飛ばし、衝突しているオブジェクトをすべて探す。
        // RaycastAll(rRay : 原点、飛ばす方向, iDistance : 長さ, lmLayerMask : 衝突処理を行うレイヤーを制限)
        rhHits = Physics.RaycastAll(rRay, iDistance, lmLayerMask);
        foreach (RaycastHit rhHitObject in rhHits) {
            Debug.Log("Rayが" + stTagName + "に当たった");

            // ひっくり返す処理
            if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.OUTSIDE) {
                rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.INSIDE);
            }
            else if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.INSIDE) {
                rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.OUTSIDE);
            }
        }
    }

    // 20210404_Hirano
    // SphereCastを飛ばす関数
    private void CreateSphereCast(Vector3 vPos, Vector3 vDir) {

        // rayの生成位置と方向を指定
        rSphereRay = new Ray(vPos, vDir);
        // rayの可視化
        Debug.DrawRay(rSphereRay.origin, rSphereRay.direction * iDistance, Color.red);

        // SphereCastを飛ばし、衝突しているオブジェクトをすべて探す。
        // SpereCastAll( rSphereRay : 原点、飛ばす方向　, fSphereCastRadius : SphereCastの大きさ　, iDistance : rayの最大距離　, lmLayerMask : 衝突処理を行うレイヤー)
        rhSphereHits = Physics.SphereCastAll(rSphereRay, fSphereCastRadius, iDistance, lmLayerMask);
        foreach (RaycastHit rhHitObject in rhSphereHits) {
            Debug.Log("SphereCastが" + stTagName + "に当たった");

            if (!rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_isAccele()) {
                if (COrderManager.Instance.Get_Order(0) != rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_Shape()) {
                    // ひっくり返す処理
                    if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.OUTSIDE) {
                        rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.INSIDE);
                    }
                    else if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.INSIDE) {
                        rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.OUTSIDE);
                    }
                    CObjectManager.Instance.Accele(rhHitObject.collider.gameObject);
                }
            }
        }
    }

    // 2点間の角度を算出する関数
    float Get_fAngle(Vector3 vStart, Vector3 vTarget) {
        vDt = vTarget - vStart;
        fRad = Mathf.Atan2(vDt.x, vDt.z);
        fDeg = fRad * -Mathf.Rad2Deg;

        if (fDeg < 0) {
            fDeg += 360;
        }

        return fDeg;
    }
}
