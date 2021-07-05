using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNumberDisp : MonoBehaviour {

    [SerializeField] private int iDigit;
    [SerializeField] private int iNumber;
    [SerializeField] private Sprite[] number_words = new Sprite[11];
    private SpriteRenderer sr;

    private CNumberManager gNumberManager;

    void Start() {

        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = number_words[0];
        iNumber = 0;

        gNumberManager = GameObject.Find("PFB_Number").GetComponent<CNumberManager>();
    }

    void Update() {

        sr.sprite = number_words[iNumber];

        // :Ç»ÇÁÇ‚ÇÁÇ»Ç¢ÅB
        if (iDigit == -1) {
            iNumber = 10;
            return;
        }

        iNumber = gNumberManager.Get_iNumber(iDigit);

    }
}
