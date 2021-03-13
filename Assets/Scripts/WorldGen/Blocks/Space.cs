using UnityEngine;

namespace MeshGen.WorldGen {
    public class Space : BaseBlock {

        public Space() : base(transparent: true,
                              sides: 0,
                              uvCount: 0,
                              vertexCount: 0,
                              indices: 0) { }

        public override RawMesh GetDrawData() {
            return null;
        }
    }
}