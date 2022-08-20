using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MapGen;
using Assets.Scripts.WorldGen.Blocks;
using Interaction;
using UnityEngine;
using static Assets.Scripts.Helpers.MathHelpers;

namespace Assets.Scripts.WorldGen {
    public class ChunkWrapper {
        public GameObject chunkObject;
        public float lifetimeRemaining;

        public ChunkWrapper(GameObject chunkObject, float lifetimeRemaining) {
            this.chunkObject = chunkObject;
            this.lifetimeRemaining = lifetimeRemaining;
        }
    }

    public class WorldMap : MonoBehaviour {
        public GameObject ChunkPrefab;

        IMapProvider mapProvider;
        public const int ChunkSize = 8;
        private const int ChunkRefreshInterval = 1;
        private float remainingRefreshTime = ChunkRefreshInterval;

        public Dictionary<Vector3Int, Chunk> Chunks { get; set; }
        public Dictionary<Vector3Int, ChunkWrapper> LoadedChunkObjects { get; set; }

        void Awake() {
            mapProvider = InstanceFactory.GetInstance().GetMapProvider();
        }

        void Start() {
            Chunks = new Dictionary<Vector3Int, Chunk>();
            LoadedChunkObjects = new Dictionary<Vector3Int, ChunkWrapper>();
        }

        private void Update() {
            if (remainingRefreshTime <= 0) {
                List<Vector3Int> toUnload = new List<Vector3Int>();

                LoadedChunkObjects.ToList().ForEach((kvp) => {
                    kvp.Value.lifetimeRemaining -= ChunkRefreshInterval;
                    if (kvp.Value.lifetimeRemaining <= 0) {
                        toUnload.Add(kvp.Key);
                    }
                });

                toUnload.ForEach((key) => {
                    var obj = LoadedChunkObjects[key].chunkObject;

                    LoadedChunkObjects.Remove(key);
                    Destroy(obj);
                });
                remainingRefreshTime = ChunkRefreshInterval;
            }
            remainingRefreshTime -= Time.deltaTime;
        }

        public void LoadChunk(Vector3Int pos, int lifetime = 15) {
            GameObject gameObject;

            if (LoadedChunkObjects.ContainsKey(pos)) {
                ChunkWrapper wrapper = LoadedChunkObjects[pos];
                wrapper.lifetimeRemaining = lifetime;
                gameObject = wrapper.chunkObject.gameObject;
            } else {
                gameObject = Instantiate(ChunkPrefab, pos * ChunkSize, transform.rotation);
                LoadedChunkObjects.Add(pos, new ChunkWrapper(gameObject, lifetime));
            }
            ChunkBehaviour chunkBehavior = gameObject.GetComponent<ChunkBehaviour>();

            chunkBehavior.Interacted += ChunkBehavior_Interacted;

            if (Chunks.ContainsKey(pos)) { //get from recycled
                Chunk tChunk = Chunks[pos];
                chunkBehavior.Chunk = tChunk;
            } else {
                chunkBehavior.Chunk = mapProvider.GetChunk(pos, ChunkSize);
                Chunks.Add(pos, chunkBehavior.Chunk);
            }
            gameObject.name = pos.ToString();
        }

        private void ChunkBehavior_Interacted(RaycastHit hit, InteractionType interaction) {
            switch (interaction) {
                case InteractionType.Destroy:
                    Vector3 respos = hit.point - hit.normal / 2;
                    Vector3Int hitPosInside = new Vector3Int((int)Mathf.Floor(respos.x), (int)Mathf.Floor(respos.y), (int)Mathf.Floor(respos.z));
                    var chunkpos = GetChunkPos(hitPosInside);

                    SetBlock(hitPosInside, new Air());
                    GetChunkBehaviour(chunkpos).RegenerateMesh();

                    break;
            }
        }

        public BaseBlock GetBlock(Vector3Int absPos) {
            var chunk = LoadedChunkObjects[GetChunkPos(absPos)].chunkObject.GetComponent<ChunkBehaviour>().Chunk.Blocks;
            Vector3Int localPos = GetBlockLocalPos(absPos);

            try {
                //print(chunk[localPos.x, localPos.y, localPos.z].GetType().ToString());
                return chunk[localPos.x, localPos.y, localPos.z];
            } catch (IndexOutOfRangeException) {
                print(localPos.ToString());
            }
            return null;
        }
        public void SetBlock(Vector3Int absPos, BaseBlock block) {
            Vector3Int localPos = GetBlockLocalPos(absPos);
            //Debug.Log("zmieniono blok na pozycji: " + localPos.ToString());

            LoadedChunkObjects[GetChunkPos(absPos)].chunkObject.GetComponent<ChunkBehaviour>().Chunk.SetBlock(localPos, block);
        }

        private Vector3Int GetBlockLocalPos(Vector3Int absPos) {
            int x = Modulo(absPos.x, ChunkSize);
            int y = Modulo(absPos.y, ChunkSize);
            int z = Modulo(absPos.z, ChunkSize);
            return new Vector3Int(x, y, z);
        }

        public Vector3Int GetChunkPos(Vector3 absPos) {
            Vector3Int chunkPos = new Vector3Int {
                x = (int)Mathf.Floor(absPos.x / (float)ChunkSize),
                y = (int)Mathf.Floor(absPos.y / (float)ChunkSize),
                z = (int)Mathf.Floor(absPos.z / (float)ChunkSize)
            };
            return chunkPos;
        }

        public Vector3Int GetChunkPos(Vector3Int absPos) {
            Vector3Int chunkPos = new Vector3Int {
                x = (int)Mathf.Floor(absPos.x / (float)ChunkSize),
                y = (int)Mathf.Floor(absPos.y / (float)ChunkSize),
                z = (int)Mathf.Floor(absPos.z / (float)ChunkSize)
            };
            return chunkPos;
        }
        private ChunkBehaviour GetChunkBehaviour(Vector3Int chunkPos) {
            //Debug.Log("szukam: " + GetChunkPos(chunkPos).ToString());
            return LoadedChunkObjects[chunkPos].chunkObject.GetComponent<ChunkBehaviour>();
        }
    }
}