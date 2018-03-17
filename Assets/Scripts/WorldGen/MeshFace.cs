using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kopacz.Unity.MeshGen.World {
	abstract class MeshFace : System.Object {

		protected Vector3[] triangles;
		protected int[] vertices;

		protected abstract void TranslateBy();
		protected virtual void TranslateTo(Side side) {
			switch (side)
			{
				case Side.Back:
					for (int i = 0; i < 3; i++)
					{
						
					}
				break;
				case Side.Front:

				break;
				case Side.Left:

				break;
				case Side.Right:

				break;
			}
		}
	}
}