/*==============================================================================
    Priject_Beta
    [CCreateEffect.cs]
    �E�p�[�e�B�N���G�t�F�N�g�𐶐�����

--------------------------------------------------------------------------------
    2021.05.27 @Author Wataru Fukuoka
================================================================================
    History

/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateEffect : MonoBehaviour {
    [SerializeField] private GameObject[] gParticle;               // �p�[�e�B�N���{��

    void Start() {
    }

    void Update() {
    }

    public void CreateEffect() {
        GameObject pParticleObject;
        for (int i = 0; i < gParticle.Length; i++) {
            pParticleObject = (GameObject)Instantiate(gParticle[i]);
            pParticleObject.transform.position = this.transform.position;
            pParticleObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
