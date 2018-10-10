using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeshGen;
using MeshGen.WorldGen;
using UnityEngine;

public class Chunk {
	protected Block[, , ] Map { get; set; }
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
    
	public Chunk(int chunkSize) {
		ChunkSize = chunkSize;
		Map = new Block[Xsize, Ysize, Zsize];
	}

	//public void SetMap(Block[, , ] chunkMap) {
	//	// todo needs chunkSize check
	//	Map = chunkMap;
	//}

	//public void SetMap(Chunk chunkMap) {
	//	///todo add chunk size check 
	//	if (ChunkSize != chunkMap.ChunkSize) {
	//		throw new Exception("Chunk size mismatch");
	//	}
	//	Map = chunkMap.GetBlockArray();
	//}
	public Block[,,] GetBlockArray() {
		return Map;
	}

    public Block GetBlock(Vector3Int pos)
    {
        if (IsInChunkBounds(pos))
        {
            return Map[pos.x, pos.y, pos.z];
        }
        else
        {
            throw new Exception("Position out of chunk bounds: " + pos.ToString());
        }
    }
	public void SetBlock(Vector3Int pos, Block block) {
		Map[pos.x, pos.y, pos.z] = block;
	}
    
    public bool IsBlockVisible(Vector3Int vec)
    {
        Vector3Int right    = new Vector3Int(vec.x + 1, vec.y, vec.z);
        Vector3Int left     = new Vector3Int(vec.x - 1, vec.y, vec.z);
        Vector3Int above    = new Vector3Int(vec.x, vec.y + 1, vec.z);
        Vector3Int below    = new Vector3Int(vec.x, vec.y - 1, vec.z);
        Vector3Int front    = new Vector3Int(vec.x, vec.y, vec.z + 1);
        Vector3Int back     = new Vector3Int(vec.x, vec.y, vec.z - 1);
        if (!IsInChunkBounds(vec))
        {
            throw new Exception("Position out of chunk bounds: " + vec.ToString());
        }
        else
        {
            if (!IsInChunkBounds(right) ||
            !IsInChunkBounds(left) ||
            !IsInChunkBounds(above) ||
            !IsInChunkBounds(below) ||
            !IsInChunkBounds(front) ||
            !IsInChunkBounds(back))
            {
                return true;
            }
            else if (   Map[right.x, right.y, right.z].Transparent ||
                        Map[left.x, left.y, left.z].Transparent ||
                        Map[above.x, above.y, above.z].Transparent ||
                        Map[below.x, below.y, below.z].Transparent ||
                        Map[front.x, front.y, front.z].Transparent ||
                        Map[back.x, back.y, back.z].Transparent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool IsInChunkBounds(Vector3Int vec)
    {
        if (vec.x < 0 || vec.y < 0 || vec.z < 0)
        {
            return false;
        }
        else if (vec.x >= chunkSize || vec.y >= chunkSize || vec.z >= chunkSize)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsAtChunkBoundary(Vector3Int vec)
    {
        //if (vec.x == 0 || vec.y == 0 || vec.z == 0)
        //{
        //    return true;
        //}
        //else if (vec.x == chunkSize || vec.y == chunkSize || vec.z == chunkSize)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        if (vec.x % ChunkSize == 0 && vec.y % ChunkSize == 0 && vec.z % ChunkSize == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}