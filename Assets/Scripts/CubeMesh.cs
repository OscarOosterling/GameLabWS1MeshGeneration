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
