using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
		Vector2[] uvs = new Vector2[vertCount];
        UnityEngine.Debug.LogFormat("Can hold max {0}, vertices.", verts.Length);
		int[] tris = new int[trianglesCount];

		Stopwatch sw = new Stopwatch();
		sw.Start();

		int vCount = 0;
		int uCount = 0;
		int tCount = 0;

		for (int z = 0; z < board.Zsize; z++) {
			for (int y = 0; y < board.Ysize; y++) {
				for (int x = 0; x < board.Xsize; x++) {
					Block block = board.GetMapArray() [x, y, z];
					Mesh tmesh = block.GetDrawData();
					//offset is used to loop over verices and triangles
					for (int offset = 0; offset < tmesh.vertices.Length; offset++) {
						verts[vCount + offset] = tmesh.vertices[offset] + new Vector3(x, y, z);
						uvs[uCount + offset] = tmesh.uv[offset];
					}
					for (int i = 0; i < tmesh.vertices.Length; i++)
					{
					}

					for (int i = 0; i < tmesh.triangles.Length; i++) {
						tris[tCount + i] = tmesh.triangles[i] + vCount;
					}
					vCount += tmesh.vertexCount;
					uCount += tmesh.vertexCount;
					tCount += tmesh.triangles.Length;
				}
			}
		}
		sw.Stop();
		UnityEngine.Debug.LogFormat(
			"Generation took: {0}ms for {1} verts and {2} triangles and {3} objects, taking avg {4}us per obj.",
			 sw.ElapsedMilliseconds,
			 vertCount,
			 trianglesCount,
			 board.ObjectCount,
			 sw.ElapsedTicks * 1000000 / Stopwatch.Frequency / board.ObjectCount
			 );

		meshFilter.mesh.vertices = verts;
		meshFilter.mesh.uv = uvs;
		meshFilter.mesh.triangles = tris;		
		meshFilter.mesh.RecalculateNormals();
		GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
	}
	private T printFallThrough<T>(string msg, T obj) {
		//print(msg + obj.ToString());
		return obj;
	}
}