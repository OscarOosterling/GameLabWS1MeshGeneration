using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class CubeMesh : MonoBehaviour
{
    private Mesh mesh;

    private List<Vector3> vertices;
    private List<int> triangles;
    

    [SerializeField]
    private float scale = 1f;
    private float adjustedScale;

    [SerializeField]
    private Vector3 position = new Vector3( 0, 0, 0);

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 0.5f;
    }

    private void Start()
    {
        GenerateMeshData(adjustedScale,position*scale);
        UpdateMesh();
    }
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    private void GenerateMeshData(float scale, Vector3 position)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            vertices.AddRange(CubeMeshData.quadVertices(i,scale,position));
            int verticesCount = vertices.Count;

            triangles.Add(verticesCount - 4);
            triangles.Add(verticesCount - 3);
            triangles.Add(verticesCount - 2);
            triangles.Add(verticesCount - 4);
            triangles.Add(verticesCount - 2);
            triangles.Add(verticesCount - 1);
        }
    }
}
public static class CubeMeshData
{
    public static Vector3[] vertices =
    {
        new Vector3(1,1,1),
        new Vector3(-1,1,1),
        new Vector3(-1,-1,1),
        new Vector3(1,-1,1),
        new Vector3(-1,1,-1),
        new Vector3(1,1,-1),
        new Vector3(1,-1,-1),
        new Vector3(-1,-1,-1)
    };

    public static int[][] quads =
    {
        new int[] {0,1,2,3},
        new int[] {5,0,3,6},
        new int[] {4,5,6,7},
        new int[] {1,4,7,2},
        new int[] {5,4,1,0},
        new int[] {3,2,7,6},
    };

    public static Vector3[] quadVertices(int dir, float scale, Vector3 position)
    {
        Vector3[] quadVertices = new Vector3[4];

        for (int i = 0; i < quadVertices.Length; i++)
        {
            quadVertices[i] = (vertices[quads[dir][i]] * scale) + position;
        }
        return quadVertices;
    }
}