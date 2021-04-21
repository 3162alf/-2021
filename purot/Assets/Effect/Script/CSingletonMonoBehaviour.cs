/*==============================================================================
    PROJECT NAME
    [CSingletonMonoBehaviour.cs]
    ・シングルトン（これ以上言えることがありませんでした。）
--------------------------------------------------------------------------------
    2021.04.21 @Author Misaki Sasaki
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/

using UnityEngine;
using System;

public abstract class CSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    public static T Instance{
        get{
            // インスタンスがない場合はデバッグログで表示する
            if (instance == null){
                Type t = typeof(T);
                instance = (T)FindObjectOfType(t);

                if (instance == null){
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake(){
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する
        CheckInstance();
    }

    protected bool CheckInstance(){
        if (instance == null){
            instance = this as T;
            return true;
        }
        else if (Instance == this){
            return true;
        }

        Destroy(this);
        return false;
    }
}