using System;
using Assets.Scripts.WorldGen.Blocks;

using UnityEngine;

namespace Assets.Scripts.WorldGen {
    public class Chunk {
        public BaseBlock[,,] Blocks { get; private set; }

        public int ChunkSize { get; private set; }

        public Chunk(int chunkSize) {
            this.ChunkSize = chunkSize;
            this.Blocks = new BaseBlock[ChunkSize, ChunkSize, ChunkSize];
        }

        public BaseBlock GetBlock(Vector3Int pos) {
            if (ChunkUtils.IsInChunkBounds(pos, ChunkSize)) {
                return Blocks[pos.x, pos.y, pos.z];
            } else {
                throw new Exception("Position out of chunk bounds: " + pos.ToString());
            }
        }

        public void SetBlock(Vector3Int pos, BaseBlock block) {
            Blocks[pos.x, pos.y, pos.z] = block;
        }
        public bool IsBlockVisible(Vector3Int vec) {
            Vector3Int right = new Vector3Int(vec.x + 1, vec.y, vec.z);
            Vector3Int left = new Vector3Int(vec.x - 1, vec.y, vec.z);
            Vector3Int above = new Vector3Int(vec.x, vec.y + 1, vec.z);
            Vector3Int below = new Vector3Int(vec.x, vec.y - 1, vec.z);
            Vector3Int front = new Vector3Int(vec.x, vec.y, vec.z + 1);
            Vector3Int back = new Vector3Int(vec.x, vec.y, vec.z - 1);
            if (!ChunkUtils.IsInChunkBounds(vec, ChunkSize)) {
                throw new Exception("Position out of chunk bounds: " + vec.ToString());
            } else {
                if (!ChunkUtils.IsInChunkBounds(right, ChunkSize) ||
                !ChunkUtils.IsInChunkBounds(left, ChunkSize) ||
                !ChunkUtils.IsInChunkBounds(above, ChunkSize) ||
                !ChunkUtils.IsInChunkBounds(below, ChunkSize) ||
                !ChunkUtils.IsInChunkBounds(front, ChunkSize) ||
                !ChunkUtils.IsInChunkBounds(back, ChunkSize)) {
                    return true;
                } else if (Blocks[right.x, right.y, right.z].Transparent ||
                              Blocks[left.x, left.y, left.z].Transparent ||
                              Blocks[above.x, above.y, above.z].Transparent ||
                              Blocks[below.x, below.y, below.z].Transparent ||
                              Blocks[front.x, front.y, front.z].Transparent ||
                              Blocks[back.x, back.y, back.z].Transparent) {
                    return true;
                } else {
                    return false;
                }
            }
        }
    }
}