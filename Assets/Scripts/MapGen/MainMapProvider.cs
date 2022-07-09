using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.WorldGen;
using Assets.Scripts.WorldGen.Blocks;
using UnityEngine;

namespace Assets.Scripts.MapGen {
    public class MainMapProvider : IMapProvider {
        private static IMapProvider instance;

        public static IMapProvider GetInstance() {
            return instance = new MainMapProvider();
        }

        public BaseBlock GetBlock(Vector3 absolutePos) {
            if (absolutePos.y > 8) {
                return new Air();
            } else {
                return new Dirt();
            }
        }

        public Chunk GetChunk(Vector3 absolutePos, int chunkSize) {
            Chunk chunk = new Chunk(chunkSize);
            int objectCount = 0;
            for (int z = 0; z < chunkSize; z++) {
                for (int y = 0; y < chunkSize; y++) {
                    for (int x = 0; x < chunkSize; x++) {
                        //multiple smaller cubes in chunks
                        if (x % 4 != 0 && y % 4 != 0 && z % 4 != 0) {
                            chunk.SetBlock(new Vector3Int(x, y, z), new Dirt());
                        } else {
                            chunk.SetBlock(new Vector3Int(x, y, z), new Air());
                        }

                        objectCount++;
                    }
                }
            }
            return chunk;
        }
    }
}