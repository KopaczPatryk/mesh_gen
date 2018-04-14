using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Board : MonoBehaviour {
	public int Xsize = 4;
	public int Ysize = 4;
	public int Zsize = 4;
	Block[, , ] map;
	public int ObjectCount = 0;
	void Awake() {
		ObjectCount = 0;
		map = new Block[Xsize, Ysize, Zsize];

		for (int z = 0; z < Zsize; z++) {
			for (int y = 0; y < Ysize; y++) {
				for (int x = 0; x < Xsize; x++) {
					// multiple smaller cubes in chunks
					// if (x % 4 != 0 && y % 4 != 0 && z % 4 != 0)
					// {
					// 	map[x, y, z] = new Dirt();
					// }
					// else
					// {
					// 	map[x, y, z] = new Blocks.Space();						
					// }

					// if (ObjectCount % 2 == 0)
					// {
					// 	map[x, y, z] = new Dirt();
					// }
					// else
					// {
					// 	map[x, y, z] = new Blocks.Space();						
					// }

					map[x, y, z] = new Dirt();
					
					ObjectCount++;
				}
			}
		}
	}

	public Block[, , ] GetMapArray() {
		return map;
	}
}