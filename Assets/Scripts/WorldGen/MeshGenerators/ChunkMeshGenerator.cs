using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using MeshGen.WorldGen;
using UnityEngine;

namespace MeshGen
{
    public class ChunkMeshGenerator : IChunkMeshGenerator
    {
        //generator
        Vector3[] verts;
        Vector2[] uvs;
        int[] tris;

        int vCount = 0;
        int uCount = 0;
        int tCount = 0;

        public RawMesh GenerateMesh(Chunk chunkModel)
        {
            int vertCount = 0;
            int trianglesCount = 0;
            for (int z = 0; z < chunkModel.Zsize; z++)
            {
                for (int y = 0; y < chunkModel.Ysize; y++)
                {
                    for (int x = 0; x < chunkModel.Xsize; x++)
                    {
                        Block block = chunkModel.GetMapArray()[x, y, z];

                        vertCount += block.GetDrawData().Vertices.Length;
                        trianglesCount += block.GetDrawData().Triangles.Length;
                    }
                }
            }
            verts = new Vector3[vertCount];
            uvs = new Vector2[vertCount];
            tris = new int[trianglesCount];

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //render visible blocks
            Block[,,] map = chunkModel.GetMapArray();

            List<Vector3> renderLayer = new List<Vector3>();

            //find visible blocks
            for (int z = 0; z < chunkModel.Zsize; z++)
            {
                for (int y = 0; y < chunkModel.Ysize; y++)
                {
                    for (int x = 0; x < chunkModel.Xsize; x++)
                    {
                        Vector3 pos = new Vector3(x, y, z);
                        try
                        {
                            //UnityEngine.Debug.LogFormat("pre if {0} {1} {2}", x,y,z);
                            if (map[(int)pos.x + 1, (int)pos.y, (int)pos.z].Transparent ||
                                map[(int)pos.x - 1, (int)pos.y, (int)pos.z].Transparent ||
                                map[(int)pos.x, (int)pos.y + 1, (int)pos.z].Transparent ||
                                map[(int)pos.x, (int)pos.y - 1, (int)pos.z].Transparent ||
                                map[(int)pos.x, (int)pos.y, (int)pos.z + 1].Transparent ||
                                map[(int)pos.x, (int)pos.y, (int)pos.z - 1].Transparent
                            )
                            {
                                //UnityEngine.Debug.LogFormat("in if {0} {1} {2}", x,y,z);
                                renderLayer.Add(new Vector3(x, y, z));
                            }
                        }
                        catch (IndexOutOfRangeException) //assume neighboring chunks are all transparent
                        {
                            renderLayer.Add(new Vector3(x, y, z));
                            //print(e.Message);
                        }
                    }
                }
            }
            //process visible faces
            for (int a = 0; a < renderLayer.Count; a++)
            {

                int x = (int)renderLayer[a].x;
                int y = (int)renderLayer[a].y;
                int z = (int)renderLayer[a].z;
                Block block = map[x, y, z];

                //front
                try
                {
                    if (map[x, y, z - 1].Transparent)
                    {
                        AppendFace(block, Face.Front, x, y, z);
                    }
                }
                catch (IndexOutOfRangeException) //assume neighbor transparent
                {
                    AppendFace(block, Face.Front, x, y, z);
                }
                //back
                try
                {
                    if (map[x, y, z + 1].Transparent)
                    {
                        AppendFace(block, Face.Back, x, y, z);
                    }
                }
                catch (IndexOutOfRangeException) //assume neighbor transparent
                {
                    AppendFace(block, Face.Back, x, y, z);
                }
                //left
                try
                {
                    if (map[x - 1, y, z].Transparent)
                    {
                        AppendFace(block, Face.Left, x, y, z);
                    }
                }
                catch (IndexOutOfRangeException) //assume neighbor transparent
                {
                    AppendFace(block, Face.Left, x, y, z);
                }
                //right
                try
                {
                    if (map[x + 1, y, z].Transparent)
                    {
                        AppendFace(block, Face.Right, x, y, z);
                    }
                }
                catch (IndexOutOfRangeException) //assume neighbor transparent
                {
                    AppendFace(block, Face.Right, x, y, z);
                }
                //top
                try
                {
                    if (map[x, y + 1, z].Transparent)
                    {
                        AppendFace(block, Face.Top, x, y, z);
                    }
                }
                catch (IndexOutOfRangeException) //assume neighbor transparent
                {
                    AppendFace(block, Face.Top, x, y, z);
                }
                //bottom
                try
                {
                    if (map[x, y - 1, z].Transparent)
                    {
                        AppendFace(block, Face.Bottom, x, y, z);
                    }
                }
                catch (IndexOutOfRangeException) //assume neighbor transparent
                {
                    AppendFace(block, Face.Bottom, x, y, z);
                }
                

            }

            //sw.Stop();
            //UnityEngine.Debug.LogFormat(
            //    "Generation took: {0}ms for {1} verts and {2} triangles and {3} objects, taking avg {4}us per obj.",
            //    sw.ElapsedMilliseconds,
            //    vertCount,
            //    trianglesCount,
            //    chunkModel.GetMapArray().Length,
            //    sw.ElapsedTicks * 1000000 / Stopwatch.Frequency / chunkModel.GetMapArray().Length
            //);

            return new RawMesh
            {
                Vertices = verts,
                Uv = uvs,
                Triangles = tris
            };
        }
        
        private void AppendFace(Block block, Face face, int xpos, int ypos, int zpos)
        {
            if (block.Sides == 0)
            {
                return;
            }
            int faceOrder = 0;

            switch (face)
            {
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

            RawMesh tmesh = block.GetDrawData();
            int sideCount = block.Sides;
            int faceVertexCount = block.VertexCount / sideCount;
            int faceIndicesCount = block.Indices / sideCount;
            int faceUvCount = block.UvCount / sideCount;

            //vertex
            for (int offset = 0; offset < faceVertexCount; offset++)
            {
                verts[vCount + offset] = tmesh.Vertices[faceOrder * 4 + offset] + new Vector3(xpos, ypos, zpos);
            }
            //triangle
            for (int offset = 0; offset < faceIndicesCount; offset++)
            {
                tris[tCount + offset] = tmesh.Triangles[faceOrder * 6 + offset] + vCount - faceOrder * 4;
            }
            //uvs
            //todo doesnt support multiple face textures
            for (int offset = 0; offset < faceUvCount; offset++)
            {
                uvs[uCount + offset] = tmesh.Uv[faceOrder * 4 + offset];
            }
            vCount += 4;
            tCount += 6;
            uCount += 4;
        }
    }
}