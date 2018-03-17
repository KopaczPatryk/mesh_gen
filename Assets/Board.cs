using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Board : MonoBehaviour {
	public int Xsize = 4;
	public int Ysize = 4;
	public int Zsize = 4;
	//List<List<Block>> map;
	Block[, , ] map;
	void Awake () {
		// map = new List<List<Block>>(25);
		map = new Block[Xsize, Ysize, Zsize];
		//map.Capacity = 25;
		int c = 0;
		for (int z = 0; z < Zsize; z++) {
			for (int y = 0; y < Ysize; y++) {
				for (int x = 0; x < Xsize; x++) {
					map[x, y, z] = new Block();//(Block) ScriptableObject.CreateInstance (typeof (Block));
					c++;
				}
			}
		}
		Debug.LogFormat ("Mapa ma {0} obiektów", c);
	}

	public Block[,,] GetMapArray () {
		return map;
	}

}