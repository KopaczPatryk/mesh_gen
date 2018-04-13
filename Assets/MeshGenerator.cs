using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

		//render visible
		//int currentObjectCount = 0;
		bool[] partiallyVisible = new bool[board.ObjectCount];
		Block[, , ] map = board.GetMapArray();

		List<Vector3Int> renderLayer = new List<Vector3Int>();

		//bool lastTransparent = false;

		for (int z = 0; z < board.Zsize; z++) {
			for (int y = 0; y < board.Ysize; y++) {
				for (int x = 0; x < board.Xsize; x++) {
					Vector3Int pos = new Vector3Int(x, y, z);
					//checking direction:
					//to right
					//above
					//deeper - to front
					Block tblock = map[x, y, z];

					if (tblock != null) {
						try
						{
                                //UnityEngine.Debug.LogFormat("pre if {0} {1} {2}", x,y,z);
							if (map[pos.x + 1, pos.y, pos.z].Transparent ||
								map[pos.x - 1, pos.y, pos.z].Transparent ||
								map[pos.x, pos.y + 1, pos.z].Transparent ||
								map[pos.x, pos.y - 1, pos.z].Transparent ||
								map[pos.x, pos.y, pos.z + 1].Transparent ||
								map[pos.x, pos.y, pos.z - 1].Transparent
							) {
                                //UnityEngine.Debug.LogFormat("in if {0} {1} {2}", x,y,z);
								renderLayer.Add(new Vector3Int(x, y, z));
							}
						}
						catch (IndexOutOfRangeException) //assume neighboring chunks are all transparent
						{
							renderLayer.Add(new Vector3Int(x, y, z));
							//print(e.Message);
						}
						
					}
				}
			}
		}
		int vCount = 0;
		int uCount = 0;
		int tCount = 0;
		for (int a = 0; a < renderLayer.Count; a++) {
			int x = renderLayer[a].x;
			int y = renderLayer[a].y;
			int z = renderLayer[a].z;

			Block block = map[x, y, z];
			Mesh tmesh = block.GetDrawData();
			//offset is used to loop over verices and triangles
			for (int offset = 0; offset < tmesh.vertices.Length; offset++) {
				verts[vCount + offset] = tmesh.vertices[offset] + new Vector3(x, y, z);
			}
			for (int i = 0; i < block.UvCount; i++) {
				uvs[uCount + i] = tmesh.uv[i];
			}

			for (int i = 0; i < tmesh.triangles.Length; i++) {
				tris[tCount + i] = tmesh.triangles[i] + vCount;
			}
			vCount += tmesh.vertexCount;
			uCount += tmesh.vertexCount;
			tCount += tmesh.triangles.Length;
		}

		/*for (int z = 0; z < board.Zsize; z++) {
			for (int y = 0; y < board.Ysize; y++) {
				for (int x = 0; x < board.Xsize; x++) {

				}
			}
		}*/

		//render all
		/*		
		int vCount = 0;
		int uCount = 0;
		int tCount = 0;
		Block[,,] map = board.GetMapArray();
		for (int z = 0; z < board.Zsize; z++) {
			for (int y = 0; y < board.Ysize; y++) {
				for (int x = 0; x < board.Xsize; x++) {
					Block block = map[x, y, z];
					Mesh tmesh = block.GetDrawData();
					//offset is used to loop over verices and triangles
					for (int offset = 0; offset < tmesh.vertices.Length; offset++) {
						verts[vCount + offset] = tmesh.vertices[offset] + new Vector3(x, y, z);
					}
					for (int i = 0; i < block.UvCount; i++)
					{
						uvs[uCount + i] = tmesh.uv[i];
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
		*/
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
/*public static class VectorExtensions {
	public static Vector3Int above(this Vector3Int vec) {
		return vec + Vector3Int.up;
	}
	public static Vector3Int beneath(this Vector3Int vec) {
		return vec + Vector3Int.down;
	}
	public static Vector3Int front(this Vector3Int vec) {
		return vec + new Vector3Int(0, 0, 1);
	}
	public static Vector3Int behind(this Vector3Int vec) {
		return vec + new Vector3Int(0, 0, -1);
	}
	public static Vector3Int left(this Vector3Int vec) {
		return vec + Vector3Int.left;
	}
	public static Vector3Int right(this Vector3Int vec) {
		return vec + Vector3Int.right;
	}
	public static Vector3Int Tesst<T>(this T t) {
		return null;
	}
}*/