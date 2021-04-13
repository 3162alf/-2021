/*==============================================================================
    PROJECT ???
    [CRandomUi.cs]
    ・UIのランダム出現
--------------------------------------------------------------------------------
    2021.03.31 @Author kaito uchida
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
           
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRandomUi : MonoBehaviour{

    public GameObject[] oUi;//Uiを入れてる配列
    private int iNumber1;//ランダム数
    private int iNumber2;//ランダム数
    private int iNumber3;//ランダム数


    void Start(){

        iNumber1 = Random.Range(0, oUi.Length);//ランダム数を決める
        iNumber2 = Random.Range(0, oUi.Length);//ランダム数を決める
        iNumber3 = Random.Range(0, oUi.Length);//ランダム数を決める

        Instantiate(oUi[iNumber1], new Vector3(-15, 2, 0),Quaternion.Euler(90,0,-90));//Ui生成
        Instantiate(oUi[iNumber2], new Vector3(-15, 2, 5), Quaternion.Euler(90, 0, -90));//Ui生成
        Instantiate(oUi[iNumber3], new Vector3(-15, 2, 10), Quaternion.Euler(90, 0, -90));//Ui生成
    }

   
    void Upadate(){
        //ENTERでUIのパターンを変更
        if(Input.GetKey(KeyCode.Return)){
            iNumber1 = Random.Range(0, oUi.Length);
            iNumber2 = Random.Range(0, oUi.Length);
            iNumber3 = Random.Range(0, oUi.Length);

            Instantiate(oUi[iNumber1], new Vector3(-15, 2, 0), Quaternion.Euler(90, 0, -90));
            Instantiate(oUi[iNumber2], new Vector3(-15, 2, 5), Quaternion.Euler(90, 0, -90));
            Instantiate(oUi[iNumber3], new Vector3(-15, 2, 10), Quaternion.Euler(90, 0, -90));
        }
    }
}
