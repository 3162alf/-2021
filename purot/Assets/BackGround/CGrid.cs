using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CGrid : MonoBehaviour
{

    void Start(){
        //MeshFilter mf = GetComponent<MeshFilter>();
        //mf.mesh.SetIndices(mf.mesh.GetIndices(0), MeshTopology.Quads, 0);

        Mesh mesh;

        mesh = GetComponent<MeshFilter>().mesh;

        var triangleList = mesh.GetIndices(0);
        var triangleNum = triangleList.Length / 3;
        var lineList = new int[triangleNum * 4];
        for (var n = 0; n < triangleNum; ++n) {
            var t = n * 3;
            var l = n * 4;
            lineList[l + 0] = triangleList[t + 0];
            lineList[l + 1] = triangleList[t + 1];

            //lineList[l + 2] = triangleList[t + 1];
            //lineList[l + 3] = triangleList[t + 2];

            lineList[l + 2] = triangleList[t + 2];
            lineList[l + 3] = triangleList[t + 0];
        }

        mesh.SetIndices(lineList, MeshTopology.Lines, 0);

        // マテリアルの色
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.color = new Color(0.4f, 0.4f, 0.9f);

    }

}
