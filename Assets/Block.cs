using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : System.Object {
	protected const float scale = 0.5f;
	protected const int vertCount = 0;

	public Block() {
		// localMesh = new Mesh();
	}
	private void PopulateMeshData(ref Mesh mesh) {
		Vector3[] verts = new Vector3[6];
		verts[0] = new Vector3(0,1,0);
		verts[1] = new Vector3(0,0,0);
		verts[2] = new Vector3(1,0,0);
		
		verts[3] = new Vector3(0,1,0);		
		verts[4] = new Vector3(1,0,0);
		verts[5] = new Vector3(1,1,0);

		int[] tri = new int[6];
		tri[0] = 2; 
		tri[1] = 1; 
		tri[2] = 0; 
		tri[3] = 5; 
		tri[4] = 4; 
		tri[5] = 3; 
		mesh.vertices = verts;
		mesh.triangles = tri;
	}
	public Mesh GetDrawData() {
		Mesh localMesh = new Mesh();
		PopulateMeshData(ref localMesh);
		return localMesh;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="mesh">Optional mesh to avoid new memory allocations</param>
	/// <returns></returns>
	public Mesh GetDrawData(ref Mesh mesh) {
		PopulateMeshData(ref mesh);
		return mesh;
	}
}