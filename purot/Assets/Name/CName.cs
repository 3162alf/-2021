using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CName : MonoBehaviour{

    [SerializeField] int iNamenumber;
    [SerializeField] public Sprite[] word = new Sprite[256];
    Image Img;
    public GameObject gObject;
    public CNameManager manager;
    
    void Start() {
        //gObject = GameObject.Find("PFB_Words");
        manager = gObject.GetComponent<CNameManager>();

        Img = gameObject.GetComponent<Image>();
        Img.sprite = word[manager.iSavename[iNamenumber]]; 
    }

    void Update(){

        Img.sprite = word[manager.iSavename[iNamenumber]];

    }
}
