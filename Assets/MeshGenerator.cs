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

	//generator
	Vector3[] verts;
	Vector2[] uvs;
	int[] tris;
	int vCount = 0;
	int uCount = 0;
	int tCount = 0;

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

		verts = new Vector3[vertCount];
		uvs = new Vector2[vertCount];
		UnityEngine.Debug.LogFormat("Can hold max {0}, vertices.", verts.Length);
		tris = new int[trianglesCount];

		Stopwatch sw = new Stopwatch();
		sw.Start();

		//render visible

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
					//Block tblock = map[x, y, z];

					//if (tblock != null) {
					try {
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
					} catch (IndexOutOfRangeException) //assume neighboring chunks are all transparent
					{
						renderLayer.Add(new Vector3Int(x, y, z));
						//print(e.Message);
					}

					//}
				}
			}
		}
		vCount = 0;
		uCount = 0;
		tCount = 0;
		for (int a = 0; a < renderLayer.Count; a++) {

			int x = renderLayer[a].x;
			int y = renderLayer[a].y;
			int z = renderLayer[a].z;

			Block block = map[x, y, z];
			Mesh tmesh = block.GetDrawData();

			//int sideCount = block.Sides;
			int VertexCount = block.VertexCount / 6;
			int IndicesCount = block.Indices / 6;
			int UvCount = block.UvCount / 6;

			//vertex
			/*for (int offset = 0; offset < VertexCount; offset++) {
				verts[vCount + offset] = tmesh.vertices[block.FrontFaceOrder * VertexCount + offset] + new Vector3(x, y, z);
				//verts[vCount + offset] = tmesh.vertices[block.FrontFaceOrder * VertexCount + offset];
			}
			//triangle
			for (int offset = 0; offset < IndicesCount; offset++) {
				tris[tCount + offset] = tmesh.triangles[block.FrontFaceOrder * IndicesCount + offset] + vCount;
			}
			//uvs
			//todo doesnt support multiple face textures
			for (int offset = 0; offset < UvCount; offset++) {
				uvs[uCount + offset] = tmesh.uv[block.FrontFaceOrder * UvCount + offset];
			}

			vCount += VertexCount;
			uCount += UvCount;
			tCount += IndicesCount;*/

			//AppendFace(block, Face.Front, x, y, z);					

			//front
			try {
				if (map[x, y, z - 1].Transparent) {
					AppendFace(block, Face.Front, x, y, z);
				}
			} catch (IndexOutOfRangeException) {
				AppendFace(block, Face.Front, x, y, z);
				//print("f");
			}
			//back
			/*try {
				if (map[x, y, z - 1].Transparent) {
					AppendFace(block, Face.Back, x, y, z);
				}
			} catch (IndexOutOfRangeException) {
				AppendFace(block, Face.Back, x, y, z);
				//print("b");

			} finally { }*/

			//right
			/*try {
				if (map[x + 1, y, z].Transparent) {
					AppendFace(block, Face.Right);
				}
			} catch (IndexOutOfRangeException) {
print("r");

			} finally {
				AppendFace(block, Face.Right);

			}
			//left
			try {
				if (map[x - 1, y, z].Transparent) {
					AppendFace(block, Face.Left);
				}
			} catch (IndexOutOfRangeException) {
print("l");

			} finally {
				AppendFace(block, Face.Left);
			}
			
			//top
			try {
				if (map[x, y + 1, z].Transparent) {
					AppendFace(block, Face.Top);
				}
			} catch (IndexOutOfRangeException) {
print("t");

			} finally {
				AppendFace(block, Face.Top);
			}
			
			//bottom
			try {
				if (map[x, y - 1, z].Transparent) {
					AppendFace(block, Face.Bottom);
				}
			} catch (IndexOutOfRangeException) {
print("bo");

			} finally {
				AppendFace(block, Face.Bottom);
			}*/

			//works but renrders whole blocks
			// map[pos.x + 1, pos.y, pos.z].Transparent ||
			// map[pos.x - 1, pos.y, pos.z].Transparent ||
			// map[pos.x, pos.y + 1, pos.z].Transparent ||
			// map[pos.x, pos.y - 1, pos.z].Transparent ||
			// map[pos.x, pos.y, pos.z + 1].Transparent ||
			// map[pos.x, pos.y, pos.z - 1].Transparent

			//offset is used to loop over verices and triangles
			/*for (int offset = 0; offset < tmesh.vertices.Length; offset++) {
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
			tCount += tmesh.triangles.Length;*/
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
	private void AppendFace(Block block, Face face, int xpos, int ypos, int zpos) {
		Mesh tmesh = block.GetDrawData();
		int faceOrder = 0;

		switch (face) {
			case Face.Front:
				faceOrder = block.FrontFaceOrder;
				break;
			case Face.Back:
				faceOrder = block.BackFaceOrder;
				break;
			case Face.Right:
				faceOrder = block.RightFaceOrder;
				break;
			case Face.Left:
				faceOrder = block.LeftFaceOrder;
				break;
			case Face.Top:
				faceOrder = block.TopFaceOrder;
				break;
			case Face.Bottom:
				faceOrder = block.BottomFaceOrder;
				break;
		}

		//int sideCount = block.Sides;
		int faceVertexCount = block.VertexCount / 6;
		int faceIndicesCount = block.Indices / 6;
		int faceUvCount = block.UvCount / 6;

		for (int offset = 0; offset < faceVertexCount; offset++) {
			verts[vCount + offset] = tmesh.vertices[faceOrder * faceVertexCount + offset] + new Vector3(xpos, ypos, zpos);
		}
		//triangle
		for (int offset = 0; offset < faceIndicesCount; offset++) {
			tris[tCount + offset] = tmesh.triangles[faceOrder * faceIndicesCount + offset] + vCount;
		}
		//uvs
		//todo doesnt support multiple face textures
		for (int offset = 0; offset < faceUvCount; offset++) {
			uvs[uCount + offset] = tmesh.uv[faceOrder * faceUvCount + offset];
		}

		vCount += faceVertexCount;
		uCount += faceUvCount;
		tCount += faceIndicesCount;
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