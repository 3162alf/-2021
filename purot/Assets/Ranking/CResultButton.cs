/*==============================================================================
    PROJECT NAME
    [CResultButton.cs]
    �E���U���g�{�^���������ƃ��U���g�V�[���ɑJ�ڂ���
--------------------------------------------------------------------------------
    2021.04.21 @Author Yusuke Yusuke
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class CResultButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void OnRanking()
    {
        SceneManager.LoadScene("Result");
    }
}
