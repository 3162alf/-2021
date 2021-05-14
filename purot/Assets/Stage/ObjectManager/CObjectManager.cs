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
    private List<GameObject> gAcceleList = new List<GameObject>();     // �I�u�W�F�N�g���X�g
    private List<OBJECT_SHAPE> CreateList = new List<OBJECT_SHAPE>();  // �������X�g

    [SerializeField] private GameObject[] gObjectSource;          // �I�u�W�F�N�g�\�[�X
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
            //if (iTimer >= fInterval / fAcceleSpeed + 30) {
            if (iTimer >= 60) {
                // �d�Ȃ�Ȃ��悤�ɐ����ʒu�𒲐�
                float sd = 180.0f;
                float rad = 9.0f;
                RotateState rs = RotateState.OUTSIDE;
                if (gObjectList.Count > 0) {
                    float ld = gObjectList[gObjectList.Count - 1].GetComponent<CRotateObject>().Get_fDegree();

                    if (ld >= 180 && ld <= 520) {
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
                cs.Set_fSpeed(fAcceleSpeed);
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
        if (GameObject.Find("PFB_Gate(Clone)") == null) {
            OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
            Sort(first);
        }
        //}

        Debug.Log(gObjectList.Count);
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
        if (!gObjectList.Contains(g)) {
            gObjectList.Add(g);
        }
    }

    // �I�u�W�F�N�g�폜�֐�
    public void Remove(GameObject g) {
        if (gObjectList.Contains(g)) {
            gObjectList.Remove(g);
        }
    }

    // �I�u�W�F�N�g�폜�֐�
    public void AcceleRemove(GameObject g) {
        if (gAcceleList.Contains(g)) {
            gAcceleList.Remove(g);
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

            g.GetComponent<CRotateObject>().Set_isAccele(true);
            g.GetComponent<CRotateObject>().Set_fSpeed(fAcceleSpeed);
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
        gAcceleList.Add(g);
        g.GetComponent<CRotateObject>().Set_isAccele(true);
        g.GetComponent<CRotateObject>().Set_fSpeed(fAcceleSpeed);

        // ���̃I�u�W�F�N�g�����������ċl�߂�
        for (int j = i; j < gObjectList.Count; j++) {
            gObjectList[j].GetComponent<CRotateObject>().Set_fSpeed(fAcceleSpeed);
        }
    }

    // �Ԋugetter
    public float Get_fInterval() {
        return fInterval;
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
        return gObjectList.IndexOf(g);
    }

    public int Get_iObjectNum() {
        return gObjectList.Count;
    }

    public float Get_fSpeed() {
        return fSpeed;
    }

    public float Get_fAcceleSpeed() {
        return fAcceleSpeed;
    }
}
