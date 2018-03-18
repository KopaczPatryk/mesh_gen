using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kopacz.Unity.MeshGen.World {
	abstract class MeshFace : System.Object {

		protected Vector3[] verts;
		protected int[] triangles;

		protected abstract void TranslateBy();
		protected virtual void TranslateTo(Side side) {
				switch (side) {
					case Side.Back:
						verts[0] = new Vector3(0, 1, 1);
						verts[1] = new Vector3(0, 0, 1);
						verts[2] = new Vector3(1, 0, 1);
						verts[3] = new Vector3(1, 1, 1);
						break;
					case Side.Front:
						verts[0] = new Vector3(0, 1, 0);
						verts[1] = new Vector3(0, 0, 0);
						verts[2] = new Vector3(1, 0, 0);
						verts[3] = new Vector3(1, 1, 0);
						break;
					case Side.Left:

						break;
					case Side.Right:

						break;

				
			}
		}
		protected virtual void Rotate()
		{
			
		}
	}
}