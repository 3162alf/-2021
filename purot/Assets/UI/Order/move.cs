using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float speed;
    private float radiusx;
    private float radiusz;
    float x;
    float y;
    float z;


    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        radiusx = 9.0f;//muki
        radiusz = -9.0f;

    }

    // Update is called once per frame
    void Update()
    {
        x = radiusx * Mathf.Sin(Time.time * speed);
        y = 0.0f;
        z = radiusz * Mathf.Cos(Time.time * speed);


        transform.position = new Vector3(x, y, z);

    }
}
