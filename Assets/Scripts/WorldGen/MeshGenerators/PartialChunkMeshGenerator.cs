using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.WorldGen.Blocks;
using UnityEngine;

namespace Assets.Scripts.WorldGen.MeshGenerators {
    public class PartialChunkMeshGenerator : IChunkMeshGenerator {
        //generator
        private Vector3[] verts;
        private Vector2[] uvs;
        private int[] tris;

        private int vCount = 0;
        private int uCount = 0;
        private int tCount = 0;

        public RawMesh GenerateMesh(Chunk mapChunk) {
            BaseBlock[,,] map = mapChunk.Map;

            int vertCount = 0;
            int trianglesCount = 0;
            for (int z = 0; z < mapChunk.zSize; z++) {
                for (int y = 0; y < mapChunk.ySize; y++) {
                    for (int x = 0; x < mapChunk.xSize; x++) {
                        BaseBlock block = map[x, y, z];

                        if (block.CanBeRendered) {
                            vertCount += block.GetDrawData().Vertices.Length;
                            trianglesCount += block.GetDrawData().Triangles.Length;
                        }
                    }
                }
            }
            verts = new Vector3[vertCount];
            uvs = new Vector2[vertCount];
            tris = new int[trianglesCount];

            //render visible blocks
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<Vector3Int> renderLayer = new List<Vector3Int>();

            //find visible blocks
            int chunkSize = mapChunk.ChunkSize;
            for (int z = 0; z < mapChunk.zSize; z++) {
                for (int y = 0; y < mapChunk.ySize; y++) {
                    for (int x = 0; x < mapChunk.xSize; x++) {
                        //Vector3Int pos = new Vector3Int(x, y, z);
                        if (mapChunk.IsBlockVisible(new Vector3Int(x, y, z))) {
                            renderLayer.Add(new Vector3Int(x, y, z));
                        }

                        //try
                        //{
                        //    //UnityEngine.Debug.LogFormat("pre if {0} {1} {2}", x,y,z);
                        //    if (map[pos.x + 1, pos.y, pos.z].Transparent ||
                        //        map[pos.x - 1, pos.y, pos.z].Transparent ||
                        //        map[pos.x, pos.y + 1, pos.z].Transparent ||
                        //        map[pos.x, pos.y - 1, pos.z].Transparent ||
                        //        map[pos.x, pos.y, pos.z + 1].Transparent ||
                        //        map[pos.x, pos.y, pos.z - 1].Transparent
                        //    )
                        //    {
                        //        //UnityEngine.Debug.LogFormat("in if {0} {1} {2}", x,y,z);
                        //        renderLayer.Add(new Vector3Int(x, y, z));
                        //    }
                        //}
                        //catch (IndexOutOfRangeException) //assume neighboring chunks are all transparent
                        //{
                        //    renderLayer.Add(new Vector3Int(x, y, z));
                        //    //print(e.Message);
                        //}
                    }
                }
            }
            //process visible faces
            for (int a = 0; a < renderLayer.Count; a++) {
                int x = renderLayer[a].x;
                int y = renderLayer[a].y;
                int z = renderLayer[a].z;
                BaseBlock block = map[x, y, z];
                //front
                try {
                    if (map[x, y, z - 1].Transparent) {
                        AppendFace(block, Face.Front, x, y, z);
                    }
                } catch (IndexOutOfRangeException) //assume neighbor transparent
                  {
                    AppendFace(block, Face.Front, x, y, z);
                }
                //back
                try {
                    if (map[x, y, z + 1].Transparent) {
                        AppendFace(block, Face.Back, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Back, x, y, z);
                }
                //left
                try {
                    if (map[x - 1, y, z].Transparent) {
                        AppendFace(block, Face.Left, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Left, x, y, z);
                }
                //right
                try {
                    if (map[x + 1, y, z].Transparent) {
                        AppendFace(block, Face.Right, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Right, x, y, z);
                }
                //top
                try {
                    if (map[x, y + 1, z].Transparent) {
                        AppendFace(block, Face.Top, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Top, x, y, z);
                }
                //bottom
                try {
                    if (map[x, y - 1, z].Transparent) {
                        AppendFace(block, Face.Bottom, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Bottom, x, y, z);
                }
            }

            sw.Stop();

            UnityEngine.Debug.LogFormat(
               "Generation took: {0}us for {1} verts and {2} triangles and {3} objects, taking avg {4}us per obj.",
               sw.ElapsedTicks,
               vertCount,
               trianglesCount,
               mapChunk.Map.Length,
               sw.ElapsedTicks / mapChunk.Map.Length
            );
            vCount = 0;
            uCount = 0;
            tCount = 0;

            return new RawMesh {
                Vertices = verts,
                Uv = uvs,
                Triangles = tris
            };
        }
        private void AppendFace(BaseBlock block, Face face, int xPos, int yPos, int zPos) {
            if (!block.CanBeRendered) {
                return;
            }
            int faceOrder = face.GetHashCode();

            RawMesh tMesh = block.GetDrawData();
            int sideCount = block.Sides;
            int faceVertexCount = block.VertexCount / sideCount;
            int faceIndicesCount = block.Indices / sideCount;
            int faceUvCount = block.UvCount / sideCount;

            //vertex
            for (int offset = 0; offset < faceVertexCount; offset++) {
                verts[vCount + offset] = tMesh.Vertices[faceOrder * 4 + offset] + new Vector3(xPos, yPos, zPos);
            }
            //triangle
            for (int offset = 0; offset < faceIndicesCount; offset++) {
                tris[tCount + offset] = tMesh.Triangles[faceOrder * 6 + offset] + vCount - faceOrder * 4;
            }
            //uvs
            //todo doesnt support multiple face textures
            for (int offset = 0; offset < faceUvCount; offset++) {
                uvs[uCount + offset] = tMesh.Uv[faceOrder * 4 + offset];
            }
            vCount += 4;
            tCount += 6;
            uCount += 4;
        }
    }
}