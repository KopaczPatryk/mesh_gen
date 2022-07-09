using Assets.Scripts.WorldGen;
using Assets.Scripts.WorldGen.Blocks;
using UnityEngine;

namespace Assets.Scripts.MapGen {
    public interface IMapProvider {
        BaseBlock GetBlock(Vector3 posAbs);
        Chunk GetChunk(Vector3 posAbs, int chunkSize);
    }
}