using UnityEngine;
namespace Blocks {
    
    public class Space : Block {
        public new virtual int Sides {get { return 0; }}
        public Space() {
            
            VertexCount = 0;
            UvCount = 0;
            Indices = 0;
            Built = true;
            Transparent = true;
        }

        public override Mesh GetDrawData()
        {
            return new Mesh();
        }

        public override void PopulateMeshData()
        {
            throw new System.NotImplementedException();
        }
    }
}