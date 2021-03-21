using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    GameObject Cube;
    private float radius;

    float x;
    float y;
    float z;
    float startposition;
    float position;
    float speed;
    Vector3 pos;
    Vector3 tmp;

    // Start is called before the first frame update
    void Start()
    {
        radius = 9.0f;
        //startposition = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
        speed += 0.01f;
        position =speed+startposition;
       
        float T = 8.0f;
        float f = 1.0f / T;

        x = radius * Mathf.Sin(2 * Mathf.PI * f * position);
        y = 0.0f;
        z = -radius * Mathf.Cos(2f * Mathf.PI * f * position);
        
        transform.position = new Vector3(x, y, z);

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="switch1")
        {

            radius = 3.0f;

        }
        if (col.gameObject.tag == "switch2")
        {
            //startposition = 0;
            speed = 0;
           // transform.position = new Vector3(0.0f, 0.0f, -9.0f);
            radius = 9.0f;
            
        }
    }



}  
    
   

