/*==============================================================================
    Priject_Beta
    [CCursorManager.cs]
    �ECursor�\���A�ړ�����

--------------------------------------------------------------------------------
    2021.04.26 @Author Hirano Tomoki
================================================================================
    History
        20210502 Hirano
            SphereCast�ǉ�
        20210515 Misaki Sasaki
            ����ւ����ɃG�t�F�N�g�ł�悤�ɂ��Ă܂��B
        20210525 Sasaki
            �|�[�Y��ʂ̎��ɃI�u�W�F�N�g����]���Ȃ��悤�ȏ����ǉ�(83~85�s��)
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCursorController : MonoBehaviour {
    //Ray����p�ϐ�-------------------------------------------------------------
    private Ray rRay;                   // �ʏ��ray
    private Ray rSphereRay;             // SphereCast�p��ray
    private RaycastHit[] rhHits;        // �ʏ��ray�p�̓����蔻��I�u�W�F�N�g�i�[�p�ϐ�
    private RaycastHit[] rhSphereHits;  // SphereCast�p�̓����蔻��I�u�W�F�N�g�i�[�p�ϐ�

    // SphereCast�̑傫����ς���ϐ�
    // �������蔻�肪����������ꍇ�͏������傫���A�ɂ�����ꍇ�͏��������������Ă��������B��{��0.5�ł��傤�ǂ����Ǝv���܂��A�����B
    [SerializeField] private float fSphereCastRadius = 0.5f;

    [SerializeField] private int iDistance = 10;                // ray�ASphereCast�̍ő勗��(���X�e�[�W���L�������ꍇ�͂���ɍ��킹�đ傫�����Ă�������)
    [SerializeField] private string stTagName = "RotateObject"; // ray�ASphereCast�ƏՓˏ������s���I�u�W�F�N�g��tag(����]�I�u�W�F�N�g���ׂĂɐݒ肵�Ă�������)
    [SerializeField] private LayerMask lmLayerMask;             // ray�ASphereCast�ƏՓˏ������s���I�u�W�F�N�g��z�u���郌�C���[(���Փˏ���������s�������I�u�W�F�N�g���ׂĂ����̃��C���[�ɂ����Ă�������)


    private GameObject gCursorManager;    // LampManager�̃I�u�W�F�N�g���i�[
    private Vector3 vMovePos;

    // �ȉ��ł́A[���A�~�A�Z�A��]�{�^���̓��͗p�ϐ���p�ӂ��Ă��܂����A
    // ����ł́A�Z�{�^���ȊO�̓��͏����͕K�v�Ȃ��̂ŁA�R�����g�A�E�g���Ă��܂��B
    // �K�v�ɂȂ����炻�̓s�x�O���܂��B

    //[SerializeField, TooltipAttribute("A�{�^���̓o�^��")]
    //private string stButton0Name = "joystickbutton0";  // A�{�^��
    [SerializeField, TooltipAttribute("B�{�^���̓o�^��")]
    private string stButton1Name = "joystickbutton1";  // B�{�^��
    //[SerializeField, TooltipAttribute("X�{�^���̓o�^��")]
    //private string stButton2Name = "joystickbutton2";    // X�{�^��
    //[SerializeField, TooltipAttribute("Y�{�^���̓o�^��")]
    //private string stButton3Name = "joystickbutton3";  // Y�{�^��

    // ����ւ����Ƀ��[�v�z�[�����o�����߂ɒǉ� ---2020/5/15 ���X��
    [SerializeField] public GameObject PAR;    // �p�[�e�B�N���{�̂��i�[
    [SerializeField] public GameObject PAR_1;    // �p�[�e�B�N���{�̂��i�[
    private GameObject pParticleObject = default;
    private GameObject pParticleObject_1 = default;
    private ParticleSystem pParticleSystem = default;
    private ParticleSystem pParticleSystem_1 = default;

    //--------------------------------------------------------------------------

    void Start() {
        // "PFB_LampManager"���擾����gLampManager�Ɋi�[
        gCursorManager = GameObject.Find("PFB_CursorManager");
        vMovePos.y = 0.0f;

        // �F��ԐF�ɕύX����B
        GetComponent<Renderer>().material.color = Color.red;

        // �p�[�e�B�N���𐶐�
        pParticleObject = (GameObject)Instantiate(PAR);
        pParticleObject_1 = (GameObject)Instantiate(PAR_1);
        // �p�[�e�B�N������p�ɃR���|�[�l���g���擾
        pParticleSystem = pParticleObject.GetComponent<ParticleSystem>();
        pParticleSystem_1 = pParticleObject_1.GetComponent<ParticleSystem>();
        // �}�Ƀp�[�e�B�N�����Đ�����邱�Ƃ��Ȃ��悤�ɗ\�ߒ�~������
        pParticleSystem.Stop();
        pParticleSystem_1.Stop();
    }

    void Update() {

        if (Mathf.Approximately(Time.timeScale, 0f)){
            return;
        }
        // �ړ��p�̌v�Z
        vMovePos.x = Mathf.Cos(gCursorManager.GetComponent<CCursorManager>().Get_fRad() + (Mathf.PI / 2)) * gCursorManager.GetComponent<CCursorManager>().Get_fCreateRad();
        vMovePos.z = Mathf.Sin(gCursorManager.GetComponent<CCursorManager>().Get_fRad() + (Mathf.PI / 2)) * gCursorManager.GetComponent<CCursorManager>().Get_fCreateRad();

        transform.position = new Vector3(vMovePos.x, vMovePos.y, vMovePos.z);

        if (Input.GetButtonDown(stButton1Name) || Input.GetKeyDown(KeyCode.Space)) {
            CreateSphereCast(gCursorManager.transform.position, this.transform.position);
        }
        // debug�p(�����Ă���ԁAray���΂�������)
        // ray�̒����Ȃǂ��m�F�������ꍇ�͓��͏�����������ɐ؂�ւ��Ă��������B
        //if (Input.GetButton(stButton1Name) || Input.GetKey(KeyCode.Return)) {
        //    CreateRay(gCursorManager.transform.position, this.transform.position);
        //}

        // ���S����������
        gCursorManager.transform.LookAt(this.transform);
    }

    // Ray���΂��֐�(�Փ˔��荞��)
    private void CreateRay(Vector3 vPos, Vector3 vDir) {

        // ray�̐����ʒu�ƕ������w��
        rRay = new Ray(vPos, vDir);
        // ray�̉���
        Debug.DrawRay(rRay.origin, rRay.direction * iDistance, Color.red);

        // ray���΂��A�Փ˂��Ă���I�u�W�F�N�g�����ׂĒT���B
        // RaycastAll(rRay : ���_�A��΂�����, iDistance : ����, lmLayerMask : �Փˏ������s�����C���[�𐧌�)
        rhHits = Physics.RaycastAll(rRay, iDistance, lmLayerMask);
        foreach (RaycastHit rhHitObject in rhHits) {
            Debug.Log("Ray��" + stTagName + "�ɓ�������");

            // �Ђ�����Ԃ�����
            if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.OUTSIDE) {
                rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.INSIDE);
            }
            else if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.INSIDE) {
                rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.OUTSIDE);
            }
        }
    }

    // 20210502_Hirano
    // SphereCast���΂��֐�
    private void CreateSphereCast(Vector3 vPos, Vector3 vDir) {

        // ray�̐����ʒu�ƕ������w��
        rSphereRay = new Ray(vPos, vDir);
        // ray�̉���
        Debug.DrawRay(rSphereRay.origin, rSphereRay.direction * iDistance, Color.red);

        // SphereCast���΂��A�Փ˂��Ă���I�u�W�F�N�g�����ׂĒT���B
        // SpereCastAll( rSphereRay : ���_�A��΂������@, fSphereCastRadius : SphereCast�̑傫���@, iDistance : ray�̍ő勗���@, lmLayerMask : �Փˏ������s�����C���[)
        rhSphereHits = Physics.SphereCastAll(rSphereRay, fSphereCastRadius, iDistance, lmLayerMask);
        foreach (RaycastHit rhHitObject in rhSphereHits) {
            Debug.Log("SphereCast��" + stTagName + "�ɓ�������");

            if (!rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_isAccele()) {
                if (COrderManager.Instance.Get_Order(0) != rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_Shape()) {

                    // �p�[�e�B�N���Đ�
                    pParticleObject.transform.position = new Vector3(rhHitObject.transform.position.x, rhHitObject.transform.position.y + 1.0f, rhHitObject.transform.position.z);
                    pParticleObject_1.transform.position = new Vector3(rhHitObject.transform.position.x, rhHitObject.transform.position.y + 1.0f, rhHitObject.transform.position.z);
                    pParticleSystem.Play();
                    pParticleSystem_1.Play();

                    // �Ђ�����Ԃ�����
                    if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.OUTSIDE) {
                        rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.INSIDE);
                    }
                    else if (rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Get_RotateState() == RotateState.INSIDE) {
                        rhHitObject.collider.gameObject.GetComponent<CRotateObject>().Set_State(RotateState.OUTSIDE);

                    }
                    CObjectManager.Instance.Accele(rhHitObject.collider.gameObject);
                }
            }
        }
    }
}
