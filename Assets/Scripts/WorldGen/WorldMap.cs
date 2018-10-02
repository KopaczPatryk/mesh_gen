using System.Collections;
using System.Collections.Generic;
using MeshGen;
using UnityEngine;

public class WorldMap : MonoBehaviour {
	IMapProvider mainMap;
	public GameObject ChunkPrefab;
	public const int ChunkSize = 8;
	public Dictionary<Vector3Int, Chunk> Chunks { get; set; }
	
	void Awake()
	{
		mainMap = InstanceFactory.GetInstance().GetMapProvider(); 
	}
	void Start() {
		Chunks = new Dictionary<Vector3Int, Chunk>();
		for (int x = 0; x < 4; x++)
		{
			for (int z = 0; z < 4; z++)
			{
				LoadChunk(new Vector3Int(x, 0, z));
			}
		}
		//LoadChunk(new Vector3Int(1,1,1));
		//LoadChunk(new Vector3Int(2,1,1));
		//LoadChunk(new Vector3Int(2,1,2));
	}
	private void LoadChunk(Vector3Int pos) {
		GameObject go = Instantiate(ChunkPrefab, pos * ChunkSize, transform.rotation);

		if (Chunks.ContainsKey(pos)) { //get from recycled
			Chunk tchunk = Chunks[pos];
			//tchunk.ChunkSize = 13;
			go.GetComponent<Chunk>().SetMap(tchunk.GetMapArray());
		}
		else
		{
			Chunk tchunk = go.GetComponent<Chunk>();
			tchunk.ChunkSize = ChunkSize;
			tchunk.SetMap(mainMap.GetChunk(pos, ChunkSize));
			Chunks.Add(pos, tchunk);
		}
		go.GetComponent<ChunkMeshGenerator>().GenerateMesh();
		
	}

	public enum Mode {
		Absolute,
		RelativeToChunks
	}
}