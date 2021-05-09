/*==============================================================================
    Project_HOGE
    [CObjectManager.cs]
    �E�I�u�W�F�N�g�Ǘ�
--------------------------------------------------------------------------------
    2021.04.22 @Author Suzuki Hayase
================================================================================
    History
        210426 Suzuki
            �I�u�W�F�N�g�����Ԃ��l�߂�悤��
        
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
    CUBE,
    SPHERE,
    TORUS,
    RAMIEL,
    STELLA,
    HOURGLASS,                
    SATURN,
    ATOM,
    MAX
}

public class CObjectManager : CSingletonMonoBehaviour<CObjectManager> {
    private List<GameObject> gObjectList = new List<GameObject>();     // �I�u�W�F�N�g���X�g
    private List<OBJECT_SHAPE> CreateList = new List<OBJECT_SHAPE>();  // �������X�g
    private List<GameObject> gAcceleList = new List<GameObject>();     // �������̃I�u�W�F�N�g���X�g

    [SerializeField] private GameObject[] gObjectSource;          // �I�u�W�F�N�g�\�[�X
    [SerializeField] private float fFirstDegree;                  // �擪�̊p�x
    [SerializeField] private float fInterval;                     // �z�u�Ԋu
    [SerializeField] private float fSpeed;                        // ��]�X�s�[�h
    [SerializeField] private float fAcceleSpeed;                  // ��������]�X�s�[�h
    [SerializeField] private float fInRadius;                     // ����蔼�a
    [SerializeField] private float fOutRadius;                    // �O��蔼�a

    private int iTimer = 0;                                       // �����Ԋu�^�C�}�[

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // ���X�g�ɂ���I�u�W�F�N�g�𐶐�
        if (CreateList.Count > 0) {
            // ���̊Ԋu���J���Đ�������
            if (iTimer >= fInterval / fAcceleSpeed) {
                // �d�Ȃ�Ȃ��悤�ɐ����ʒu�𒲐�
                float sd = 180.0f;
                float rad = 9.0f;
                RotateState rs = RotateState.OUTSIDE;
                if (gObjectList.Count > 0) {
                    float ld = gObjectList[gObjectList.Count - 1].GetComponent<CRotateObject>().Get_fDegree();

                    if (ld >= 360 && ld < 720 + fInterval) {
                        sd = 180;
                        rad = fOutRadius;
                        rs = RotateState.OUTSIDE;
                    }
                    else {
                        sd = 540;
                        rad = fInRadius;
                        rs = RotateState.INSIDE;
                    }
                }
                Vector3 pos = new Vector3(rad * Mathf.Sin((sd + 180) * Mathf.Deg2Rad),
                                          0.0f,
                                          rad * Mathf.Cos((sd + 180) * Mathf.Deg2Rad));

                // ����
                GameObject tmp = Instantiate(gObjectSource[(int)CreateList[0]], pos,
                    Quaternion.Euler(0, 0, 0));

                // �p�����[�^�Z�b�g
                CRotateObject cs = tmp.GetComponent<CRotateObject>();
                cs.Set_fSpeed(fSpeed);
                cs.Set_fAcceleSpeed(fAcceleSpeed);
                cs.Set_fDegree(sd);
                cs.Set_Shape(CreateList[0]);
                cs.Set_State(rs);

                Accele(tmp);
                CreateList.RemoveAt(0);

                iTimer = 0;
            }
            iTimer++;
        }
        else {
            // ����������Ƃ��̎��Ԃ�Z�k
            //iTimer = (int)(fInterval / fSpeed) + 10;
        }

        // �w�߂̍ŏ��̃I�u�W�F�N�g���擪�ɂȂ�悤��
        //if (gAcceleList.Count == 0 && CreateList.Count == 0) {
        OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
        Sort(first);
        //}

        if(gObjectList.Count > 0) {
            fFirstDegree = gObjectList[0].GetComponent<CRotateObject>().Get_fDegree();
        }
    }

    // �I�u�W�F�N�g�����֐�(����:�����ʒu�A�`��A�����O��)
    public void Create(int n) {
        // �I�u�W�F�N�g�������_���ȏ����Ŕz�u
        List<int> nums = new List<int>();
        for (int i = 0; i < CLevelManager.Instance.Get_iObjectNum(); i++) {
            nums.Add(i);
        }

        // �d�����Ȃ��悤��
        for (int i = 0; i < CreateList.Count; i++) {
            nums.Remove((int)CreateList[i]);
        }

        for (int i = 0; i < gObjectList.Count; i++) {
            CRotateObject cro = gObjectList[i].GetComponent<CRotateObject>();
            nums.Remove((int)cro.Get_Shape());
        }

        for (int i = 0; i < gAcceleList.Count; i++) {
            CRotateObject cro = gAcceleList[i].GetComponent<CRotateObject>();
            nums.Remove((int)cro.Get_Shape());
        }

        // �������X�g�ɒǉ�
        for (int i = 0; i < n; i++) {
            int rand = Random.Range(0, nums.Count);
            CreateList.Add((OBJECT_SHAPE)nums[rand]);
            nums.RemoveAt(rand);
        }
    }

    // �I�u�W�F�N�g�ǉ��֐�
    public void Add(GameObject g) {
        gObjectList.Add(g);
        gAcceleList.Remove(g);

        CDegreeCompare dc = new CDegreeCompare();
        gObjectList.Sort(dc);
    }

    // �I�u�W�F�N�g�폜�֐�
    public void Remove(GameObject g) {
        gObjectList.Remove(g);
        gAcceleList.Remove(g);
        for (int i = 0; i < gAcceleList.Count; i++) {
            gAcceleList[i].GetComponent<CRotateObject>().Set_iRanking(gObjectList.Count + i);
        }
    }

    public void AddCreateList(OBJECT_SHAPE first) {
        CreateList.Add(first);
    }

    // �w�肵���I�u�W�F�N�g���擪�ɗ���悤�ɕ��בւ���
    public void Sort(OBJECT_SHAPE first) {
        // �擪�ɂ���I�u�W�F�N�g��index�擾
        int firstid = 0;
        for (int i = 0; i < gObjectList.Count; i++) {
            if (gObjectList[i].GetComponent<CRotateObject>().Get_Shape() == first) {
                firstid = i;
                break;
            }
        }

        if (firstid == 0) {
            return;
        }

        // �ז��ȃI�u�W�F�N�g�������
        for (int i = 0; i < firstid; i++) {
            GameObject g = gObjectList[0];
            gObjectList.Remove(g);
            gAcceleList.Add(g);

            g.GetComponent<CRotateObject>().Set_isAccele(true);
        }

        for (int i = 0; i < gAcceleList.Count; i++) {
            gAcceleList[i].GetComponent<CRotateObject>().Set_iRanking(gObjectList.Count + i);
        }
    }

    public void RankSort() {
        CDegreeCompare dc = new CDegreeCompare();
        gAcceleList.Sort(dc);

        for (int i = 0; i < gAcceleList.Count; i++) {
            gAcceleList[i].GetComponent<CRotateObject>().Set_iRanking(gObjectList.Count + i);
        }
    }

    // �I�u�W�F�N�g������������
    public void Accele(GameObject g) {
        // �I�u�W�F�N�g���X�g����폜
        int i;
        for (i = 0; i < gObjectList.Count; i++) {
            if (gObjectList[i] == g) {
                gObjectList.RemoveAt(i);
                break;
            }
        }

        // ����������
        g.GetComponent<CRotateObject>().Set_isAccele(true);
        gAcceleList.Add(g);

        // ���̃I�u�W�F�N�g�����������ċl�߂�
        for (int j = i; j < gObjectList.Count;) {
            gObjectList[j].GetComponent<CRotateObject>().Set_isAccele(true);
            gAcceleList.Add(gObjectList[j]);
            gObjectList.RemoveAt(j);
        }

        RankSort();
    }

    // �Ԋugetter
    public float Get_fInterval() {
        return fInterval;
    }

    // �S�I�u�W�F�N�g�̃X�s�[�h�ύX�֐�
    public void Set_fSpeed(float s) {
        for (int i = 0; i < gObjectList.Count; i++) {
            CRotateObject cs = gObjectList[i].GetComponent<CRotateObject>();
            cs.Set_fSpeed(s);
        }
    }

    // �I�u�W�F�N�g���X�ggetter
    public List<GameObject> Get_gObjectList() {
        return gObjectList;
    }

    // ����̃I�u�W�F�N�ggetter
    public GameObject Get_gObject(int id) {
        if (gObjectList.Count <= id) {
            return null;
        }
        else {
            return gObjectList[id];
        }
    }

    // ����getter
    public int Get_iRanking(GameObject g) {
        for (int i = 0; i < gObjectList.Count; i++) {
            if (gObjectList[i] == g) {
                return i;
            }
        }
        return -1;
    }

    // �擪�̊p�x
    public float Get_fFirstDegree() {
        return fFirstDegree;
    }

    public int Get_iObjectNum() {
        return gObjectList.Count;
    }
}
