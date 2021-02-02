using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    [SerializeField]
    private float cellSize = 1;
    [SerializeField]
    private Vector3 gridOffset;
    [SerializeField]
    private int gridSize = 1;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        GenerateMeshData();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void GenerateMeshData()
    {
        vertices = new Vector3[(gridSize+1) * (gridSize+1)];
        triangles = new int[gridSize * gridSize * 6];

        int v = 0;
        int t = 0;

        float vertexOffset = cellSize * 0.5f;

        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, 0, (y * cellSize) - vertexOffset);
                v++;
            }
        }

        v = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + gridSize  +1;
                triangles[t + 5] = v + gridSize + 2;
                v++;
                t += 6;
            }
            v++;
        }


    }
}