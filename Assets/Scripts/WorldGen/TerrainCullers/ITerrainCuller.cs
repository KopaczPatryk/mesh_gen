using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WorldGen.TerrainCullers {
    public interface ITerrainCuller {
        List<Vector3Int> cull(Chunk chunk);
    }
}