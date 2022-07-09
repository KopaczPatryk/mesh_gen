using Assets.Scripts.WorldGen;
using Assets.Scripts.WorldGen.Blocks;
using UnityEngine;

namespace Assets.Scripts.MapGen {
    public class PerlinMapProvider : IMapProvider {
        private static IMapProvider instance;

        public static IMapProvider GetInstance() {
            instance = new PerlinMapProvider();
            return instance;
        }

        public BaseBlock GetBlock(Vector3 absolutePos) {
            throw new System.NotImplementedException();
        }

        public Chunk GetChunk(Vector3 absolutePos, int chunkSize) {
            Chunk chunk = new Chunk(chunkSize);

            for (int x = 0; x < chunkSize; x++) {
                for (int z = 0; z < chunkSize; z++) {
                    float h = Mathf.PerlinNoise(1000000f + (absolutePos.x * chunkSize + x) / 40f,
                                                1000000f + (absolutePos.z * chunkSize + z) / 40f);
                    h *= 20;
                    //h += 8 * 3;

                    for (int y = 0; y < chunkSize; y++) {
                        if (absolutePos.y * chunkSize + y < h) {
                            chunk.SetBlock(new Vector3Int(x, y, z), new Dirt());
                        } else {
                            chunk.SetBlock(new Vector3Int(x, y, z), new Air());
                        }
                    }
                }
            }
            return chunk;
        }
    }
}