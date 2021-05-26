
// 仮のオブジェクト生成用スクリプトなので詳しくは書きません
// 作るだけで消してないんで、あんまりいっぱい生成しないでください

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreate : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;

    public void Create() {
        for (int i = 0; i < obj.Length; i++)
            Instantiate(obj[i], new Vector3(-20.0f, 0.0f, i * 5.0f), Quaternion.identity);
    }
}
