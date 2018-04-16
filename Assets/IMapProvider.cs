using MeshGen.WorldGen;
using UnityEngine;

public interface IMapProvider {
    Block GetBlock(Vector3 posAbs);
    Block[,,] GetChunk(Vector3 posAbs, int chunkSize);
}