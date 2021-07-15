/*==============================================================================
    PROJECT ???
    [CBackGroundController.cs]
    背景オブジェクトのコントローラ
--------------------------------------------------------------------------------
    2021.05.13 @Author Kaname Ota
================================================================================
    History
        2021.05.13 @Kaname Ota
            
        210525 Sasaki
        ポーズ画面の時にオブジェクトが回転しないような処理追加(●~●行目)
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CBackGroundController : MonoBehaviour
{
    // 背景のプレハブをそれぞれ入れる
    [SerializeField] private GameObject[] gPrefab = new GameObject[6];
    [SerializeField] private float fColorLv;    // 色合いの調整用
    [SerializeField] private float fSpeed;      // 背景の速度
    [SerializeField] private Vector3 vBackGroundPos;      // 生成位置

    public Material material;                   // ワイヤーフレームを付けるための変数
    private GameObject gBackGround01;           // 生成する方　多分無駄してる
    private GameObject gBackGround02;
    private int iObjectCount;                   // 表示しているオブジェクトの数      
    private Transform tTransform01;             // 各背景のトランスフォーム格納用
    private Transform tTransform02;
    private int iRandomElement;                 // 背景生成の乱数用
    private byte bColorRed;                     // RGBの変数用
    private byte bColorGreen;
    private byte bColorBlue;
    private bool isReturnColor;                 // ゲーミング背景に必要なフラグ
    private int iBackGroundLevel = 0;           // 背景のレベル変動用
    private CLevelManager Clevelmanager;        // レベルマネージャー取得用変数


    void Start() {
        // 開始時の背景を生成
        iBackGroundLevel = 0;
        bColorRed = 1;
        bColorGreen = 1;
        bColorBlue = 255;
        isReturnColor = false;

        // ゲームシーンならオブジェクトを探し、レベル変動させる
        if (SceneManager.GetActiveScene().name == "GameScene") {
            Clevelmanager = GameObject.Find("PFB_LevelManager").GetComponent<CLevelManager>();
            iBackGroundLevel = Clevelmanager.Get_iLevel();
            gBackGround01 = Instantiate(gPrefab[iBackGroundLevel], new Vector3(vBackGroundPos.x, 150.0f, vBackGroundPos.z), Quaternion.Euler(90.0f, 0.0f, 0.0f)) as GameObject;
        }
        else {
            gBackGround01 = Instantiate(gPrefab[iBackGroundLevel], new Vector3(vBackGroundPos.x, 150.0f, vBackGroundPos.z), Quaternion.Euler(90.0f, 0.0f, 0.0f)) as GameObject;
        }

        gBackGround01.transform.localScale = new Vector3(1, 1, 3);
        GameObject gChild = gBackGround01.transform.Find("default").gameObject;
        gChild.GetComponent<Renderer>().material = material;
        iObjectCount++;

    }

    void Update() {
        // 魔法
        if (Mathf.Approximately(Time.timeScale, 0f)) {
            return;
        }

        // 色を変える分岐式
        if (!isReturnColor) {
            bColorRed++;
        }
        else {
            bColorRed--;
        }

        if (bColorRed >= 254) {
            bColorRed = 254;
            bColorBlue--;

            if(bColorBlue <= 1) {
                bColorBlue = 1;
                bColorGreen++;

                if(bColorGreen >= 254) {
                    bColorGreen = 254;
                    isReturnColor = true;
                }
            }
        }

        if(bColorRed <= 1) {
            bColorRed = 1;
            bColorBlue++;

            if(bColorBlue >= 254) {
                bColorBlue = 254;
                bColorGreen--;

                if(bColorGreen <= 1) {
                    bColorGreen = 1;
                    isReturnColor = false;
                }
            }
        }

        // マテリアルの色をセット
        material.SetColor("_FrontColor", new Color32((byte)(bColorRed * fColorLv), (byte)(bColorGreen * fColorLv), (byte)(bColorBlue * fColorLv), 255));
        material.SetColor("_BackColor", new Color32((byte)(bColorRed * fColorLv), (byte)(bColorGreen * fColorLv), (byte)(bColorBlue * fColorLv), 255));

        // 背景のレベル変動を変更するやつ
        if (SceneManager.GetActiveScene().name == "GameScene") {
            iBackGroundLevel = Clevelmanager.Get_iLevel();
            if (iBackGroundLevel % 2 == 1)
            {
                Debug.Log(iBackGroundLevel);
                iBackGroundLevel--;
            }
        }


        // 有無の確認
        if (gBackGround01 != null) {
            // transformを取得
            tTransform01 = gBackGround01.transform;

            // 座標を取得
            Vector3 vPos = tTransform01.position;
            vPos.y += fSpeed;

            // 速度を加算
            tTransform01.position = vPos;

            // 二種類目を出す
            if (tTransform01.position.y >= 700.0f && iObjectCount < 2)
            {

                Debug.Log("二個目生成1");
                // 3個の背景からランダムに生成
                //iRandomElement = Random.Range(0, 2);
                gBackGround02 = Instantiate(gPrefab[iBackGroundLevel + 1], new Vector3(vBackGroundPos.x, vBackGroundPos.y, vBackGroundPos.z), Quaternion.Euler(90.0f, 0.0f, 0.0f)) as GameObject;
                gBackGround02.transform.localScale = new Vector3(1, 1, 3);
                GameObject gChild02 = gBackGround02.transform.Find("default").gameObject;
                gChild02.GetComponent<Renderer>().material = material;
                iObjectCount++;
            }

            // もう一個が定位置に来たら、片方を消去
            if (tTransform01.position.y >= 1650.0f)
            {
                Destroy(gBackGround01);
                iObjectCount--;
            }

        }

        // 有無の確認
        if (gBackGround02 != null) {
            // transformを取得
            tTransform02 = gBackGround02.transform;

            // 座標を取得
            Vector3 vPos02 = tTransform02.position;
            vPos02.y += fSpeed;

            // 速度を加算
            tTransform02.position = vPos02;

            // 一種類目を出す
            if (tTransform02.position.y >= 700.0f && iObjectCount < 2) {
                // 3個の背景からランダムに生成
                //iRandomElement = Random.Range(0, 2);
                gBackGround01 = Instantiate(gPrefab[iBackGroundLevel], new Vector3(vBackGroundPos.x, vBackGroundPos.y, vBackGroundPos.z), Quaternion.Euler(90.0f, 0.0f, 0.0f)) as GameObject;
                gBackGround01.transform.localScale = new Vector3(1, 1, 3);
                GameObject gChild01 = gBackGround01.transform.Find("default").gameObject;
                gChild01.GetComponent<Renderer>().material = material;
                iObjectCount++;

                Debug.Log("二個目生成2");
            }

            // もう一個が定位置に来たら、片方を消去
            if (tTransform02.position.y >= 1650.0f) {
                Destroy(gBackGround02);
                iObjectCount--;
            }

        }

    }
}
