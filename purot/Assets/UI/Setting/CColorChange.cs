using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CColorChange : MonoBehaviour
{
    [SerializeField] Image iImage;
    private float fR;
    private float fG;
    private float fB;

    // Start is called before the first frame update
    void Start()
    {
        fR = 0.0f;
        fG = 0.0f;
        fB = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLerp(Color.red,Color.blue);
    }

    private void ChangeBlack_to_White()
    {
        fR += 0.01f;
        fG += 0.01f;
        fB += 0.01f;
        iImage.color = new Color(fR, fG, fB, 1.0f);
    }

    private void ChangeLerp(Color a,Color b)
    {
        iImage.color = Color.Lerp(a, b, Mathf.PingPong(Time.time, 1));
    }
}
