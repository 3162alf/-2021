/*==============================================================================
    Project_HOGE
    [COrderManager.cs]
    �E�w�ߐ���
--------------------------------------------------------------------------------
    2021.04.25 @Author Suzuki Hayase
================================================================================
    History
        
        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COrderManager : CSingletonMonoBehaviour<COrderManager> {
    [SerializeField] private GameObject[] gOrderSource;              // �w�߃I�u�W�F�N�g�z��
    [SerializeField] private GameObject gClearLamp;                  // �N���A�����v

    private List<OBJECT_SHAPE> OrderList = new List<OBJECT_SHAPE>();      // �w�߃��X�g
    private List<GameObject> gOrderList = new List<GameObject>();         // �w�߃I�u�W�F�N�g
    private List<GameObject> gClearLampList = new List<GameObject>();     // �N���A�����v�X�^���v

    // Start is called before the first frame update
    void Start() {
        //CreateOrder(3);
    }

    // Update is called once per frame
    void Update() {

    }

    // �w�ߐ�getter
    public int Get_iOrderNum() {
        return gOrderList.Count;
    }

    // �w�肵���ԍ��̎w��getter
    public OBJECT_SHAPE Get_Order(int id) {
        return OrderList[id];
    }

    // �w�肵���ԍ��̃N���A�����vgetter
    public GameObject Get_gClearLamp(int id) {
        return gClearLampList[id];
    }

    // �w�ߐ����֐�(����:�w�ߐ�)
    public void CreateOrder(int n) {
        // �w�߂̎��
        int kind = CLevelManager.Instance.Get_iObjectNum();

        // �O��̎w�߂�j��
        for (int i = 0; i < gOrderList.Count; i++) {
            Destroy(gOrderList[i]);
            Destroy(gClearLampList[i]);
        }

        // ���X�g������
        gOrderList.Clear();
        gClearLampList.Clear();
        OrderList.Clear();

        List<int> nums = new List<int>();

        for(int i = 0; i < kind; i++) {
            nums.Add(i);
        }

        // �w�߂𗐐��Ő���
        for (int i = 0; i < n; i++) {
            // ��������(0 �` nums.Count-1)
            int rand = Random.Range(0, nums.Count);

            OrderList.Add((OBJECT_SHAPE)nums[rand]);

            // �w�߂̏d�����Ȃ���
            nums.RemoveAt(rand);

            float st = 0;

            if(n % 2 == 0) {
                st = 2.5f + 5 * (n / 2.0f - 1);
            }
            else {
                st = 5 * (n / 2);
            }

            // �w�ߐ���
            gOrderList.Add(Instantiate(gOrderSource[(int)OrderList[i]],
                new Vector3(-18, 0, st - i * 5), Quaternion.Euler(0, 180, 0)));

            // �N���A�����v����
            gClearLampList.Add(Instantiate(gClearLamp,
                new Vector3(-20, 0, st - i * 5), Quaternion.Euler(0, 0, 0)));
        }
    }
}
