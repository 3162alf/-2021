using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScoreController : MonoBehaviour
{
    [SerializeField] private Sprite[] sp = new Sprite[11];

    public void ChangeSprite(int no) {

        if (no > 10 || no < 0) no = 0;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sp[no];

    }
}
