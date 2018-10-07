using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Interaction;
using MeshGen;
using UnityEngine;

public delegate void InteractionHandler(RaycastHit hit, InteractionType interaction);
public class ChunkBehaviour : MonoBehaviour, IInteractable {
    public event InteractionHandler Interacted;
    public Chunk MapChunk;
    public ChunkMeshGenerator MeshGenerator;

    private MeshFilter meshFilter;
    private MeshCollider MeshCollider;

    public void Interact(RaycastHit hitInfo, InteractionType interaction)
    {
        if (Interacted != null)
        {
            Interacted(hitInfo, interaction);
        }
    }

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

    public void RegenerateMesh()
    {
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