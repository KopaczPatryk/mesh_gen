using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.WorldGen.Blocks;
using Assets.Scripts.WorldGen.TerrainCullers;
using UnityEngine;

namespace Assets.Scripts.WorldGen.MeshGenerators {
    public class PartialChunkMeshGenerator : IChunkMeshGenerator {
        private ITerrainCuller terrainCuller;

        private Vector3[] verts;
        private Vector2[] uvs;
        private int[] tris;

        private int vCount = 0;
        private int uCount = 0;
        private int tCount = 0;

        public PartialChunkMeshGenerator(ITerrainCuller terrainCuller) {
            this.terrainCuller = terrainCuller;
        }

        public RawMesh GenerateMesh(Chunk chunk) {
            BaseBlock[,,] blocks = chunk.Blocks;

            int vertCount = 0;
            int trianglesCount = 0;
            for (int z = 0; z < chunk.zSize; z++) {
                for (int y = 0; y < chunk.ySize; y++) {
                    for (int x = 0; x < chunk.xSize; x++) {
                        BaseBlock block = blocks[x, y, z];

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

            List<Vector3Int> visibleBlockPositions = terrainCuller.cull(chunk);

            //process visible faces
            for (int a = 0; a < visibleBlockPositions.Count; a++) {
                int x = visibleBlockPositions[a].x;
                int y = visibleBlockPositions[a].y;
                int z = visibleBlockPositions[a].z;
                BaseBlock block = blocks[x, y, z];
                //front
                try {
                    if (blocks[x, y, z - 1].Transparent) {
                        AppendFace(block, Face.Front, x, y, z);
                    }
                } catch (IndexOutOfRangeException) //assume neighbor transparent
                  {
                    AppendFace(block, Face.Front, x, y, z);
                }
                //back
                try {
                    if (blocks[x, y, z + 1].Transparent) {
                        AppendFace(block, Face.Back, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Back, x, y, z);
                }
                //left
                try {
                    if (blocks[x - 1, y, z].Transparent) {
                        AppendFace(block, Face.Left, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Left, x, y, z);
                }
                //right
                try {
                    if (blocks[x + 1, y, z].Transparent) {
                        AppendFace(block, Face.Right, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Right, x, y, z);
                }
                //top
                try {
                    if (blocks[x, y + 1, z].Transparent) {
                        AppendFace(block, Face.Top, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(block, Face.Top, x, y, z);
                }
                //bottom
                try {
                    if (blocks[x, y - 1, z].Transparent) {
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
               chunk.Blocks.Length,
               sw.ElapsedTicks / chunk.Blocks.Length
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