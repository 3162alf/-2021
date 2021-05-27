using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateTrail : MonoBehaviour {


    [SerializeField] private GameObject[] obj;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Create() {
        for (int i = 0; i < obj.Length; i++)
            Instantiate(obj[i], new Vector3(-20.0f, 0.0f, 5.0f - i * 5.0f), Quaternion.identity);
    }
}
