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
        //float min = 1f / 2147483647;
        //float c = 1f / 1073741823;
        Debug.Log("pos: " + posAbs);

        //int half = 1073741823;
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                float h = Mathf.PerlinNoise(10000f + (posAbs.x * chunkSize + x) / 20f, 10000f + (posAbs.z * chunkSize + z) / 20f);
                h *= 10;
                //h += 10;
                //Debug.Log("h" + h);
                //Debug.Log("perlin" + (100f + (posAbs.x * chunkSize + x) / 10f));

                for (int y = 0; y < chunkSize; y++)
                {
                    //Debug.Log("coord x " + posAbs.x + x);
                    //Debug.Log("coord y " + posAbs.y + y);

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
