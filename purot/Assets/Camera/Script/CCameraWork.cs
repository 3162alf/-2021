/*==============================================================================
    Priject_Beta
    [CCameraWork.cs]
    ・カメラワークパターンの数種類

--------------------------------------------------------------------------------
    2021.05.12 @Author Hirano
================================================================================
    History
        20210513 hirano
            カメラワークの開始位置を調整

/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraWork : MonoBehaviour {

    [SerializeField] private GameObject gCameraManager;
    [SerializeField] private GameObject gTarget;

    private Vector3 vDefaultPos;
    private Vector3 vMovePos;
    private Vector3 vStartPos;

    private bool isChange;

    [SerializeField] private Vector3 vPointPos;

    [SerializeField] private float fRadius;
    [SerializeField] private float fRotateSpeed;
    [SerializeField] private float fMoveSpeed;

    void Start() {
        vDefaultPos = this.transform.position;
        vMovePos = this.transform.position;
        vStartPos = this.transform.position;

        isChange = false;
    }

    void Update() {

    }

    public void RotationUp() {
        this.transform.RotateAround(gTarget.transform.position, new Vector3(1.0f, 0.0f, 0.0f), fRotateSpeed);
    }

    public void RotationDown() {
        this.transform.RotateAround(gTarget.transform.position, new Vector3(1.0f, 0.0f, 0.0f), -fRotateSpeed);
    }

    public void RotationLeft() {
        this.transform.RotateAround(gTarget.transform.position, new Vector3(0.0f, 0.0f, 1.0f), fRotateSpeed);
    }

    public void RotationRight() {
        this.transform.RotateAround(gTarget.transform.position, new Vector3(0.0f, 0.0f, 1.0f), -fRotateSpeed);
    }

    public void MoveUp() {
        if (!isChange) {
            isChange = true;
            vStartPos.x = -15.0f;
            vStartPos.z = -8.0f;
            vMovePos = vStartPos;
        }
        vMovePos.z += fMoveSpeed;
        this.transform.position = vMovePos;
    }

    public void MoveDown() {
        if (!isChange) {
            isChange = true;
            vStartPos.x = 15.0f;
            vStartPos.z = 8.0f;
            vMovePos = vStartPos;
        }
        vMovePos.z -= fMoveSpeed;
        this.transform.position = vMovePos;
    }

    public void MoveLeft() {
        if (!isChange) {
            isChange = true;
            vStartPos.x = 15.0f;
            vStartPos.z = -8.0f;
            vMovePos = vStartPos;
        }
        vMovePos.x -= fMoveSpeed * 1.75f;
        this.transform.position = vMovePos;
    }

    public void MoveRight() {
        if (!isChange) {
            isChange = true;
            vStartPos.x = -15.0f;
            vStartPos.z = 8.0f;
            vMovePos = vStartPos;
        }
        vMovePos.x += fMoveSpeed * 1.75f;
        this.transform.position = vMovePos;
    }

    public void MoveFront() {
        vMovePos.y -= fMoveSpeed;
        this.transform.position = vMovePos;
    }

    public void MoveBack() {
        vMovePos.y += fMoveSpeed;
        this.transform.position = vMovePos;
    }

    public void PointUpLeft() {
        vMovePos = new Vector3(-vPointPos.x, vPointPos.y, vPointPos.z);
        this.transform.position = vMovePos;
        this.transform.rotation = Quaternion.Euler(60, 120, 120);
    }

    public void PointUpRight() {
        vMovePos = new Vector3(vPointPos.x, vPointPos.y, vPointPos.z);
        this.transform.position = vMovePos;
        this.transform.rotation = Quaternion.Euler(120, 60, 60);
    }

    public void PointDownLeft() {
        vMovePos = new Vector3(-vPointPos.x, vPointPos.y, -vPointPos.z);
        this.transform.position = vMovePos;
        this.transform.rotation = Quaternion.Euler(60, 50, 60);
    }

    public void PointDownRight() {
        vMovePos = new Vector3(vPointPos.x, vPointPos.y, -vPointPos.z);
        this.transform.position = vMovePos;
        this.transform.rotation = Quaternion.Euler(60, -50, -60);
    }

    public void Default() {
        vMovePos = vDefaultPos;
        vStartPos = vDefaultPos;
        isChange = false;
        this.transform.position = vDefaultPos;
        this.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
