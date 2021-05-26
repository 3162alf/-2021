using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CNamemanager : MonoBehaviour{
    
    public  int[] iSavename = new int[3];
    public int i = 0;
    
    void Start(){

        iSavename[0] = 0;
        iSavename[1] = 0;
        iSavename[2] = 0;

    }
    
    void Update(){
        
        if (Input.GetKeyDown(KeyCode.D)){
            
            iSavename[i] += 1;

            if (iSavename[i] > 69){
                
                iSavename[i] = 0;

            }

        }
        if (Input.GetKeyDown(KeyCode.A)){
           
            iSavename[i] -= 1;
           
            if (iSavename[i] < 0){
                
                iSavename[i] = 69;

            }

        }
        if (Input.GetKeyDown(KeyCode.Return)){
            
            i += 1;

        }

    }
}
