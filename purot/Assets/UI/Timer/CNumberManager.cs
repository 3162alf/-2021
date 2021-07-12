using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNumberManager : MonoBehaviour {

    [SerializeField] private int[] l_iNumber;            // �����̐��l�����Ă���
    [SerializeField] private int iDigitMillisecond = 3;  // �\�����錅���~���b
    [SerializeField] private int iDigitSeconds = 2;      // �\�����錅���b
    [SerializeField] private int iDigitMinutes = 2;      // �\�����錅����
    [SerializeField] private int i1Minutes = 60;         // ���b�ňꕪ��

    //[SerializeField] private int iDispNumber = 0;
    [SerializeField] private float fTime = 0.0f;         // deltaTime�����Z���Ă���
    private int iTimeMillisecond;                        // �~���b
    private int iTimeMinutes;                            // ��


    void Start() {
        l_iNumber = new int[iDigitMillisecond + iDigitSeconds + iDigitSeconds];
        fTime = 0.0f;
        iTimeMillisecond = 0;
        iTimeMinutes = 0;
    }

    void Update() {
        // �|�[�Y��ʂ̎��ɃI�u�W�F�N�g���������Ȃ��悤�ȏ���
        if (Mathf.Approximately(Time.timeScale, 0f)) { return; }

        // ���Ԃ��v��
        fTime += Time.deltaTime;

        // �����ɌJ��グ
        iTimeMillisecond = (int)(fTime * 1000);

        // 1���������番���𑫂��ĕb������
        if (iTimeMillisecond >= i1Minutes * 1000) {
            fTime -= i1Minutes;
            iTimeMinutes++;
        }

        {
            int minutes = iTimeMinutes;
            // �~���b
            for (int i = 0; i < iDigitMillisecond; i++) {
                l_iNumber[i] = iTimeMillisecond % 10;
                iTimeMillisecond /= 10;
            }
            // �b
            for (int i = 0; i < iDigitSeconds; i++) {
                l_iNumber[i + iDigitMillisecond] = iTimeMillisecond % 10;
                iTimeMillisecond /= 10;
            }
            // ��
            for (int i = 0; i < iDigitMinutes; i++) {
                l_iNumber[i + iDigitMillisecond + iDigitSeconds] = minutes % 10;
                minutes /= 10;
            }
        }

        //for (int i = 0; i < 7; i++) {
        //    Debug.Log(l_iNumber[i]);
        //}

    }

    public int Get_iNumber(int iDigit) {
        return l_iNumber[iDigit];
    }

    public float Get_fTime() {
        return fTime;
    }
}
