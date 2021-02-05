using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class CubeMesh : MonoBehaviour
{
    private Mesh mesh;

    private List<Vector3> vertices;
    private List<int> triangles;

    private List<Vector2> uvs;

    [SerializeField]
    private float scale = 1f;
    private float adjustedScale;

    [SerializeField]
    private Vector3 position = new Vector3(0, 0, 0);

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 0.5f;
    }

    private void Start()
    {
        GenerateMeshData(adjustedScale, position * scale);
        UpdateMesh();
    }
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
    }

    private void GenerateMeshData(float scale, Vector3 position)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {
            vertices.AddRange(CubeMeshData.quadVertices(i, scale, position));


            int verticesCount = vertices.Count;

            switch (i)
            {
                case 0:
                    uvs.Add(new Vector2(vertices[verticesCount - 3].x, vertices[verticesCount - 3].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 4].x, vertices[verticesCount - 4].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 1].x, vertices[verticesCount - 1].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 2].x, vertices[verticesCount - 2].y));
                    break;
                case 1:
                    uvs.Add(new Vector2(vertices[verticesCount - 4].z, vertices[verticesCount - 4].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 3].z, vertices[verticesCount - 3].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 2].z, vertices[verticesCount - 2].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 1].z, vertices[verticesCount - 1].y));
                    break;
                case 2:
                    uvs.Add(new Vector2(vertices[verticesCount - 4].x, vertices[verticesCount - 4].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 3].x, vertices[verticesCount - 3].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 2].x, vertices[verticesCount - 2].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 1].x, vertices[verticesCount - 1].y));
                    break;
                case 3:
                    uvs.Add(new Vector2(vertices[verticesCount - 3].z, vertices[verticesCount - 3].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 4].z, vertices[verticesCount - 4].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 1].z, vertices[verticesCount - 1].y));
                    uvs.Add(new Vector2(vertices[verticesCount - 2].z, vertices[verticesCount - 2].y));
                    break;
                case 4:
                    uvs.Add(new Vector2(vertices[verticesCount - 4].x, vertices[verticesCount - 4].z));
                    uvs.Add(new Vector2(vertices[verticesCount - 3].x, vertices[verticesCount - 3].z));
                    uvs.Add(new Vector2(vertices[verticesCount - 2].x, vertices[verticesCount - 2].z));
                    uvs.Add(new Vector2(vertices[verticesCount - 1].x, vertices[verticesCount - 1].z));
                    break;
                case 5:
                    uvs.Add(new Vector2(vertices[verticesCount - 3].x, vertices[verticesCount - 3].z));
                    uvs.Add(new Vector2(vertices[verticesCount - 4].x, vertices[verticesCount - 4].z));
                    uvs.Add(new Vector2(vertices[verticesCount - 1].x, vertices[verticesCount - 1].z));
                    uvs.Add(new Vector2(vertices[verticesCount - 2].x, vertices[verticesCount - 2].z));

                    break;
            }

            triangles.Add(verticesCount - 4);
            triangles.Add(verticesCount - 3);
            triangles.Add(verticesCount - 2);
            triangles.Add(verticesCount - 4);
            triangles.Add(verticesCount - 2);
            triangles.Add(verticesCount - 1);
        }
    }
}
