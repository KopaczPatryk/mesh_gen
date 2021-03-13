using UnityEngine;

namespace MeshGen.WorldGen {
    public abstract class BaseBlock : System.Object {
        protected RawMesh mesh = new RawMesh();
        protected const float scale = 1;

        [SerializeField]
        public int VertexCount { get; protected set; }

        [SerializeField]
        public int UvCount { get; protected set; }

        [SerializeField]
        public int Indices { get; protected set; }
        protected bool Built = false;

        public bool Transparent { get; private set; }
        //public abstract void PopulateMeshData();
        public abstract RawMesh GetDrawData();

        public int Sides { get; private set; } = 0;


        protected BaseBlock(
                            bool built,
                            bool transparent,
                            int uvCount = 24,
                            int sides = 6,
                            int vertexCount = 24,
                            int indices = 36
                            ) {

            VertexCount = vertexCount;
            UvCount = uvCount;
            Indices = indices;
            Built = built;
            Transparent = transparent;
            Sides = sides;
        }
    }
}