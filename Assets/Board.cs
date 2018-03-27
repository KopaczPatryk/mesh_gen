﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Board : MonoBehaviour {
	public int Xsize = 4;
	public int Ysize = 4;
	public int Zsize = 4;
	//List<List<Block>> map;
	Block[, , ] map;
	public int ObjectCount = 0;
	void Awake() {
		ObjectCount = 0;
		map = new Block[Xsize, Ysize, Zsize];

		//creates copies of result, thus avoiding calculations
		//Dirt block = new Dirt();
		//block.PopulateMeshData();

		for (int z = 0; z < Zsize; z++) {
			for (int y = 0; y < Ysize; y++) {
				for (int x = 0; x < Xsize; x++) {
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