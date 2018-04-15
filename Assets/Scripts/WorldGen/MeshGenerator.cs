using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MeshGen.WorldGen;
using UnityEngine;

namespace MeshGen {
	[RequireComponent(typeof(Chunk))]
	//[RequireComponent(typeof())]
	public class MeshGenerator : MonoBehaviour {
		private Chunk chunkData;
		private MeshFilter meshFilter;

		//generator
		Vector3[] verts;
		Vector2[] uvs;
		int[] tris;
		int vCount = 0;
		int uCount = 0;
		int tCount = 0;

		void Awake() {
			meshFilter = GetComponent<MeshFilter>();
			chunkData = GetComponent<Chunk>();
		}
		void Start(){
			//GenerateMesh();
		}
		public void GenerateMesh() {
			int vertCount = 0;
			int trianglesCount = 0;
			for (int z = 0; z < chunkData.Zsize; z++) {
				for (int y = 0; y < chunkData.Ysize; y++) {
					for (int x = 0; x < chunkData.Xsize; x++) {
						Block block = chunkData.GetMapArray() [x, y, z];

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
			// verts = new List<Vector3>();
			// tris = new List<int>();
			// uvs = new List<Vector2>();

			Stopwatch sw = new Stopwatch();
			sw.Start();

			//render visible blocks

			Block[, , ] map = chunkData.GetMapArray();

			List<Vector3> renderLayer = new List<Vector3>();

			//find visible blocks
			for (int z = 0; z < chunkData.Zsize; z++) {
				for (int y = 0; y < chunkData.Ysize; y++) {
					for (int x = 0; x < chunkData.Xsize; x++) {
						Vector3 pos = new Vector3(x, y, z);
						try {
							//UnityEngine.Debug.LogFormat("pre if {0} {1} {2}", x,y,z);
							if (map[(int) pos.x + 1, (int) pos.y, (int) pos.z].Transparent ||
								map[(int) pos.x - 1, (int) pos.y, (int) pos.z].Transparent ||
								map[(int) pos.x, (int) pos.y + 1, (int) pos.z].Transparent ||
								map[(int) pos.x, (int) pos.y - 1, (int) pos.z].Transparent ||
								map[(int) pos.x, (int) pos.y, (int) pos.z + 1].Transparent ||
								map[(int) pos.x, (int) pos.y, (int) pos.z - 1].Transparent
							) {
								//UnityEngine.Debug.LogFormat("in if {0} {1} {2}", x,y,z);
								renderLayer.Add(new Vector3(x, y, z));
							}
						} catch (IndexOutOfRangeException) //assume neighboring chunks are all transparent
						{
							renderLayer.Add(new Vector3(x, y, z));
							//print(e.Message);
						}
					}
				}
			}
			for (int a = 0; a < renderLayer.Count; a++) {

				int x = (int) renderLayer[a].x;
				int y = (int) renderLayer[a].y;
				int z = (int) renderLayer[a].z;
				Block block = map[x, y, z];

				//front
				try {
					if (map[x, y, z - 1].Transparent) {
						AppendFace(block, Face.Front, x, y, z);
					}
				} catch (IndexOutOfRangeException) //assume neighbor transparent
				{
					AppendFace(block, Face.Front, x, y, z);
				}
				//back
				try {
					if (map[x, y, z + 1].Transparent) {
						AppendFace(block, Face.Back, x, y, z);
					}
				} catch (IndexOutOfRangeException) //assume neighbor transparent
				{
					AppendFace(block, Face.Back, x, y, z);
				}
				//left
				try {
					if (map[x - 1, y, z].Transparent) {
						AppendFace(block, Face.Left, x, y, z);
					}
				} catch (IndexOutOfRangeException) //assume neighbor transparent
				{
					AppendFace(block, Face.Left, x, y, z);
				}
				//right
				try {
					if (map[x + 1, y, z].Transparent) {
						AppendFace(block, Face.Right, x, y, z);
					}
				} catch (IndexOutOfRangeException) //assume neighbor transparent
				{
					AppendFace(block, Face.Right, x, y, z);
				}
				//top
				try {
					if (map[x, y + 1, z].Transparent) {
						AppendFace(block, Face.Top, x, y, z);
					}
				} catch (IndexOutOfRangeException) //assume neighbor transparent
				{
					AppendFace(block, Face.Top, x, y, z);
				}
				//bottom
				try {
					if (map[x, y - 1, z].Transparent) {
						AppendFace(block, Face.Bottom, x, y, z);
					}
				} catch (IndexOutOfRangeException) //assume neighbor transparent
				{
					AppendFace(block, Face.Bottom, x, y, z);
				}

				//**************render visible blocks*********************

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

			} //dont comment it

			//************************render all faces*************************
			/*nt vCount = 0;
			int uCount = 0;
			int tCount = 0;
			//Block[,,] map = board.GetMapArray();
			for (int z = 0; z < board.Zsize; z++) {
				for (int y = 0; y < board.Ysize; y++) {
					for (int x = 0; x < board.Xsize; x++) {
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
				}
			}*/

			sw.Stop();
			UnityEngine.Debug.LogFormat(
				"Generation took: {0}ms for {1} verts and {2} triangles and {3} objects, taking avg {4}us per obj.",
				sw.ElapsedMilliseconds,
				vertCount,
				trianglesCount,
				chunkData.ObjectCount,
				sw.ElapsedTicks * 1000000 / Stopwatch.Frequency / chunkData.ObjectCount
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
			if (block.Sides == 0) {
				return;
			}
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

			Mesh tmesh = block.GetDrawData();
			int sideCount = block.Sides;
			int faceVertexCount = block.VertexCount / sideCount;
			int faceIndicesCount = block.Indices / sideCount;
			int faceUvCount = block.UvCount / sideCount;

			//vertex
			for (int offset = 0; offset < faceVertexCount; offset++) {
				// print("base " + tmesh.vertices[faceOrder * 4 + offset]);

				// print("calc " + (tmesh.vertices[faceOrder * 4 + offset] + new Vector3(xpos, ypos, zpos)));
				// print("add " + new Vector3(xpos, ypos, zpos));
				verts[vCount + offset] = tmesh.vertices[faceOrder * 4 + offset] + new Vector3(xpos, ypos, zpos);
			}
			//triangle
			for (int offset = 0; offset < faceIndicesCount; offset++) {
				tris[tCount + offset] = tmesh.triangles[faceOrder * 6 + offset] + vCount - faceOrder * 4;
			}
			//uvs
			//todo doesnt support multiple face textures
			for (int offset = 0; offset < faceUvCount; offset++) {
				uvs[uCount + offset] = tmesh.uv[faceOrder * 4 + offset];
			}
			vCount += 4;
			tCount += 6;
			uCount += 4;

		}
	}
}