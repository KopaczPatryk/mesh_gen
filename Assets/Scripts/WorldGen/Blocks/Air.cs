using UnityEngine;

namespace Assets.Scripts.WorldGen.Blocks {
    public class Air : BaseBlock {

        public Air() : base(transparent: true,
                            sides: 0,
                            uvCount: 0,
                            vertexCount: 0,
                            indices: 0) { }

        public override RawMesh GetDrawData() {
            return null;
        }
    }
}