using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeshGen;
using MeshGen.WorldGen;
using UnityEngine;

public class Chunk {
	public int Xsize = 4;
	public int Ysize = 4;
	public int Zsize = 4;
	private int chunkSize = 13;
	public int ChunkSize {
		get {
			return chunkSize;
		}
		set {
			Xsize = value;
			Ysize = value;
			Zsize = value;
			chunkSize = value;
		}
	}

	protected Block[, , ] map { get; set; }

	public Chunk(int chunkSize) {
		ChunkSize = chunkSize;
		map = new Block[Xsize, Ysize, Zsize];
	}

	public void SetMap(Block[, , ] chunkMap) {
		// todo needs chunkSize check
		map = chunkMap;
	}
	public void SetMap(Chunk chunkMap) {
		///todo add chunk size check 
		if (ChunkSize != chunkMap.ChunkSize) {
			throw new Exception("Chunk size mismatch");
		}
		map = chunkMap.GetMapArray();
	}
	public void SetBlock(Vector3Int pos, Block block) {
		map[pos.x, pos.y, pos.z] = block;
	}
	public Block[, , ] GetMapArray() {
		return map;
	}
}