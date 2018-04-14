using UnityEngine;

namespace MeshGen.WorldGen {
	public abstract class Block : System.Object {
		protected const float scale = 1;

		[SerializeField]
		public int VertexCount { get; protected set; }

		[SerializeField]
		public int UvCount { get; protected set; }

		[SerializeField]
		public int Indices { get; protected set; }
		protected bool Built = false;

		//to change in the future, currently it allocates obsolete memory
		protected Mesh mesh;
		public bool Transparent { get; protected set; }
		public abstract void PopulateMeshData();
		public abstract Mesh GetDrawData();

		public virtual int Sides { get { return 0; } }
		public virtual int FrontFaceOrder { get { return 0; } }
		public virtual int BackFaceOrder { get { return 0; } }
		public virtual int RightFaceOrder { get { return 0; } }
		public virtual int LeftFaceOrder { get { return 0; } }
		public virtual int TopFaceOrder { get { return 0; } }
		public virtual int BottomFaceOrder { get { return 0; } }

		/*public abstract Mesh FrontFace();
		public abstract Mesh BackFace();
		public abstract Mesh RightFace();
		public abstract Mesh LeftFace();
		public abstract Mesh TopFace();
		public abstract Mesh BottomFace();*/
	}
}