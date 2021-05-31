using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CName : MonoBehaviour{

    [SerializeField] int iNamenumber;
    [SerializeField] public Sprite[] word = new Sprite[256];
    SpriteRenderer sr;
    public GameObject gObject;
    public CNameManager manager;
    
    void Start() {
        gObject = GameObject.Find("PFB_Words");
        manager = gObject.GetComponent<CNameManager>();
      
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = word[manager.iSavename[iNamenumber]]; 
    }

    void Update(){

        sr.sprite = word[manager.iSavename[iNamenumber]];

    }


}
