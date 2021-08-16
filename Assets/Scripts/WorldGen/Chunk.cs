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
    }
}