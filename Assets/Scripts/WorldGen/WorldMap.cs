using System.Collections;
using System.Collections.Generic;
using MeshGen;
using UnityEngine;

public class WorldMap : MonoBehaviour {
	public GameObject ChunkPrefab;
	public const int ChunkSize = 13;
	public Dictionary<Vector3Int, Chunk> Chunks { get; set; }

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
	public void LoadChunk(Vector3Int pos) {
		if (Chunks.ContainsKey(pos)) {

		}
		else
		{
			GameObject go = Instantiate(ChunkPrefab, pos * ChunkSize, transform.rotation);
			Chunk tchunk = go.GetComponent<Chunk>();
			tchunk.ChunkSize = ChunkSize;
			Chunks.Add(pos, tchunk);
			go.GetComponent<MeshGenerator>().GenerateMesh();
		}
	}

	public enum Mode {
		Absolute,
		RelativeToChunks
	}
}