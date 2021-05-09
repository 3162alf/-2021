/*==============================================================================
    Project_HOGE
    [CLevelManager.cs]
    �E�V�X�e������
--------------------------------------------------------------------------------
    2021.05.09 @Author Suzuki Hayase
================================================================================
    History

        
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelManager : CSingletonMonoBehaviour<CLevelManager> {
    [SerializeField] private int iObjectNum;   // �I�u�W�F�N�g��

    private GameObject gTimer;                 // �^�C�}�[
    private int iFrame;                        // �t���[���J�E���^�[

    // Start is called before the first frame update
    void Start() {
        // �I�u�W�F�N�g�W�J
        OBJECT_SHAPE first = COrderManager.Instance.Get_Order(0);
        CObjectManager.Instance.AddCreateList(first);
        CObjectManager.Instance.Create(iObjectNum - 1);

        // �^�C�}�[�Q�b�g
        gTimer = GameObject.Find("PFB_TimerController");
    }

    // Update is called once per frame
    void Update() {
        // 1�����ƂɈ�ǉ�
        if (iObjectNum <= (int)OBJECT_SHAPE.MAX) {
            iFrame++;
            if (iFrame > 60 * 60) {
                iObjectNum++;
                CObjectManager.Instance.Create(1);
                iFrame = 0;
            }
        }
    }

    // �I�u�W�F�N�g��getter
    public int Get_iObjectNum() {
        return iObjectNum;
    }
}
