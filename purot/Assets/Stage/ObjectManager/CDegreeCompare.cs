/*==============================================================================
    Project_HOGE
    [CDegreeCompare.cs]
    �E�p�x��r�N���X
--------------------------------------------------------------------------------
    2021.04.29 @Author Suzuki Hayase
================================================================================
    History

        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDegreeCompare : IComparer<GameObject> {

    public int Compare(GameObject a, GameObject b) {
        // �I�u�W�F�N�gA,B�̊p�x�擾
        float deg_a = a.GetComponent<CRotateObject>().Get_fDegree();
        float deg_b = b.GetComponent<CRotateObject>().Get_fDegree();

        // �p�x������
        if(deg_a == deg_b) {
            return 0;
        }


        GameObject g = CObjectManager.Instance.Get_gObject(0);

        float deg_s = 0;
        if (g != null) {
            // �I�u�W�F�N�g���X�g�̐擪�̃I�u�W�F�N�g�̊p�x�擾
            deg_s = g.GetComponent<CRotateObject>().Get_fDegree();
        }

        // �擪�I�u�W�F�N�g�̍�
        float dis_a = deg_a - deg_s;
        float dis_b = deg_b - deg_s;

        // ��r
        if(dis_a * dis_b < 0) {
            if(dis_a < dis_b) {
                return -1;
            }
            else {
                return 1;
            }
        }
        else {
            if (dis_a > dis_b) {
                return -1;
            }
            else {
                return 1;
            }
        }
    }
}
