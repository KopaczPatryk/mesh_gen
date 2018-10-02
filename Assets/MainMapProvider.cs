using System.Collections;
using System.Collections.Generic;
using MeshGen.WorldGen;
using UnityEngine;

public class MainMapProvider : IMapProvider {

	private IMapProvider savedMap;
	private IMapProvider mapGenerator;
	private static IMapProvider instance;

	public static IMapProvider GetInstance() {
		instance = new MainMapProvider();
		return instance;
	}

    public Block GetBlock(Vector3 posAbs)
    {
        if (posAbs.y > 8)
		{
			return new MeshGen.WorldGen.Space();
		}
		else 
			return new Dirt();
    }

    public Chunk GetChunk(Vector3 posAbs, int chunkSize)
    {
		Chunk chun = new Chunk(chunkSize);
		int ObjectCount = 0;
		for (int z = 0; z < chunkSize; z++) {
			for (int y = 0; y < chunkSize; y++) {
				for (int x = 0; x < chunkSize; x++) {
					//multiple smaller cubes in chunks
					if (x % 4 != 0 && y % 4 != 0 && z % 4 != 0)
					{
						chun.SetBlock(new Vector3Int(x,y,z), new Dirt());
					}
					else
					{
						chun.SetBlock(new Vector3Int(x,y,z), new MeshGen.WorldGen.Space());
					}

					// if (ObjectCount % 6 == 0)
					// {
					// 	tchunk[x, y, z] = new Dirt();
					// }
					// else
					// {
					// 	tchunk[x, y, z] = new MeshGen.WorldGen.Space();						
					// }

					//map[x, y, z] = new Dirt();

					ObjectCount++;
				}
			}
		}
        return chun;
    }
}
