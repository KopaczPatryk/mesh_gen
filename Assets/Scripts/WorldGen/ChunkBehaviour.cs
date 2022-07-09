using System.Collections;
using System.Diagnostics;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Interaction;
using Assets.Scripts.WorldGen.Blocks;
using Assets.Scripts.WorldGen.MeshGenerators;
using UnityEngine;

public delegate void InteractionHandler(RaycastHit hit, InteractionType interaction);

namespace Assets.Scripts.WorldGen {
    public class ChunkBehaviour : MonoBehaviour, IInteractable {
        public event InteractionHandler Interacted;
        public Chunk Chunk;
        public PartialChunkMeshGenerator MeshGenerator;

        private MeshFilter meshFilter;
        private MeshCollider MeshCollider;

        public void Interact(RaycastHit hitInfo, InteractionType interaction) {
            if (Interacted != null) {
                //Debug.Log("interacted with chunk " + hitInfo.point);
                Interacted(hitInfo, interaction);
            }
        }
        void Awake() {
            MeshGenerator = new PartialChunkMeshGenerator(terrainCuller: new TransparencyCuller());
            MeshCollider = GetComponent<MeshCollider>();
            meshFilter = GetComponent<MeshFilter>();
        }
        void Start() {
            RegenerateMesh();
            //IEnumerator editor = DelBlocks();
            //StartCoroutine(editor);
        }

        public void RegenerateMesh() {
            // Stopwatch sw = new Stopwatch();
            // sw.Start();
            RawMesh rawMesh = MeshGenerator.GenerateMesh(Chunk);
            // sw.Stop();

            // UnityEngine.Debug.LogFormat(
            //    "{5} Generation took: {0}us for {1} verts and {2} triangles and {3} objects, taking avg {4}us per obj.",
            //    sw.ElapsedTicks / 10d,
            //    rawMesh.Vertices.Length,
            //    rawMesh.Triangles.Length,
            //    Chunk.Blocks.Length,
            //    (sw.ElapsedTicks / 10d) / Chunk.Blocks.Length,
            //    name
            // );
            Mesh localmesh = meshFilter.mesh;

            localmesh.Clear();
            localmesh.vertices = rawMesh.Vertices;
            localmesh.uv = rawMesh.Uv;
            localmesh.triangles = rawMesh.Triangles;
            localmesh.RecalculateNormals();
            MeshCollider.sharedMesh = localmesh;
        }

        private IEnumerator DelBlocks() {
            for (int i = 0; i < Chunk.ChunkSize; i++) {
                Chunk.SetBlock(new Vector3Int(i, 7, 7), new Air());
                RegenerateMesh();
                yield return new WaitForSeconds(1);

                Chunk.SetBlock(new Vector3Int(i, 7, 7), new Dirt());
                RegenerateMesh();
                yield return new WaitForSeconds(1);
            }
        }

        internal void test() {
            Chunk = new Chunk(8);
            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    for (int z = 0; z < 8; z++) {
                        Chunk.SetBlock(new Vector3Int(x, y, z), new Air());
                    }
                }
            }
            RegenerateMesh();
            //IEnumerator editor = DelBlocks();
            //StartCoroutine(editor);
        }
    }
}