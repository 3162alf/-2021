using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CNamemanager : MonoBehaviour{
    
    public  int[] iSavename = new int[3];
    public int i = 0;

    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameHome = "Xbox_Home";    // ホームボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameA = "Xbox_A";    // ホームボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameB = "Xbox_B";    // Bボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameLB = "Xbox_LB";    // LBボタン
    [SerializeField, TooltipAttribute("ホームボタンの登録名")]
    private string stButtonNameRB = "Xbox_RB";    // RBボタン
    bool bIsEnd;
    [SerializeField] bool bIsYou;   // 特定の名前だけ入力させたいのでこのフラグがtrueの場合は入力可能にする
    [SerializeField] private AudioClip aSE01;
    AudioSource aAudioSource;

    void Start(){

        iSavename[0] = 0;
        iSavename[1] = 0;
        iSavename[2] = 0;

        bIsEnd = false;
       // bIsYou = false;
        aAudioSource = GetComponent<AudioSource>();
    }
    
    void Update(){

        // 名前入力を決定終了したら無駄にUpdateの処理をおこなわないようにreturnする。
        if (bIsEnd)
        {
            if(bIsYou)
            {
                // ホームボタンを押したらタイトルに戻るように遷移（自動でできるようにしたかったの。。。）
                if (Input.GetButtonDown(stButtonNameHome))
                {
                    aAudioSource.PlayOneShot(aSE01);
                    CSceneManager.SetRecently("TitleScene");
                    SceneManager.LoadScene("TitleScene");
                }
            }
            return;
        }

        if (bIsYou && !bIsEnd)
        {

            if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown(stButtonNameRB))
            {

                iSavename[i] += 1;

                if (iSavename[i] > 69)
                {

                    iSavename[i] = 0;

                }

            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown(stButtonNameLB))
            {

                iSavename[i] -= 1;

                if (iSavename[i] < 0)
                {

                    iSavename[i] = 69;

                }

            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown(stButtonNameB))
            {

                i += 1;

            }
        }

        if(Input.GetButtonDown(stButtonNameA))
        {
            bIsEnd = true;
        }
    }
}
