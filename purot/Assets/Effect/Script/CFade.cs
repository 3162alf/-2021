using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CFade : MonoBehaviour
{
  

    float fFadeSpeed = 0.002f;
    float fRed, fGreen, fBlue, fAlfa;
    public bool isFadeOut = false;
    public bool isFadeIn = false;
    Image fadeImage;

    void Start()
    {

        fadeImage = GetComponent<Image>();
        fRed = fadeImage.color.r;
        fGreen = fadeImage.color.g;
        fBlue = fadeImage.color.b;
        fAlfa = fadeImage.color.a;
       

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isFadeOut = true;
        }

        if (isFadeIn)
        {
            StartFadeIn();

        }

        if (isFadeOut)
        {
            StartFadeOut();
            
        }

        

    }

    void StartFadeIn()
    {
       
        fAlfa-= fFadeSpeed;
        SetAlpha();
        if (fAlfa <= 0)
        {
            isFadeIn = false;
           
            fadeImage.enabled = false; //panel off
        }

    }


    void StartFadeOut()
    {
        
        fAlfa += fFadeSpeed;
        SetAlpha();
        if (fAlfa >= 1)
        {
            //fadeImage.enabled = false;
            isFadeOut = false;
            isFadeIn = true;
            Invoke("ChangeScene", 1.5f);

        }

    }


    void SetAlpha()
    {
        fadeImage.color = new Color(fRed, fGreen, fBlue, fAlfa);
    }


    void ChangeScene()
    {
        SceneManager.LoadScene("Scene2");
    }
}
