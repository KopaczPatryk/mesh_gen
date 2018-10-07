using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.Interaction;
using MeshGen;
using MeshGen.WorldGen;
using UnityEngine;

public class WorldMap : MonoBehaviour {
	public GameObject ChunkPrefab;

	IMapProvider mainMap;
	public const int ChunkSize = 8;
	public Dictionary<Vector3Int, Chunk> Chunks { get; set; }
	public Dictionary<Vector3Int, ChunkBehaviour> LoadedChunkObjects { get; set; }

    void Awake()
	{
		mainMap = InstanceFactory.GetInstance().GetMapProvider(); 
	}

	void Start() {
		Chunks = new Dictionary<Vector3Int, Chunk>();
        LoadedChunkObjects = new Dictionary<Vector3Int, ChunkBehaviour>();
        for (int y = 0; y < 1; y++)
        {
            for (int x = -1; x < 1; x++)
            {
                for (int z = -1; z < 1; z++)
                {
                    LoadChunk(new Vector3Int(x, y, z));
                }
            }
        }
	}

	private void LoadChunk(Vector3Int pos) {
		GameObject go = Instantiate(ChunkPrefab, pos * ChunkSize, transform.rotation);
        ChunkBehaviour chunkBehavior = go.GetComponent<ChunkBehaviour>();
        LoadedChunkObjects.Add(pos, chunkBehavior);
        chunkBehavior.Interacted += ChunkBehavior_Interacted;

		if (Chunks.ContainsKey(pos)) { //get from recycled
			Chunk tchunk = Chunks[pos];
            chunkBehavior.MapChunk = tchunk;
		}
		else
		{
            chunkBehavior.MapChunk = mainMap.GetChunk(pos, ChunkSize);
            Chunks.Add(pos, chunkBehavior.MapChunk);
		}
	}

    private void ChunkBehavior_Interacted(RaycastHit hit, InteractionType interaction)
    {
        switch (interaction)
        {
            case InteractionType.Destroy:
                Vector3 respos = (hit.point - hit.normal / 2);
                Vector3Int hitPosInside = new Vector3Int(Mathf.FloorToInt(respos.x), Mathf.FloorToInt(respos.y), Mathf.FloorToInt(respos.z));
                
                SetBlock(hitPosInside, new MeshGen.WorldGen.Space());
                GetChunkBehaviour(GetChunkPos(hitPosInside)).RegenerateMesh();
                break;
        }
    }

    private ChunkBehaviour GetChunkBehaviour(Vector3Int absPos)
    {
        return LoadedChunkObjects[GetChunkPos(absPos)];
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
        LoadedChunkObjects[GetChunkPos(absPos)].MapChunk.SetBlock(GetBlockLocalPos(absPos), block);
    }
    public static int Mod(int a, int n)
    {
        return a - (int)Math.Floor((float)a / n) * n;
    }
    private Vector3Int GetBlockLocalPos (Vector3Int absPos)
    {
        int x = Mod(absPos.x, ChunkSize);
        int y = Mod(absPos.y, ChunkSize);
        int z = Mod(absPos.z, ChunkSize);
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