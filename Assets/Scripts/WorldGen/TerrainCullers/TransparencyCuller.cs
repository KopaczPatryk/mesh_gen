using System.Collections.Generic;
using Assets.Scripts.WorldGen;
using Assets.Scripts.WorldGen.TerrainCullers;
using UnityEngine;

public class TransparencyCuller : ITerrainCuller {
    public List<Vector3Int> cull(Chunk chunk) {
        List<Vector3Int> culled = new List<Vector3Int>();

        int chunkSize = chunk.ChunkSize;
        for (int z = 0; z < chunk.zSize; z++) {
            for (int y = 0; y < chunk.ySize; y++) {
                for (int x = 0; x < chunk.xSize; x++) {
                    //Vector3Int pos = new Vector3Int(x, y, z);
                    if (chunk.IsBlockVisible(new Vector3Int(x, y, z))) {
                        culled.Add(new Vector3Int(x, y, z));
                    }

                    //try
                    //{
                    //    //UnityEngine.Debug.LogFormat("pre if {0} {1} {2}", x,y,z);
                    //    if (map[pos.x + 1, pos.y, pos.z].Transparent ||
                    //        map[pos.x - 1, pos.y, pos.z].Transparent ||
                    //        map[pos.x, pos.y + 1, pos.z].Transparent ||
                    //        map[pos.x, pos.y - 1, pos.z].Transparent ||
                    //        map[pos.x, pos.y, pos.z + 1].Transparent ||
                    //        map[pos.x, pos.y, pos.z - 1].Transparent
                    //    )
                    //    {
                    //        //UnityEngine.Debug.LogFormat("in if {0} {1} {2}", x,y,z);
                    //        renderLayer.Add(new Vector3Int(x, y, z));
                    //    }
                    //}
                    //catch (IndexOutOfRangeException) //assume neighboring chunks are all transparent
                    //{
                    //    renderLayer.Add(new Vector3Int(x, y, z));
                    //    //print(e.Message);
                    //}
                }
            }
        }
        return culled;
    }
}