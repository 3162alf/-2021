/*==============================================================================
    PROJECT NAME
    [CEffect.cs]
    ・エフェクトを再生する（シングルトンにしたからどこでもいつでも呼べます）
--------------------------------------------------------------------------------
    2021.04.21 @Author Misaki Sasaki
================================================================================
    History
        YYMMDD NAME
            UPDATE LOG
            
/*============================================================================*/
using UnityEngine;
using System.Collections;

public class CEffect : CSingletonMonoBehaviour<CEffect>
{
    // パーティクルを格納する変数
    // インスペクタ上で再生したいParticleのPrefabを入れて番号指定で再生
    // ただし、エフェクト再生位置は別途セルフで設定してください。
    [SerializeField] private ParticleSystem[] psEffect; 

    // パーティクルエフェクトを再生させる（引数・番号指定）
    public void PlayEffect(int num){
        psEffect[num].Play(true);
    }
}