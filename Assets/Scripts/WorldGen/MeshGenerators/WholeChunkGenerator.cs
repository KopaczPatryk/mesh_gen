using MeshGen;
using MeshGen.WorldGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.WorldGen.MeshGenerators
{
    class WholeChunkGenerator : IChunkMeshGenerator
    {
        Vector3[] verts;
        Vector2[] uvs;
        int[] tris;

        int vCount = 0;
        int uCount = 0;
        int tCount = 0;

        public RawMesh GenerateMesh(Chunk chunkModel)
        {
            throw new NotImplementedException();
            //int vCount = 0;
            //int uCount = 0;
            //int tCount = 0;
            ////Block[,,] map = board.GetMapArray();
            //for (int z = 0; z < board.Zsize; z++)
            //{
            //    for (int y = 0; y < board.Ysize; y++)
            //    {
            //        for (int x = 0; x < board.Xsize; x++)
            //        {
            //            Block block = map[x, y, z];
            //            RawMesh tmesh = block.GetDrawData();
            //            //offset is used to loop over verices and triangles
            //            for (int offset = 0; offset < tmesh.vertices.Length; offset++)
            //            {
            //                verts[vCount + offset] = tmesh.vertices[offset] + new Vector3(x, y, z);
            //            }
            //            for (int i = 0; i < block.UvCount; i++)
            //            {
            //                uvs[uCount + i] = tmesh.uv[i];
            //            }

            //            for (int i = 0; i < tmesh.triangles.Length; i++)
            //            {
            //                tris[tCount + i] = tmesh.triangles[i] + vCount;
            //            }
            //            vCount += tmesh.vertexCount;
            //            uCount += tmesh.vertexCount;
            //            tCount += tmesh.triangles.Length;
            //        }
            //    }
            //}
        }
    }
}
