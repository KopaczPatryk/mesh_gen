using System.Collections.Generic;
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
            for (int z = 0; z < chunk.ChunkSize; z++) {
                for (int y = 0; y < chunk.ChunkSize; y++) {
                    for (int x = 0; x < chunk.ChunkSize; x++) {
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

            List<Vector3Int> visibleBlockPositions = terrainCuller.cull(chunk);

            var mesh = initRawMesh(chunk);
            //process visible faces
            for (int a = 0; a < visibleBlockPositions.Count; a++) {
                int x = visibleBlockPositions[a].x;
                int y = visibleBlockPositions[a].y;
                int z = visibleBlockPositions[a].z;
                BaseBlock block = blocks[x, y, z];
                if (ChunkUtils.IsInsideOfChunk(x, y, z, chunk.ChunkSize)) {
                    //front
                    if (blocks[x, y, z - 1].Transparent) {
                        AppendFace(mesh, block, Face.Front, x, y, z);
                    }
                    //back
                    if (blocks[x, y, z + 1].Transparent) {
                        AppendFace(mesh, block, Face.Back, x, y, z);
                    }
                    //left
                    if (blocks[x - 1, y, z].Transparent) {
                        AppendFace(mesh, block, Face.Left, x, y, z);
                    }
                    //right
                    if (blocks[x + 1, y, z].Transparent) {
                        AppendFace(mesh, block, Face.Right, x, y, z);
                    }
                    //top
                    if (blocks[x, y + 1, z].Transparent) {
                        AppendFace(mesh, block, Face.Top, x, y, z);
                    }
                    //bottom
                    if (blocks[x, y - 1, z].Transparent) {
                        AppendFace(mesh, block, Face.Bottom, x, y, z);
                    }
                } else {
                    //front
                    if (!ChunkUtils.IsInChunkBounds(x, y, z - 1, chunk.ChunkSize) || blocks[x, y, z - 1].Transparent) {
                        AppendFace(mesh, block, Face.Front, x, y, z);
                    }
                    //back
                    if (!ChunkUtils.IsInChunkBounds(x, y, z + 1, chunk.ChunkSize) || blocks[x, y, z + 1].Transparent) {
                        AppendFace(mesh, block, Face.Back, x, y, z);
                    }
                    //left
                    if (!ChunkUtils.IsInChunkBounds(x - 1, y, z, chunk.ChunkSize) || blocks[x - 1, y, z].Transparent) {
                        AppendFace(mesh, block, Face.Left, x, y, z);
                    }
                    //right
                    if (!ChunkUtils.IsInChunkBounds(x + 1, y, z, chunk.ChunkSize) || blocks[x + 1, y, z].Transparent) {
                        AppendFace(mesh, block, Face.Right, x, y, z);
                    }
                    //top
                    if (!ChunkUtils.IsInChunkBounds(x, y + 1, z, chunk.ChunkSize) || blocks[x, y + 1, z].Transparent) {
                        AppendFace(mesh, block, Face.Top, x, y, z);
                    }
                    //bottom
                    if (!ChunkUtils.IsInChunkBounds(x, y - 1, z, chunk.ChunkSize) || blocks[x, y - 1, z].Transparent) {
                        AppendFace(mesh, block, Face.Bottom, x, y, z);
                    }
                }
            }

            return mesh;
        }
    }
}