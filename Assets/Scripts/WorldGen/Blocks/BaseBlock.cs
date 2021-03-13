using UnityEngine;

namespace MeshGen.WorldGen {
    public abstract class BaseBlock : System.Object {
        // protected RawMesh mesh = new RawMesh();
        private const float scale = 1;

        [SerializeField]
        public int VertexCount { get; private set; }

        [SerializeField]
        public int UvCount { get; private set; }

        [SerializeField]
        public int Indices { get; private set; }
        public bool CanBeRendered => Sides > 0;
        public bool Transparent { get; private set; }
        public abstract RawMesh GetDrawData();
        public int Sides { get; private set; } = 0;

        protected BaseBlock(
                            bool transparent,
                            int sides,
                            int uvCount,
                            int vertexCount,
                            int indices
                            ) {

            Transparent = transparent;
            Sides = sides;
            UvCount = uvCount;
            VertexCount = vertexCount;
            Indices = indices;
        }
    }
}