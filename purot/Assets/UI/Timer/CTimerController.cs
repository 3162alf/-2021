/*==============================================================================
    PROJECT ???
    [CTimerController.cs]
    �E�����N���A����܂ł̐�������
--------------------------------------------------------------------------------
    2021.03.25 @Author Kaname Ota
================================================================================
    History
        2021.04.10 @Tsubasa Ono
            制限時間が0になったらリザルト画面に遷移するように変更
            
/*============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CTimerController : MonoBehaviour {
    public Text tTimer;          // �e�L�X�g�I�u�W�F�N�g(Timer)��i�[����

    [SerializeField]
    private float fTotalTime;   // ��������
    private int iSecond = 0;    // �e�L�X�g�ɕ\������b��

    void Start() {
        // �������Ԃ̍ő�l�B�Ƃ肠������
        if (fTotalTime > 30.0f) {
            fTotalTime = 29.9f;
        }
    }

    void Update() {
        // �t���[�����Ƃ̕b������Z 
        fTotalTime -= Time.deltaTime;

        // �������Ԃ�int�ŃL���X�g���ĕb����Z�o
        iSecond = (int)fTotalTime;

        // �b���𕶎���ɂ��ĕ\��
        tTimer.text = iSecond.ToString("00");

        /* �������Ԃ��Ȃ��Ȃ�����\���Ɛ������Ԃ�0�ɌŒ�
        if (iSecond <= 0) {
            tTimer.text = "00";
            fTotalTime = 0.0f;
        }*/

        // 制限時間が0になったらリザルト画面へ遷移
        if (iSecond == 0) {
            SceneManager.LoadScene("Result");
        }

    }
}
