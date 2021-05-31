/*==============================================================================
    Priject_Beta
    [CCursorManager.cs]
    �ECursor�ł̑I���V�X�e��

--------------------------------------------------------------------------------
    2021.04.26 @Author Hirano Tomoki
================================================================================
    History
        210525 Sasaki
            �|�[�Y��ʂ̎��ɃI�u�W�F�N�g����]���Ȃ��悤�ȏ����ǉ�(69~71�s��)

/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCursorManager : MonoBehaviour {

    //�X�e�B�b�N���͗p�ϐ�------------------------------------------------------
    [SerializeField, TooltipAttribute("���������p�̃X�e�B�b�N�̓o�^��")]
    private string stHorStickName = "Horizontal";
    [SerializeField, TooltipAttribute("���������p�̃X�e�B�b�N�̓o�^��")]
    private string stVerStickName = "Vertical";

    [SerializeField] private float fDeadZone = 0.01f;

    private float fHorizontal;                      // ���������̃X�e�B�b�N�̌X���i�[�ϐ�
    private float fVertivcal;                       // 

    private float fDeg;                             // �X�e�B�b�N�̌X������Z�o�����p�x�i�[�p�ϐ�
    private float fRad;                             // �X�e�B�b�N�̌X������Z�o�������W�A���p�i�[�p�ϐ�
    private float fOldDeg;                          // 1�t���[���O�̊p�x�L���p�ϐ�

    //--------------------------------------------------------------------------

    //--------------------------------------------------------------------------
    [SerializeField] private GameObject gCursor;              // ��������I�u�W�F�N�g
    [SerializeField] private float fCreateRad = 0f;         // "Lamp"�I�u�W�F�N�g�����Ɏg�p���锼�a

    private float fPosX = 0f;       // 
    private float fPosZ = 0f;       // 

    private Vector3 vCreatePos;     // 

    //--------------------------------------------------------------------------

    void Start() {
        fDeg = 0;
        fOldDeg = fDeg;

        // �I�u�W�F�N�g�𐶐�����ʒu���v�Z����
        fPosX = Mathf.Cos(Mathf.PI / 2) * fCreateRad;
        fPosZ = Mathf.Sin(Mathf.PI / 2) * fCreateRad;

        vCreatePos = new Vector3(fPosX, 1.0f, fPosZ);

        // Lamp�I�u�W�F�N�g�𐶐�
        gCursor = Instantiate(
            gCursor,
            vCreatePos,
            Quaternion.identity,
            transform
        ) as GameObject;
    }

    void Update() {
        if (Mathf.Approximately(Time.timeScale, 0f)){
            return;
        }
        // ���������Ɛ��������̃X�e�B�b�N�̌X�����擾
        fHorizontal = Input.GetAxis(stHorStickName);
        fVertivcal = Input.GetAxis(stVerStickName);

        // �X�e�B�b�N�̌��m�͈͂𒲐�
        if (fHorizontal < fDeadZone &&
            fHorizontal > -fDeadZone &&
            fVertivcal < fDeadZone &&
            fVertivcal > -fDeadZone) {
            fDeg = fOldDeg;
            return;
        }

        // �X�e�B�b�N�̌X�������W�A���p�ɕϊ�
        fDeg = Mathf.Atan2(fVertivcal, fHorizontal) * Mathf.Rad2Deg - 90;

        fOldDeg = fDeg;
        fRad = fDeg * Mathf.Deg2Rad;
    }

    // �ϊ���̃��W�A���p�̃Q�b�^�[
    public float Get_fRad() {
        return fRad;
    }

    // �ϊ���̊p�x�̃Q�b�^�[
    public int Get_iDeg() {
        return (int)fDeg;
    }

    public float Get_fCreateRad() {
        return fCreateRad;
    }
}
