using MeshGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBehaviour : MonoBehaviour
{
    public Chunk MapChunk;
    public ChunkMeshGenerator MeshGenerator;

    private MeshFilter meshFilter;
    private MeshCollider MeshCollider;

    private void Awake()
    {
        
    }
    void Start()
    {
        MeshGenerator = new ChunkMeshGenerator();
        MeshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        GetComponent<MeshCollider>();

        Mesh mesh = MeshGenerator.GenerateMesh(MapChunk);
        Mesh localmesh = meshFilter.mesh;

        localmesh.Clear();
        localmesh.vertices = mesh.vertices;
        localmesh.uv = mesh.uv;
        localmesh.triangles = mesh.triangles;
        localmesh.RecalculateNormals();
        //localmesh.normals = mesh.normals;
        MeshCollider.sharedMesh = localmesh;
    }

    void Update()
    {

    }
}
