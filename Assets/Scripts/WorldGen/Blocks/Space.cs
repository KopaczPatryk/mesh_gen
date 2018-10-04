using UnityEngine;

namespace MeshGen.WorldGen {
    public class Space : Block {
        public new virtual int Sides { get { return 0; } }
        public Space() {

            VertexCount = 0;
            UvCount = 0;
            Indices = 0;
            Built = true;
            Transparent = true;
        }

        public override RawMesh GetDrawData() {
            return new RawMesh();
        }

        public override void PopulateMeshData() {
            throw new System.NotImplementedException();
        }
    }
}