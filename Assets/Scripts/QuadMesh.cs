using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)),RequireComponent(typeof(MeshRenderer))]
public class QuadMesh : MonoBehaviour
{
    private Mesh mesh;

    private  Vector3[] vertices;
    private int[] triangles;

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
        vertices = new Vector3[] { new Vector3(0,0,0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1,0,1) };
        triangles = new int[] { 0, 1, 2, 2, 1, 3 };
    }
}
