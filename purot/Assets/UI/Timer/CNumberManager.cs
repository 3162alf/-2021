using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNumberManager : MonoBehaviour {

    [SerializeField] private int[] l_iNumber;            // Œ…–ˆ‚Ì”’l‚ğ“ü‚ê‚Ä‚¨‚­
    [SerializeField] private int iDigitMillisecond = 3;  // •\¦‚·‚éŒ…”ƒ~ƒŠ•b
    [SerializeField] private int iDigitSeconds = 2;      // •\¦‚·‚éŒ…”•b
    [SerializeField] private int iDigitMinutes = 2;      // •\¦‚·‚éŒ…”•ª
    [SerializeField] private int i1Minutes = 60;         // ‰½•b‚Åˆê•ª‚©

    //[SerializeField] private int iDispNumber = 0;
    [SerializeField] private float fTime = 0.0f;         // deltaTime‚ğ‰ÁZ‚µ‚Ä‚¢‚­
    private int iTimeMillisecond;                        // ƒ~ƒŠ•b
    private int iTimeMinutes;                            // •ª


    void Start() {
        l_iNumber = new int[iDigitMillisecond + iDigitSeconds + iDigitSeconds];
        fTime = 0.0f;
        iTimeMillisecond = 0;
        iTimeMinutes = 0;
    }

    void Update() {
        // ƒ|[ƒY‰æ–Ê‚Ì‚ÉƒIƒuƒWƒFƒNƒg‚ª¶¬‚µ‚È‚¢‚æ‚¤‚Èˆ—
        if (Mathf.Approximately(Time.timeScale, 0f)) { return; }

        // ŠÔ‚ğŒv‘ª
        fTime += Time.deltaTime;

        // ®”‚ÉŒJ‚èã‚°
        iTimeMillisecond = (int)(fTime * 1000);

        // 1•ª‘«‚Á‚½‚ç•ª”‚ğ‘«‚µ‚Ä•b‚ğˆø‚­
        if (iTimeMillisecond >= i1Minutes * 1000) {
            fTime -= i1Minutes;
            iTimeMinutes++;
        }

        {
            int minutes = iTimeMinutes;
            // ƒ~ƒŠ•b
            for (int i = 0; i < iDigitMillisecond; i++) {
                l_iNumber[i] = iTimeMillisecond % 10;
                iTimeMillisecond /= 10;
            }
            // •b
            for (int i = 0; i < iDigitSeconds; i++) {
                l_iNumber[i + iDigitMillisecond] = iTimeMillisecond % 10;
                iTimeMillisecond /= 10;
            }
            // •ª
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
