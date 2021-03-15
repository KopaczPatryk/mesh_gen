using System.Collections.Generic;
using Assets.Scripts.WorldGen;
using Assets.Scripts.WorldGen.TerrainCullers;
using UnityEngine;

public class TransparencyCuller : ITerrainCuller {
    public List<Vector3Int> cull(Chunk chunk) {
        List<Vector3Int> culled = new List<Vector3Int>();

        int chunkSize = chunk.ChunkSize;
        for (int z = 0; z < chunk.ChunkSize; z++) {
            for (int y = 0; y < chunk.ChunkSize; y++) {
                for (int x = 0; x < chunk.ChunkSize; x++) {
                    //Vector3Int pos = new Vector3Int(x, y, z);
                    if (chunk.IsBlockVisible(new Vector3Int(x, y, z))) {
                        culled.Add(new Vector3Int(x, y, z));
                    }
                }
            }
        }
        return culled;
    }
}