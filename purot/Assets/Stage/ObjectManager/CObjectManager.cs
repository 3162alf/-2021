/*==============================================================================
    Project_HOGE
    [CObjectManager.cs]
    �E�I�u�W�F�N�g�Ǘ�
--------------------------------------------------------------------------------
    2021.04.22 @Author Suzuki Hayase
================================================================================
    History
        210426 Suzuki
            �I�u�W�F�N�g�����Ԃ�l�߂�悤��
        210517 Ono
            ステート処理の調整
        210525 Sasaki
            ポーズ画面の時にオブジェクトが回転しないような処理追加(60~63行目)
        
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
    TRIANGLE,
    RAMIEL,
    STELLA,
    HOURGLASS,
    SATURN,
    TRIPLE,
    MAX
}

public class CObjectManager : CSingletonMonoBehaviour<CObjectManager> {
    [SerializeField] private List<GameObject> gObjectList = new List<GameObject>();     // �I�u�W�F�N�g���X�g
    [SerializeField] private List<GameObject> gAcceleList = new List<GameObject>();     // �I�u�W�F�N�g���X�g
    [SerializeField] private List<OBJECT_SHAPE> CreateList = new List<OBJECT_SHAPE>();  // �������X�g

    [SerializeField] private GameObject[] gObjectSource;          // �I�u�W�F�N�g�\�[�X
    [SerializeField] private float fInterval;                     // �z�u�Ԋu
    [SerializeField] private float fSpeed;                        // ��]�X�s�[�h
    [SerializeField] private float fAcceleSpeed;                  // ��������]�X�s�[�h
    [SerializeField] private float fInRadius;                     // ���蔼�a
    [SerializeField] private float fOutRadius;                    // �O��蔼�a

    private int iTimer = 0;                                       // �����Ԋu�^�C�}�[

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // ポーズ画面の時にオブジェクトが生成しないような処理
        if (Mathf.Approximately(Time.timeScale, 0f)) {
            return;
        }

        // オブジェクト生成
        if (CreateList.Count > 0) {
            if (iTimer >= 60) {
                // �d�Ȃ�Ȃ��悤�ɐ����ʒu�𒲐�
                float sd = 180.0f;
                float rad = 9.0f;
                RotateState rs = RotateState.OUTSIDE;
                if (gObjectList.Count > 0) {
                    float ld = gObjectList[gObjectList.Count - 1].GetComponent<CRotateObject>().Get_fDegree();

                    if (ld >= 190 && ld <= 520) {
                        sd = 180;
                        rad = fOutRadius;
                        rs = RotateState.OUTSIDE;
                    }
                    else {
                        sd = 520;
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
                cs.Set_fDegree(sd);
                cs.Set_Shape(CreateList[0]);
                cs.Set_State(rs);

                tmp.GetComponent<CRotateObject>().Set_isAccele(true);

                CreateList.RemoveAt(0);

                gAcceleList.Add(tmp);

                iTimer = 0;
            }
            iTimer++;
        }

        if (GameObject.Find("PFB_Gate(Clone)") == null) {
            OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
            Sort(first);
        }

        for (int i = 0; i < gObjectList.Count; i++) {
            gObjectList[i].GetComponent<CRotateObject>().UpdateObject();
        }
        for(int i = 0; i < gAcceleList.Count; i++) {
            gAcceleList[i].GetComponent<CRotateObject>().UpdateObject();
        }
    }

    // オブジェクト生成関数
    public void Create(int n) {
        // �I�u�W�F�N�g������_���ȏ����Ŕz�u
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

        for(int i = 0; i < gAcceleList.Count; i++) {
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

    // オブジェクトリストに追加
    public void Add(GameObject g) {
        if (!gObjectList.Contains(g)) {
            gObjectList.Add(g);
        }
    }

    // オブジェクトリストから削除
    public void Remove(GameObject g) {
        if (gObjectList.Contains(g)) {
            gObjectList.Remove(g);
        }
        if (gAcceleList.Contains(g)) {
            gAcceleList.Remove(g);
        }
    }

    public void Accele(GameObject g) {
        Remove(g);
        gAcceleList.Add(g);
    }

    public void Decelerate(GameObject g) {
        gAcceleList.Remove(g);
        Add(g);
    }

    public void AddCreateList(OBJECT_SHAPE first) {
        CreateList.Add(first);
    }

    // 指定したオブジェクトを先頭に
    public void Sort(OBJECT_SHAPE first) {
        // �擪�ɂ���I�u�W�F�N�g��index�擾
        int firstid = 0;
        GameObject firstobj = null;
        for (int i = 0; i < gObjectList.Count; i++) {
            if (gObjectList[i].GetComponent<CRotateObject>().Get_Shape() == first) {
                firstobj = gObjectList[i];
                firstid = i;
                break;
            }
        }

        if (firstid == 0) {
            return;
        }
        
        // �ז��ȃI�u�W�F�N�g������
        for (int i = 0; i < firstid; i++) {
            GameObject g = gObjectList[0];
            g.GetComponent<CRotateObject>().Set_isAccele(true);
            Accele(g);
        }
    }

    public void Inverse(GameObject g) {
        if (!g.GetComponent<CRotateObject>().Get_isInverse() &&
            !g.GetComponent<CRotateObject>().Get_isInverse2()) {
            // オブジェクトリストから削除
            int i;
            i = Get_iRanking(g);

            g.GetComponent<CRotateObject>().Set_isInverse(true);
            Accele(g);
            g.GetComponent<CRotateObject>().Set_isSort(true);

            // ���̃I�u�W�F�N�g����������ċl�߂�
            for (int j = i; j < gObjectList.Count;) {
                gObjectList[j].GetComponent<CRotateObject>().Set_isAccele(true);
                Accele(gObjectList[j]);
            }

            CRotateObject cs = g.GetComponent<CRotateObject>();
            cs.InvDegree();
        }
    }


    //// �I�u�W�F�N�g�����������
    //public void Accele(GameObject g) {
    //    // ����������
    //    g.GetComponent<CRotateObject>().Set_isAccele(true);
    //}

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
