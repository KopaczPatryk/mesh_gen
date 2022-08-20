using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreeGen {
    public class RawListMesh {
        public List<int> Triangles = new List<int>();
        public int TriCount = 0;

        public List<Vector3> Vertices = new List<Vector3>();
        public int VertCount = 0;

        public List<Vector2> Uv = new List<Vector2>();
    }
}
