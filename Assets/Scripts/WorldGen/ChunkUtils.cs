using System;
using Assets.Scripts.WorldGen.Blocks;
using UnityEngine;

public static class ChunkUtils {

    public static bool IsInChunkBounds(Vector3Int vec, int chunkSize) {
        if (vec.x < 0 || vec.y < 0 || vec.z < 0 || vec.x >= chunkSize || vec.y >= chunkSize || vec.z >= chunkSize) {
            return false;
        } else {
            return true;
        }
    }
    public static bool IsInChunkBounds(int x, int y, int z, int chunkSize) {
        if (x < 0 || y < 0 || z < 0 || x >= chunkSize || y >= chunkSize || z >= chunkSize) {
            return false;
        } else {
            return true;
        }
    }

    public static bool IsInsideOfChunk(Vector3Int vec, int chunkSize, int shellThickness = 1) {
        if (vec.x < shellThickness || vec.y < shellThickness || vec.z < shellThickness || vec.x >= chunkSize - shellThickness || vec.y >= chunkSize - shellThickness || vec.z >= chunkSize - shellThickness) {
            return false;
        } else {
            return true;
        }
    }
    public static bool IsInsideOfChunk(int x, int y, int z, int chunkSize, int shellThickness = 1) {
        if (x < shellThickness || y < shellThickness || z < shellThickness || x >= chunkSize - shellThickness || y >= chunkSize - shellThickness || z >= chunkSize - shellThickness) {
            return false;
        } else {
            return true;
        }
    }
    public static bool IsAtChunkBoundary(Vector3Int vec, int chunkSize) {
        if (vec.x % chunkSize == 0 && vec.y % chunkSize == 0 && vec.z % chunkSize == 0) {
            return true;
        } else {
            return false;
        }
    }
    public static bool IsBlockVisible(Vector3Int vec, BaseBlock[,,] blocks, int chunkSize) {
        Vector3Int right = new Vector3Int(vec.x + 1, vec.y, vec.z);
        Vector3Int left = new Vector3Int(vec.x - 1, vec.y, vec.z);
        Vector3Int above = new Vector3Int(vec.x, vec.y + 1, vec.z);
        Vector3Int below = new Vector3Int(vec.x, vec.y - 1, vec.z);
        Vector3Int front = new Vector3Int(vec.x, vec.y, vec.z + 1);
        Vector3Int back = new Vector3Int(vec.x, vec.y, vec.z - 1);
        if (!ChunkUtils.IsInChunkBounds(vec, chunkSize)) {
            throw new Exception("Position out of chunk bounds: " + vec.ToString());
        } else {
            if (!ChunkUtils.IsInChunkBounds(right, chunkSize)
                || !ChunkUtils.IsInChunkBounds(left, chunkSize)
                || !ChunkUtils.IsInChunkBounds(above, chunkSize)
                || !ChunkUtils.IsInChunkBounds(below, chunkSize)
                || !ChunkUtils.IsInChunkBounds(front, chunkSize)
                || !ChunkUtils.IsInChunkBounds(back, chunkSize)) {
                return true;
            } else if (blocks[right.x, right.y, right.z].Transparent ||
                        blocks[left.x, left.y, left.z].Transparent ||
                        blocks[above.x, above.y, above.z].Transparent ||
                        blocks[below.x, below.y, below.z].Transparent ||
                        blocks[front.x, front.y, front.z].Transparent ||
                        blocks[back.x, back.y, back.z].Transparent) {
                return true;
            } else {
                return false;
            }
        }
    }
}