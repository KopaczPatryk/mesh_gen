using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {
	public Board board;
	MeshFilter meshFilter;

	void Awake() {
		meshFilter = GetComponent<MeshFilter>();
	}

	void Start() {
		int vertCount = 0;
		int trianglesCount = 0;
		for (int z = 0; z < board.Zsize; z++) {
			for (int y = 0; y < board.Ysize; y++) {
				for (int x = 0; x < board.Xsize; x++) {
					Block block = board.GetMapArray() [x, y, z];

					vertCount += block.GetDrawData().vertexCount;
					trianglesCount += block.GetDrawData().triangles.Length;
				}
			}
		}

//		print("verts1: " + vertCount);
//		print("tris1: " + trianglesCount);

		Vector3[] verts = new Vector3[vertCount];
		Debug.LogFormat("Can hold max {0}, vertices.", verts.Length);
		int[] tris = new int[trianglesCount];

		int vCount = 0;
		int tCount = 0;

		for (int z = 0; z < board.Zsize; z++) {
			for (int y = 0; y < board.Ysize; y++) {
				for (int x = 0; x < board.Xsize; x++) {
					Block block = board.GetMapArray() [x, y, z];
					Mesh tmesh = block.GetDrawData();
					//offset is used to loop over verices and triangles
					for (int offset = 0; offset < tmesh.vertices.Length; offset++) {
						verts[vCount + offset] = tmesh.vertices[offset] + new Vector3(x, y, z);
					}
					vCount += tmesh.vertexCount;

					for (int i = 0; i < tmesh.triangles.Length; i++) {
						tris[tCount + i] = tmesh.triangles[i] + tCount;
					}
					tCount += tmesh.triangles.Length;
				}
			}
		}
		meshFilter.mesh.vertices = verts;
		meshFilter.mesh.triangles = tris;
	}
	private T printFallThrough<T>(string msg, T obj) {
		//print(msg + obj.ToString());
		return obj;
	}
}