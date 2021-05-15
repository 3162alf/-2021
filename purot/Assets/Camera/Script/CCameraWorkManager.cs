/*==============================================================================
    Priject_Beta
    [CCameraWorkManager.cs]
    ・カメラワークのマネージャー

--------------------------------------------------------------------------------
    2021.05.12 @Author Hirano
================================================================================
    History
        20210513 hirano
            同じカメラワークが連続で実行されないように変更
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

// カメラワークパターンの列挙
public enum CAMERA_PATTERN {
    ROTATION_UP,
    ROTATION_DOWN,
    ROTATION_LEFT,
    ROTATION_RIGHT,
    MOVE_UP,
    MOVE_DOWN,
    MOVE_LEFT,
    MOVE_RIGHT,
    MOVE_FRONT,
    MOVE_BACK,
    POINT_UPLEFT,
    POINT_UPRIGHT,
    POINT_DOWNLEFT,
    POINT_DOWNRIGHT,
    DEFAULT
}

public class CCameraWorkManager : MonoBehaviour {

    private static readonly System.Random mRandom = new System.Random();

    [SerializeField] private CAMERA_PATTERN pattern;
    [SerializeField] private Camera cMoveCamera;
    [SerializeField] private GameObject gTarget;

    [SerializeField] private int iRotateLim;
    [SerializeField] private int iMoveLim;
    [SerializeField] private int iPointLim;
    [SerializeField] private int iMoveCoolTime;

    private CAMERA_PATTERN OldPattern;
    private int iCnt;

    void Start() {
        iCnt = 0;
        OldPattern = CAMERA_PATTERN.DEFAULT;
    }

    void Update() {
        switch (pattern) {
            case CAMERA_PATTERN.ROTATION_UP:
                cMoveCamera.GetComponent<CCameraWork>().RotationUp();
                OldPattern = CAMERA_PATTERN.ROTATION_UP;

                if (iCnt >= iRotateLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.ROTATION_DOWN:
                cMoveCamera.GetComponent<CCameraWork>().RotationDown();
                OldPattern = CAMERA_PATTERN.ROTATION_DOWN;

                if (iCnt >= iRotateLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.ROTATION_LEFT:
                cMoveCamera.GetComponent<CCameraWork>().RotationLeft();
                OldPattern = CAMERA_PATTERN.ROTATION_LEFT;

                if (iCnt >= iRotateLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.ROTATION_RIGHT:
                cMoveCamera.GetComponent<CCameraWork>().RotationRight();
                OldPattern = CAMERA_PATTERN.ROTATION_RIGHT;

                if (iCnt >= iRotateLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.MOVE_UP:
                cMoveCamera.GetComponent<CCameraWork>().MoveUp();
                OldPattern = CAMERA_PATTERN.MOVE_UP;

                if (iCnt >= iMoveLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.MOVE_DOWN:
                cMoveCamera.GetComponent<CCameraWork>().MoveDown();
                OldPattern = CAMERA_PATTERN.MOVE_UP;

                if (iCnt >= iMoveLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.MOVE_LEFT:
                cMoveCamera.GetComponent<CCameraWork>().MoveLeft();
                OldPattern = CAMERA_PATTERN.MOVE_UP;

                if (iCnt >= iMoveLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.MOVE_RIGHT:
                cMoveCamera.GetComponent<CCameraWork>().MoveRight();
                OldPattern = CAMERA_PATTERN.MOVE_UP;

                if (iCnt >= iMoveLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.MOVE_FRONT:
                cMoveCamera.GetComponent<CCameraWork>().MoveFront();
                OldPattern = CAMERA_PATTERN.MOVE_UP;

                if (iCnt >= iMoveLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.MOVE_BACK:
                cMoveCamera.GetComponent<CCameraWork>().MoveBack();
                OldPattern = CAMERA_PATTERN.MOVE_UP;

                if (iCnt >= iMoveLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.POINT_UPLEFT:
                cMoveCamera.GetComponent<CCameraWork>().PointUpLeft();
                OldPattern = CAMERA_PATTERN.POINT_UPLEFT;

                if (iCnt >= iPointLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.POINT_UPRIGHT:
                cMoveCamera.GetComponent<CCameraWork>().PointUpRight();
                OldPattern = CAMERA_PATTERN.POINT_UPRIGHT;

                if (iCnt >= iPointLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.POINT_DOWNLEFT:
                cMoveCamera.GetComponent<CCameraWork>().PointDownLeft();
                OldPattern = CAMERA_PATTERN.POINT_DOWNLEFT;

                if (iCnt >= iPointLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.POINT_DOWNRIGHT:
                cMoveCamera.GetComponent<CCameraWork>().PointDownRight();
                OldPattern = CAMERA_PATTERN.POINT_DOWNRIGHT;

                if (iCnt >= iPointLim) {
                    iCnt = 0;
                    Set_CameraPattern(CAMERA_PATTERN.DEFAULT);
                }
                break;

            case CAMERA_PATTERN.DEFAULT:
                cMoveCamera.GetComponent<CCameraWork>().Default();
                if (iCnt >= iMoveCoolTime) {
                    iCnt = 0;

                    do {
                        Set_CameraPattern(GetRandom<CAMERA_PATTERN>());
                    } while (pattern == OldPattern);
                }
                break;

            default: break;
        }

        iCnt++;
    }

    public static int GetTypeNum<T>() where T : struct {
        return Enum.GetValues(typeof(T)).Length;
    }

    public static T NoToType<T>(int targetNo) where T : struct {
        return (T)Enum.ToObject(typeof(T), targetNo);
    }

    public static T GetRandom<T>() where T : struct {
        int no = UnityEngine.Random.Range(0, GetTypeNum<T>() - 1);
        return NoToType<T>(no);
    }

    private void Set_CameraPattern(CAMERA_PATTERN newPattern) {
        pattern = newPattern;
    }
}
