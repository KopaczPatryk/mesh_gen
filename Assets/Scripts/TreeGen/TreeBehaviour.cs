using Assets.Scripts.TreeGen.Parts;
using System;
using UnityEngine;

namespace Assets.Scripts.TreeGen {
    public class TreeBehaviour : MonoBehaviour {
        public Part body { get; private set; }
        private InstanceFactory instanceFactory = InstanceFactory.GetInstance();
        private System.Random rnd = InstanceFactory.GetInstance().GetRandom();

        void Start() {
            body = instanceFactory.GetTreeGenerator().GenerateTree();

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            Mesh localmesh = meshFilter.mesh;

            var treeGen = new TreeMeshGenenerator();
            var rawMesh = treeGen.GenerateTree(body);

            localmesh.Clear();
            localmesh.vertices = rawMesh.Vertices.ToArray();
            localmesh.uv = rawMesh.Uv.ToArray();
            localmesh.triangles = rawMesh.Triangles.ToArray();
            localmesh.RecalculateNormals();
        }
    }
}
