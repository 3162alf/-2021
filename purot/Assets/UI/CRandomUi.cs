/*==============================================================================
    PROJECT ???
    [CRandomUi.cs]
    �EUI�̃����_���o��
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

    public GameObject[] oUi;//Ui�����Ă�z��
    private int iNumber1;//�����_����
    private int iNumber2;//�����_����
    private int iNumber3;//�����_����


    void Start(){

        iNumber1 = Random.Range(0, oUi.Length);//�����_���������߂�
        iNumber2 = Random.Range(0, oUi.Length);//�����_���������߂�
        iNumber3 = Random.Range(0, oUi.Length);//�����_���������߂�

        Instantiate(oUi[iNumber1], new Vector3(-15, 2, 0),Quaternion.Euler(90,0,-90));//Ui����
        Instantiate(oUi[iNumber2], new Vector3(-15, 2, 5), Quaternion.Euler(90, 0, -90));//Ui����
        Instantiate(oUi[iNumber3], new Vector3(-15, 2, 10), Quaternion.Euler(90, 0, -90));//Ui����
    }

   
    void Upadate(){
        //ENTER��UI�̃p�^�[����ύX
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
