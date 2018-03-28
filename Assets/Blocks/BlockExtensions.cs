using Kopacz.Unity.MeshGen.World;
using UnityEngine;

namespace unity_meshgen.Assets.Blocks
{
    public static class BlockExtensions
    {
        public static Mesh GetSideMesh(this Block block, Side side) 
        {
            Mesh partialMesh = new Mesh();
            partialMesh.vertices = new Vector3[4];
            partialMesh.uv = new Vector2[4];
            partialMesh.triangles = new int[6];

            Vector3[] tVertices = new Vector3[4];
            Vector2[] tUvs = new Vector2[4];
            int[] tTriangles = new int[6];

            int VUVOffset = 0;
            int triOffset = 0;
            switch (side)
            {
                case Side.Front:
                    VUVOffset = 0;
                    triOffset = 0;
                break;
                case Side.Back:
                    VUVOffset = 4;
                    triOffset = 6;
                break;
                case Side.Right:
                    VUVOffset = 8;
                    triOffset = 12;
                break;
                case Side.Left:
                    VUVOffset = 12;
                    triOffset = 18;
                break;
                case Side.Top:
                    VUVOffset = 16;
                    triOffset = 24;
                break;
                case Side.Bottom:
                    VUVOffset = 20;
                    triOffset = 30;
                break;

            }
            for (int i = 0; i < 4; i++)
            {
                tVertices[i] = block.GetDrawData().vertices[i + VUVOffset];
                tUvs[i] = block.GetDrawData().uv[i + VUVOffset];
            }
            for (int i = 0; i < 6; i++)
            {
                tTriangles[i] = block.GetDrawData().triangles[i + triOffset];                        
            }
            return partialMesh;
        }
    }
}