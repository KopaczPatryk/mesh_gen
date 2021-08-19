using UnityEngine;

namespace Assets.Scripts.WorldGen.Blocks {
    public abstract class BaseBlock : object {
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
                            bool transparent = false,
                            bool built = false,
                            int vertexCount = 24,
                            int indices = 36,
                            int uvCount = 24,
                            int sides = 6
                            ) {
            Transparent = transparent;
            VertexCount = vertexCount;
            Indices = indices;
            UvCount = uvCount;
            Sides = sides;
        }
    }
}
