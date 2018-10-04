using System.Collections;
using System.Collections.Generic;
using MeshGen;
using UnityEngine;

public class WorldMap : MonoBehaviour {
	public GameObject ChunkPrefab;

	IMapProvider mainMap;
	public const int ChunkSize = 8;
	public Dictionary<Vector3Int, Chunk> LoadedChunks { get; set; }
	
	void Awake()
	{
		mainMap = InstanceFactory.GetInstance().GetMapProvider(); 
	}

	void Start() {
		LoadedChunks = new Dictionary<Vector3Int, Chunk>();
        for (int y = 0; y < 2; y++)
        {
            for (int x = -10; x < 10; x++)
            {
                for (int z = -10; z < 10; z++)
                {
                    LoadChunk(new Vector3Int(x, y, z));
                    Debug.LogFormat("Requesting chunk at {0}, {1}, {2}.", x, y, z);
                }
            }
        }
	}

	private void LoadChunk(Vector3Int pos) {
		GameObject go = Instantiate(ChunkPrefab, pos * ChunkSize, transform.rotation);
        ChunkBehaviour behavior = go.GetComponent<ChunkBehaviour>();

		if (LoadedChunks.ContainsKey(pos)) { //get from recycled
			Chunk tchunk = LoadedChunks[pos];
            behavior.MapChunk = tchunk;
		}
		else
		{
            behavior.MapChunk = mainMap.GetChunk(pos, ChunkSize);
            LoadedChunks.Add(pos, behavior.MapChunk);
		}
	}
}