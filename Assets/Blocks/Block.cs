using UnityEngine;

public abstract class Block : System.Object {
	protected const float scale = 1;

	[SerializeField]
	public int VertexCount {get; protected set;}
	[SerializeField]	
	public int UvCount {get; protected set; }
	[SerializeField]	
	public int Indices {get; protected set;}
	protected bool Built = false;

	//to change in the future, currently it allocates obsolete memory
	protected Mesh mesh; 
	public bool Transparent {get; protected set;}
	public abstract void PopulateMeshData();
	public abstract Mesh GetDrawData();
}
