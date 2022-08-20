using UnityEngine;

namespace TreeGen {
    public class TreeMeshGenerator {
        public RawListMesh GenerateTree(Part tree) {
            RawListMesh mesh = new RawListMesh();

            Generate(tree, mesh);

            // mesh.Vertices.ForEach((vert) => {
            //     var vec = new Vector2(vert.x, vert.z);
            //     mesh.Uv.Add(vec);
            //     Debug.Log(vec);
            // });

            return mesh;
        }

        private void Generate(Part treeSegment, RawListMesh mesh, Vector3 pos = new Vector3()) {
            mesh.AppendSegment(treeSegment, pos);
            pos += treeSegment.direction;
            foreach (var segment in treeSegment) {
                Generate(segment, mesh, pos);
            }
        }
    }
}