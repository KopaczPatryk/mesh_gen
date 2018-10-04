using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MeshGen;
using UnityEngine;

public class ChunkBehaviour : MonoBehaviour {
    public Chunk MapChunk;
    public ChunkMeshGenerator MeshGenerator;

    private MeshFilter meshFilter;
    private MeshCollider MeshCollider;
    bool meshDone = false;
    bool meshAssigned = false;
    void Start() {
        MeshGenerator = new ChunkMeshGenerator();
        MeshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();

        RawMesh rawMesh = MeshGenerator.GenerateMesh(MapChunk);
        Mesh localmesh = meshFilter.mesh;

        localmesh.Clear();
        localmesh.vertices = rawMesh.Vertices;
        localmesh.uv = rawMesh.Uv;
        localmesh.triangles = rawMesh.Triangles;
        localmesh.RecalculateNormals();
        MeshCollider.sharedMesh = localmesh;
    }
}