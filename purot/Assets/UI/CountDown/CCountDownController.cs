using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCountDownController : MonoBehaviour
{
    [SerializeField]
    public int iCountDownTime;
    [SerializeField]
    public Text CountDownDisplay;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(CountDownToStart());
    }

    IEnumerator CountDownToStart() {

        CPauser.Pause();

        while (iCountDownTime > 0) {
            CountDownDisplay.text = iCountDownTime.ToString();
            yield return new WaitForSeconds(1f) ;
            iCountDownTime--;
        }

        CountDownDisplay.text = "GO!";
 
        yield return new WaitForSeconds(1f);
        CountDownDisplay.gameObject.SetActive(false);
        Debug.Log("解除");
        CPauser.Resume();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
