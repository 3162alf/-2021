using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CGrid : MonoBehaviour
{
    /*
    public enum Face
    {
        xy,
        zx,
        yz,
    };

    public float gridSize = 1f;
    public int size = 8;
    public Color color = Color.white;
    public Face face = Face.xy;
    public bool back = true;

    //�X�V���o�p
    float preGridSize = 0;
    int preSize = 0;
    Color preColor = Color.red;
    Face preFace = Face.zx;
    bool preBack = true;

    Mesh mesh;

    void Start()
    {

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh = ReGrid(mesh);

    }

    Mesh ReGrid(Mesh mesh)
    {
        if (back)
        {
            GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        }
        else
        {
            GetComponent<MeshRenderer>().material = new Material(Shader.Find("GUI/Text Shader"));
        }

        mesh.Clear();

        int drawSize;
        float width;
        int resolution;
        float diff;
        Vector3[] vertices;
        Vector2[] uvs;
        int[] lines;
        Color[] colors;

        drawSize = size * 2;
        width = gridSize * drawSize / 4.0f;
        Vector2 startPosition = new Vector2(-width, -width);
        Vector2 endPosition = new Vector2(width, width);
        diff = width / drawSize;
        resolution = (drawSize + 2) * 2;
        //�Ŋ��̂Q�ӂ�ǉ����Ă���

        vertices = new Vector3[resolution];
        uvs = new Vector2[resolution];
        lines = new int[resolution];
        colors = new Color[resolution];

        for (int i = 0; i < vertices.Length; i += 4)
        {
            vertices[i] = new Vector3(startPosition.x + (diff * (float)i), startPosition.y, 0);
            vertices[i + 1] = new Vector3(startPosition.x + (diff * (float)i), endPosition.y, 0);
            vertices[i + 2] = new Vector3(startPosition.x, endPosition.y - (diff * (float)i), 0);
            vertices[i + 3] = new Vector3(endPosition.x, endPosition.y - (diff * (float)i), 0);
        }

        for (int i = 0; i < resolution; i++)
        {
            uvs[i] = Vector2.zero;
            lines[i] = i;
            colors[i] = color;
        }

        Vector3 rotDirection;
        switch (face)
        {
            case Face.xy:
                rotDirection = Vector3.forward;
                break;
            case Face.zx:
                rotDirection = Vector3.up;
                break;
            case Face.yz:
                rotDirection = Vector3.right;
                break;
            default:
                rotDirection = Vector3.forward;
                break;
        }

        mesh.vertices = RotationVertices(vertices, rotDirection);
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.SetIndices(lines, MeshTopology.Lines, 0);

        preGridSize = gridSize;
        preSize = size;
        preColor = color;
        preFace = face;
        preBack = back;

        return mesh;
    }

    // ���_�z��f�[�^�[�����ׂĎw��̕����։�]�ړ�������
    Vector3[] RotationVertices(Vector3[] vertices, Vector3 rotDirection)
    {
        Vector3[] ret = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            ret[i] = Quaternion.LookRotation(rotDirection) * vertices[i];
        }
        return ret;
    }

    void Update()
    {
        //�֌W�l�̍X�V�����o�����烁�b�V�����X�V
        if (gridSize != preGridSize || size != preSize || preColor != color || preFace != face || preBack != back)
        {
            if (gridSize < 0) { gridSize = 0.000001f; }
            if (size < 0) { size = 1; }
            ReGrid(mesh);
        }
    }*/

    void Start(){
        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh.SetIndices(mf.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
    }

}
