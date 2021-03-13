using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.WorldGen.Blocks;
using Assets.Scripts.WorldGen.TerrainCullers;
using UnityEngine;

namespace Assets.Scripts.WorldGen.MeshGenerators {
    public class PartialChunkMeshGenerator : IChunkMeshGenerator {
        private ITerrainCuller terrainCuller;

        public PartialChunkMeshGenerator(ITerrainCuller terrainCuller) {
            this.terrainCuller = terrainCuller;
        }
        private RawMesh initRawMesh(Chunk chunk) {
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

            return new RawMesh {
                Vertices = new Vector3[vertCount],
                Uv = new Vector2[vertCount],
                Triangles = new int[trianglesCount]
            };
        }

        private void AppendFace(RawMesh mesh, BaseBlock block, Face face, int xPos, int yPos, int zPos) {
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
                mesh.Vertices[mesh.vertexCount + offset] = tMesh.Vertices[faceOrder * 4 + offset] + new Vector3(xPos, yPos, zPos);
            }
            //triangle
            for (int offset = 0; offset < faceIndicesCount; offset++) {
                mesh.Triangles[mesh.trianglesCount + offset] = tMesh.Triangles[faceOrder * 6 + offset] + mesh.vertexCount - faceOrder * 4;
            }
            //uvs
            //todo doesnt support multiple face textures
            for (int offset = 0; offset < faceUvCount; offset++) {
                mesh.Uv[mesh.uvCount + offset] = tMesh.Uv[faceOrder * 4 + offset];
            }
            mesh.vertexCount += 4;
            mesh.trianglesCount += 6;
            mesh.uvCount += 4;
        }
        public RawMesh GenerateMesh(Chunk chunk) {
            BaseBlock[,,] blocks = chunk.Blocks;

            //render visible blocks
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<Vector3Int> visibleBlockPositions = terrainCuller.cull(chunk);
            var mesh = initRawMesh(chunk);
            //process visible faces
            for (int a = 0; a < visibleBlockPositions.Count; a++) {
                int x = visibleBlockPositions[a].x;
                int y = visibleBlockPositions[a].y;
                int z = visibleBlockPositions[a].z;
                BaseBlock block = blocks[x, y, z];
                //front
                try {
                    if (blocks[x, y, z - 1].Transparent) {
                        AppendFace(mesh, block, Face.Front, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(mesh, block, Face.Front, x, y, z);
                }
                //back
                try {
                    if (blocks[x, y, z + 1].Transparent) {
                        AppendFace(mesh, block, Face.Back, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(mesh, block, Face.Back, x, y, z);
                }
                //left
                try {
                    if (blocks[x - 1, y, z].Transparent) {
                        AppendFace(mesh, block, Face.Left, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(mesh, block, Face.Left, x, y, z);
                }
                //right
                try {
                    if (blocks[x + 1, y, z].Transparent) {
                        AppendFace(mesh, block, Face.Right, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(mesh, block, Face.Right, x, y, z);
                }
                //top
                try {
                    if (blocks[x, y + 1, z].Transparent) {
                        AppendFace(mesh, block, Face.Top, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(mesh, block, Face.Top, x, y, z);
                }
                //bottom
                try {
                    if (blocks[x, y - 1, z].Transparent) {
                        AppendFace(mesh, block, Face.Bottom, x, y, z);
                    }
                } catch (IndexOutOfRangeException) {
                    //assume neighbor transparent
                    AppendFace(mesh, block, Face.Bottom, x, y, z);
                }
            }

            sw.Stop();

            UnityEngine.Debug.LogFormat(
               "Generation took: {0}us for {1} verts and {2} triangles and {3} objects, taking avg {4}us per obj.",
               sw.ElapsedTicks,
               mesh.Vertices.Length,
               mesh.Triangles.Length,
               chunk.Blocks.Length,
               sw.ElapsedTicks / chunk.Blocks.Length
            );

            return mesh;
        }
    }
}