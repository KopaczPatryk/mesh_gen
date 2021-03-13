using MeshGen.WorldGen;
using UnityEngine;

public interface IMapProvider {
    BaseBlock GetBlock(Vector3 posAbs);
    Chunk GetChunk(Vector3 posAbs, int chunkSize);
}