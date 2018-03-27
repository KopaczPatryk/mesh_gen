
using UnityEngine;

public abstract class Block : System.Object {
	protected const float scale = 1;

	[SerializeField]
	protected int VertexCount;

	[SerializeField]	
	protected int Indices;
	protected bool Built = false;
	protected Mesh mesh;
	protected bool Transparent = false;

	public abstract void PopulateMeshData();
	public abstract Mesh GetDrawData();
}
