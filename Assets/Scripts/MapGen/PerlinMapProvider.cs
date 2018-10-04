using System.Collections;
using System.Collections.Generic;
using MeshGen.WorldGen;
using UnityEngine;

public class PerlinMapProvider : IMapProvider {
    private static IMapProvider instance;

    public static IMapProvider GetInstance()
    {
        instance = new PerlinMapProvider();
        return instance;
    }

    public Block GetBlock(Vector3 posAbs)
    {
        throw new System.NotImplementedException();
    }

    public Chunk GetChunk(Vector3 posAbs, int chunkSize)
    {
        Chunk chunk = new Chunk(chunkSize);
        //Debug.Log("pos: " + posAbs);
        
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                float h = Mathf.PerlinNoise(1000000f + (posAbs.x * chunkSize + x) / 40f, 1000000f + (posAbs.z * chunkSize + z) / 40f);
                h *= 20;
                //h += 8 * 3;

                for (int y = 0; y < chunkSize; y++)
                {
                    if (posAbs.y * chunkSize + y < h)
                    {
                        chunk.SetBlock(new Vector3Int(x, y, z), new Dirt());
                    }
                    else
                    {
                        chunk.SetBlock(new Vector3Int(x, y, z), new MeshGen.WorldGen.Space());
                    }
                }
            }
        }
        return chunk;
    }
}
