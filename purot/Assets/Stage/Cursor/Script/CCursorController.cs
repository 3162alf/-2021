/*==============================================================================
    Priject_Beta
    [CCursorManager.cs]
    ・Cursor表示、移動処理

--------------------------------------------------------------------------------
    2021.04.26 @Author Hirano Tomoki
================================================================================
    History
        20210502 Hirano
            SphereCast追加
        20210515 Misaki Sasaki
            入れ替え時にエフェクトでるようにしてます。
        20210525 Sasaki
            ポーズ画面の時にオブジェクトが回転しないような処理追加(83~85行目)
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCursorController : MonoBehaviour {
    //Ray判定用変数-------------------------------------------------------------
    private Ray rRay;                   // 通常のray
    private Ray rSphereRay;             // SphereCast用のray
    private RaycastHit[] rhHits;        // 通常のray用の当たり判定オブジェクト格納用変数
    private RaycastHit[] rhSphereHits;  // SphereCast用の当たり判定オブジェクト格納用変数

    // SphereCastの大きさを変える変数
    // ※当たり判定が厳しすぎる場合は少しずつ大きく、緩すぎる場合は少しずつ小さくしてください。基本は0.5でちょうどいいと思います、多分。
    [SerializeField] private float fSphereCastRadius = 0.5f;

    [SerializeField] private int iDistance = 10;                // ray、SphereCastの最大距離(※ステージを広くした場合はそれに合わせて大きくしてください)
    [SerializeField] private string stTagName = "RotateObject"; // ray、SphereCastと衝突処理を行うオブジェクトのtag(※回転オブジェクトすべてに設定してください)
    [SerializeField] private LayerMask lmLayerMask;             // ray、SphereCastと衝突処理を行うオブジェクトを配置するレイヤー(※衝突処理判定を行いたいオブジェクトすべてをこのレイヤーにおいてください)


    private GameObject gCursorManager;    // LampManagerのオブジェクトを格納
    private Vector3 vMovePos;

    // 以下では、[□、×、〇、△]ボタンの入力用変数を用意していますが、
    // 現状では、〇ボタン以外の入力処理は必要ないので、コメントアウトしています。
    // 必要になったらその都度外します。

    //[SerializeField, TooltipAttribute("Aボタンの登録名")]
    //private string stButton0Name = "joystickbutton0";  // Aボタン
    [SerializeField, TooltipAttribute("Bボタンの登録名")]
    private string stButton1Name = "joystickbutton1";  // Bボタン
    //[SerializeField, TooltipAttribute("Xボタンの登録名")]
    //private string stButton2Name = "joystickbutton2";    // Xボタン
    //[SerializeField, TooltipAttribute("Yボタンの登録名")]
    //private string stButton3Name = "joystickbutton3";  // Yボタン

    // 入れ替え時にワープホールを出すために追加 ---2020/5/15 佐々木
    [SerializeField] public GameObject PAR;    // パーティクル本体を格納
    [SerializeField] public GameObject PAR_1;    // パーティクル本体を格納
    private GameObject pParticleObject = default;
    private GameObject pParticleObject_1 = default;
    private ParticleSystem pParticleSystem = default;
    private ParticleSystem pParticleSystem_1 = default;

    //--------------------------------------------------------------------------

    void Start() {
        // "PFB_LampManager"を取得してgLampManagerに格納
        gCursorManager = GameObject.Find("PFB_CursorManager");
        vMovePos.y = 0.0f;

        // 色を赤色に変更する。
        GetComponent<Renderer>().material.color = Color.red;

        // パーティクルを生成
        pParticleObject = (GameObject)Instantiate(PAR);
        pParticleObject_1 = (GameObject)Instantiate(PAR_1);
        // パーティクル制御用にコンポーネントを取得
        pParticleSystem = pParticleObject.GetComponent<ParticleSystem>();
        pParticleSystem_1 = pParticleObject_1.GetComponent<ParticleSystem>();
        // 急にパーティクルが再生されることがないように予め停止させる
        pParticleSystem.Stop();
        pParticleSystem_1.Stop();
    }

    void Update() {

        if (Mathf.Approximately(Time.timeScale, 0f)){
            return;
        }
        // 移動用の計算
        vMovePos.x = Mathf.Cos(gCursorManager.GetComponent<CCursorManager>().Get_fRad() + (Mathf.PI / 2)) * gCursorManager.GetComponent<CCursorManager>().Get_fCreateRad();
        vMovePos.z = Mathf.Sin(gCursorManager.GetComponent<CCursorManager>().Get_fRad() + (Mathf.PI / 2)) * gCursorManager.GetComponent<CCursorManager>().Get_fCreateRad();

        transform.position = new Vector3(vMovePos.x, vMovePos.y, vMovePos.z);

        if (Input.GetButtonDown(stButton1Name) || Input.GetKeyDown(KeyCode.Space)) {
            CreateSphereCast(gCursorManager.transform.position, this.transform.position);
        }
        // debug用(押している間、rayを飛ばし続ける)
        // rayの長さなどを確認したい場合は入力処理をこちらに切り替えてください。
        //if (Input.GetButton(stButton1Name) || Input.GetKey(KeyCode.Return)) {
        //    CreateRay(gCursorManager.transform.position, this.transform.position);
        //}

        // 中心を向かせる
        gCursorManager.transform.LookAt(this.transform);
    }

    // Rayを飛ばす関数(衝突判定込み)
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

    // 20210502_Hirano
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

                    // パーティクル再生
                    pParticleObject.transform.position = new Vector3(rhHitObject.transform.position.x, rhHitObject.transform.position.y + 1.0f, rhHitObject.transform.position.z);
                    pParticleObject_1.transform.position = new Vector3(rhHitObject.transform.position.x, rhHitObject.transform.position.y + 1.0f, rhHitObject.transform.position.z);
                    pParticleSystem.Play();
                    pParticleSystem_1.Play();

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
}
