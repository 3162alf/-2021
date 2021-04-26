/*==============================================================================
    Project_HOGE
    [CObjectManager.cs]
    �E�I�u�W�F�N�g�Ǘ�
--------------------------------------------------------------------------------
    2021.04.22 @Author Suzuki Hayase
================================================================================
    History

        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��]��Ԃ̗񋓌^
public enum RotateState {
    INSIDE,
    OUTSIDE
}

public enum OBJECT_SHAPE {                     // �I�u�W�F�N�g�̌`��
    CUBE = 0,                                  // �L���[�u
    SPHERE = 1,                                // ��
    CYLINDER = 2                               // �~��
}

public class CObjectManager : MonoBehaviour {
    private List<GameObject> gObjectList = new List<GameObject>(); // �I�u�W�F�N�g���X�g

    [SerializeField] private GameObject[] gObjectSource;  // �I�u�W�F�N�g�\�[�X
    [SerializeField] int iObjectNum;                      // �I�u�W�F�N�g��
    [SerializeField] float fStartRad;                     // �z�u�X�^�[�g�p�x
    [SerializeField] float fInterval;                     // �z�u�Ԋu
    [SerializeField] float fSpeed;                        // ��]�X�s�[�h

    // Start is called before the first frame update
    void Start() {
        // �I�u�W�F�N�g�������_���ȏ����Ŕz�u
        List<int> num = new List<int>();
        for(int i = 0; i < iObjectNum; i++) {
            num.Add(i);
        }

        for (int i = 0; i < iObjectNum; i++) {
            int rand = Random.Range(0, num.Count);
            Create(fStartRad + i * fInterval, (OBJECT_SHAPE)num[rand], RotateState.OUTSIDE);
            num.RemoveAt(rand);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    // �I�u�W�F�N�g�����֐�(����:�����ʒu�A�`��A�����O��)
    public void Create(float srad, OBJECT_SHAPE shape, RotateState rs) {
        float radius = (rs == RotateState.INSIDE ? 3.0f : 9.0f);

        // �����ʒu
        Vector3 pos = new Vector3(radius * Mathf.Sin(srad * Mathf.Deg2Rad),
                                  0.0f,
                                  radius * Mathf.Cos(srad * Mathf.Deg2Rad));


        // ����
        GameObject tmp = Instantiate(gObjectSource[(int)shape], pos,
            Quaternion.Euler(0, 0, 0));

        // �p�����[�^�Z�b�g
        CRotateObject cs = tmp.GetComponent<CRotateObject>();
        cs.Set_fSpeed(fSpeed);
        cs.Set_fStartDegree(srad);
        cs.Set_Shape(shape);

        gObjectList.Add(tmp);
    }

    // �S�I�u�W�F�N�g�̃X�s�[�h�ύX�֐�
    public void Set_fSpeed(float s) {
        for(int i = 0; i < gObjectList.Count; i++) {
            CRotateObject cs = gObjectList[i].GetComponent<CRotateObject>();
            cs.Set_fSpeed(s);
        }
    }

    // �I�u�W�F�N�g���X�ggetter
    public List<GameObject> Get_gObjectList() {
        return gObjectList;
    }
}
