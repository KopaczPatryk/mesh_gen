﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.Interaction;
using MeshGen;
using MeshGen.WorldGen;
using UnityEngine;
using static Assets.Scripts.Helpers.MathHelpers;

public class WorldMap : MonoBehaviour
{
    public GameObject ChunkPrefab;

    IMapProvider mainMap;
    public const int ChunkSize = 8;
    public Dictionary<Vector3Int, Chunk> Chunks { get; set; }
    public Dictionary<Vector3Int, ChunkBehaviour> LoadedChunkObjects { get; set; }
    private GameObject lastClickedGizmo { get; set; }

    void Awake()
    {
        mainMap = InstanceFactory.GetInstance().GetMapProvider();
    }

    void Start()
    {
        Chunks = new Dictionary<Vector3Int, Chunk>();
        LoadedChunkObjects = new Dictionary<Vector3Int, ChunkBehaviour>();
        for (int y = -3; y < 2; y++)
        {
            for (int x = -3; x < 3; x++)
            {
                for (int z = -3; z < 3; z++)
                {
                    LoadChunk(new Vector3Int(x, y, z));
                }
            }
        }
        //LoadChunk(new Vector3Int(0, 1, -1));
    }

    private void LoadChunk(Vector3Int pos)
    {
        GameObject go = Instantiate(ChunkPrefab, pos * ChunkSize, transform.rotation);
        ChunkBehaviour chunkBehavior = go.GetComponent<ChunkBehaviour>();

        chunkBehavior.Interacted += ChunkBehavior_Interacted;

        if (Chunks.ContainsKey(pos))
        { //get from recycled
            Chunk tchunk = Chunks[pos];
            chunkBehavior.MapChunk = tchunk;
        }
        else
        {
            chunkBehavior.MapChunk = mainMap.GetChunk(pos, ChunkSize);
            Chunks.Add(pos, chunkBehavior.MapChunk);
            go.name = pos.ToString();
            LoadedChunkObjects.Add(pos, chunkBehavior);
        }
    }

    private void ChunkBehavior_Interacted(RaycastHit hit, InteractionType interaction)
    {
        switch (interaction)
        {
            case InteractionType.Destroy:
                Vector3 respos = (hit.point - hit.normal / 2);
                Vector3Int hitPosInside = new Vector3Int((int)Mathf.Floor(respos.x), (int)Mathf.Floor(respos.y), (int)Mathf.Floor(respos.z));
                var chunkpos = GetChunkPos(hitPosInside);

                //var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //obj.transform.position = respos;

                //if (lastClickedGizmo != null)
                //{
                //    Destroy(lastClickedGizmo);
                //}

                //lastClickedGizmo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //lastClickedGizmo.transform.position = GetChunkPos(hitPosInside) * ChunkSize + GetBlockLocalPos(hitPosInside) + Vector3.one / 2;
                //lastClickedGizmo.transform.localScale = Vector3.one * 1.1f;

                print("hit pos" + hitPosInside);
                print("chunkpos: " + GetChunkPos(hitPosInside));
                print("chunkpos multiplied: " + GetChunkPos(hitPosInside) * ChunkSize);
                print("block localpos: " + GetBlockLocalPos(hitPosInside));
                print("summed: " + (GetChunkPos(hitPosInside) * ChunkSize + GetBlockLocalPos(hitPosInside)));

                SetBlock(hitPosInside, new MeshGen.WorldGen.Space());
                //GetChunkBehaviour(chunkpos).test();
                GetChunkBehaviour(chunkpos).RegenerateMesh();

                break;
        }
    }

    private ChunkBehaviour GetChunkBehaviour(Vector3Int chunkPos)
    {
        //Debug.Log("szukam: " + GetChunkPos(chunkPos).ToString());
        //return GameObject.Find(chunkPos.ToString()).GetComponent<ChunkBehaviour>();
        return LoadedChunkObjects[chunkPos];
    }
    public Block GetBlock(Vector3Int absPos)
    {
        var chunk = LoadedChunkObjects[GetChunkPos(absPos)].MapChunk.GetBlockArray();

        Vector3Int localPos = GetBlockLocalPos(absPos);
        try
        {
            //print(chunk[localPos.x, localPos.y, localPos.z].GetType().ToString());
            return chunk[localPos.x, localPos.y, localPos.z];
        }
        catch (IndexOutOfRangeException)
        {
            print(localPos.ToString());
        }
        return null;
    }
    public void SetBlock(Vector3Int absPos, Block block)
    {
        Vector3Int localPos = GetBlockLocalPos(absPos);
        //Debug.Log("zmieniono blok na pozycji: " + localPos.ToString());

        LoadedChunkObjects[GetChunkPos(absPos)].MapChunk.SetBlock(localPos, block);
    }

    private Vector3Int GetBlockLocalPos(Vector3Int absPos)
    {
        int x = Modulo(absPos.x, ChunkSize);
        int y = Modulo(absPos.y, ChunkSize);
        int z = Modulo(absPos.z, ChunkSize);
        return new Vector3Int(x, y, z);
    }

    private Vector3Int GetChunkPos(Vector3Int absPos)
    {
        Vector3Int chunkPos = new Vector3Int
        {
            x = (int)Mathf.Floor(absPos.x / (float)ChunkSize),
            y = (int)Mathf.Floor(absPos.y / (float)ChunkSize),
            z = (int)Mathf.Floor(absPos.z / (float)ChunkSize)
        };
        return chunkPos;
    }
}