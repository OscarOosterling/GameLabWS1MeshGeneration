using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class VoxelMesh : MonoBehaviour
{
    private Mesh mesh;

    private List<Vector3> vertices;
    private List<int> triangles;

    private List<Vector2> uvs;



    [SerializeField]
    private float scale = 1f;
    private float adjustedScale;

    [SerializeField]
    private int voxelSize = 5;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 0.5f;
    }

    private void Start()
    {
        VoxelMeshData voxelMeshData = GenerateVoxelMeshData(voxelSize);
        GenerateVoxelMesh(voxelMeshData);
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
    private VoxelMeshData GenerateVoxelMeshData(int voxelSize)
    {
        int[,,] data = new int[voxelSize, voxelSize, voxelSize];
        int x1 = (int)(voxelSize * .5f);
        int y1 = (int)(voxelSize * .5f);
        int z1 = (int)(voxelSize * .5f);

        for (int x = 0; x < data.GetLength(0); x++)
        {
            for (int y = 0; y < data.GetLength(1); y++)
            {
                for (int z = 0; z < data.GetLength(2); z++)
                {
                    if (CalcDistCoordinates.getDistWithCoordinates(x1, y1, z1, x, y, z) < voxelSize * 0.5f)
                    {
                        data[x, y, z] = 1;
                    }
                    else
                    {
                        data[x, y, z] = 0;
                    }
                }
            }
        }
        return new VoxelMeshData(data);
    }

    private void GenerateVoxelMesh(VoxelMeshData voxelMeshData)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        for (int x = 0; x < voxelMeshData.Width; x++)
        {
            for (int y = 0; y < voxelMeshData.Height; y++)
            {
                for (int z = 0; z < voxelMeshData.Depth; z++)
                {
                    if (voxelMeshData.GetCell(x, y, z) == 0)
                    {
                        continue;
                    }
                    GenerateCubeMeshData(adjustedScale, new Vector3((float)x, (float)y, (float)z) * scale, x, y, z, voxelMeshData);
                }
            }
        }
    }

    private void GenerateCubeMeshData(float scale, Vector3 position, int x, int y, int z, VoxelMeshData voxelMeshData)
    {
        for (int i = 0; i < 6; i++)
        {
            if (voxelMeshData.getNeighbour(x, y, z, i) == 0)
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
                    default:
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
}
