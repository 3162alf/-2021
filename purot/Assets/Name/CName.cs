using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CName : MonoBehaviour{

    [SerializeField] int iNamenumber;
    [SerializeField] public Sprite[] word = new Sprite[256];
    SpriteRenderer sr;
    public CNamemanager manager;
    
    void Start() {
        manager = transform.parent.gameObject.GetComponent<CNamemanager>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = word[manager.iSavename[iNamenumber]]; 
    }

    void Update(){

        sr.sprite = word[manager.iSavename[iNamenumber]];

    }


}
