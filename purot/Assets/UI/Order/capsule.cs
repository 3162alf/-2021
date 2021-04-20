using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class capsule : MonoBehaviour
{

    bool Cubeflag = false;
    bool Cylinderflag = false;


    List<GameObject> colList = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        


        if (col.gameObject)
        {
            colList.Add(col.gameObject);
            
           // Debug.Log("flag");
        }
        if(colList.Count==1)
        {
            foreach (GameObject checkObj in colList)
            {
                if (checkObj.name == "Sphere")
                {
                    Debug.Log("clear1");
                }
                else
                {
                    Debug.Log("out");
                }
            }
        }
        if (colList.Count == 2)
        {
            foreach (GameObject checkObj in colList)
            {
                if (checkObj.name == "Cube")
                {
                    Debug.Log("clear2");
                }
                else
                {
                    Debug.Log("out");
                }
            }
        }

        if (colList.Count == 3)
        {
            foreach (GameObject checkObj in colList)
            {
                if (checkObj.name == "Cylinder")
                {
                    Debug.Log("clear3");
                }
                else
                {
                    Debug.Log("out");
                }
            }
        }



    }

}
