﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : System.Object {
	protected const float scale = 0.8f;
	//protected const int vertCount = 6;
	Mesh mesh;
	public readonly int VertexCount;
	public readonly int Indices;
	protected bool built = false;
	public Block() {
		VertexCount = 24;
		Indices = 36;
	}
	public Block(Block toClone) {
		Indices = toClone.Indices;
		VertexCount = toClone.VertexCount;
		mesh = toClone.mesh;
		built = true;
	}
	public void PopulateMeshData() {
		if (!built) {
			mesh = new Mesh();
			/****************VERICES************** */
			Vector3[] verts = new Vector3[VertexCount];
			//front
			verts[0] = new Vector3(0, 1, 0);
			verts[1] = new Vector3(0, 0, 0);
			verts[2] = new Vector3(1, 0, 0);
			verts[3] = new Vector3(1, 1, 0);
			
			//back
			verts[4] = new Vector3(0,1,1);
			verts[5] = new Vector3(0,0,1);
			verts[6] = new Vector3(1,0,1);
			verts[7] = new Vector3(1,1,1);

			//right
			verts[8] = new Vector3(1,1,0);
			verts[9] = new Vector3(1,0,0);
			verts[10] = new Vector3(1,0,1);
			verts[11] = new Vector3(1,1,1);

			//left
			verts[12] = new Vector3(0,1,1);
			verts[13] = new Vector3(0,0,1);
			verts[14] = new Vector3(0,0,0);
			verts[15] = new Vector3(0,1,0);

			//top
			verts[16] = new Vector3(0,1,1);
			verts[17] = new Vector3(0,1,0);
			verts[18] = new Vector3(1,1,0);
			verts[19] = new Vector3(1,1,1);

			//bottom
			verts[20] = new Vector3(0,0,1);
			verts[21] = new Vector3(0,0,0);
			verts[22] = new Vector3(1,0,0);
			verts[23] = new Vector3(1,0,1);
			
			/***************TRIANGLES***************** */

			int[] tri = new int[Indices];
			//front
			tri[0] = 2;
			tri[1] = 1;
			tri[2] = 0;
			tri[3] = 0;
			tri[4] = 3;
			tri[5] = 2;
			
			//back
			tri[6] = 4 + 0;
			tri[7] = 4 + 1;
			tri[8] = 4 + 2;
			tri[9] = 4 + 2;
			tri[10] = 4 + 3;
			tri[11] = 4 + 0;
			
			//right
			tri[12] = 8 + 2;
			tri[13] = 8 + 1;
			tri[14] = 8 + 0;
			tri[15] = 8 + 0;
			tri[16] = 8 + 3;
			tri[17] = 8 + 2;
			
			//left
			tri[18] = 12 + 2;
			tri[19] = 12 + 1;
			tri[20] = 12 + 0;
			tri[21] = 12 + 0;
			tri[22] = 12 + 3;
			tri[23] = 12 + 2;

			//top 
			tri[24] = 16 + 2;
			tri[25] = 16 + 1;
			tri[26] = 16 + 0;
			tri[27] = 16 + 0;
			tri[28] = 16 + 3;
			tri[29] = 16 + 2;

			//bottom 
			tri[30] = 20 + 0;
			tri[31] = 20 + 1;
			tri[32] = 20 + 2;
			tri[33] = 20 + 2;
			tri[34] = 20 + 3;
			tri[35] = 20 + 0;

			for (int i = 0; i < VertexCount; i++) {
				verts[i] *= scale; // new Vector3(.5f, .5f, .5f);
			}
			mesh.vertices = verts;
			mesh.triangles = tri;
			built = true;
		}
	}
	public Mesh GetDrawData() {
		//mesh = new Mesh();
		PopulateMeshData();
		return mesh;
	}
	/*
	/// <summary>
	/// 
	/// </summary>
	/// <param name="mesh">Optional mesh to avoid new memory allocations</param>
	/// <returns></returns>
	public Mesh GetDrawData(ref Mesh mesh) {
		//PopulateMeshData(ref mesh);
		return mesh;
	} 
	*/
}