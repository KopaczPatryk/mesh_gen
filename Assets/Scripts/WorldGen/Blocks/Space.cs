using UnityEngine;

namespace MeshGen.WorldGen {
    public class Space : BaseBlock {

        public Space() : base(built: true,
                              transparent: true,
                              uvCount: 0,
                              sides: 0,
                              vertexCount: 0) { }

        public override RawMesh GetDrawData() {
            return new RawMesh();
        }
    }
}