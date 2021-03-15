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
            if (IsInChunkBounds(pos)) {
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
            if (!IsInChunkBounds(vec)) {
                throw new Exception("Position out of chunk bounds: " + vec.ToString());
            } else {
                if (!IsInChunkBounds(right) ||
                !IsInChunkBounds(left) ||
                !IsInChunkBounds(above) ||
                !IsInChunkBounds(below) ||
                !IsInChunkBounds(front) ||
                !IsInChunkBounds(back)) {
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
        public bool IsInChunkBounds(Vector3Int vec) {
            if (vec.x < 0 || vec.y < 0 || vec.z < 0 || vec.x >= ChunkSize || vec.y >= ChunkSize || vec.z >= ChunkSize) {
                return false;
            } else {
                return true;
            }
        }
        public bool IsInChunkBounds(int x, int y, int z) {
            if (x < 0 || y < 0 || z < 0 || x >= ChunkSize || y >= ChunkSize || z >= ChunkSize) {
                return false;
            } else {
                return true;
            }
        }

        public bool IsInsideOfChunk(Vector3Int vec, int shellThickness = 1) {
            if (vec.x < shellThickness || vec.y < shellThickness || vec.z < shellThickness || vec.x >= ChunkSize - shellThickness || vec.y >= ChunkSize - shellThickness || vec.z >= ChunkSize - shellThickness) {
                return false;
            } else {
                return true;
            }
        }
        public bool IsInsideOfChunk(int x, int y, int z, int shellThickness = 1) {
            if (x < shellThickness || y < shellThickness || z < shellThickness || x >= ChunkSize - shellThickness || y >= ChunkSize - shellThickness || z >= ChunkSize - shellThickness) {
                return false;
            } else {
                return true;
            }
        }
        public bool IsAtChunkBoundary(Vector3Int vec) {
            if (vec.x % ChunkSize == 0 && vec.y % ChunkSize == 0 && vec.z % ChunkSize == 0) {
                return true;
            } else {
                return false;
            }
        }
    }
}