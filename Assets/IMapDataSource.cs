
using MeshGen.WorldGen;
using UnityEngine;

public interface IMapDataSource
{
    Block GetBlock(Vector3 posAbs);
    Chunk GetChunk(Vector3 posAbs);
}