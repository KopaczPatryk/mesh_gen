using System;
using System.Linq;
using System.Threading;
using Assets.Scripts.WorldGen;
using UnityEngine;

public class ChunkLoader : MonoBehaviour {
    WorldMap world;
    public int RefreshTime { get; set; } = 1;
    private float currentRefreshTime;

    private void Start() {
        world = GameObject.FindObjectOfType<WorldMap>();
        currentRefreshTime = RefreshTime;
    }
    private void Update() {
        if (currentRefreshTime <= 0) {
            refreshChunks();
            currentRefreshTime = RefreshTime;
        }
        currentRefreshTime -= Time.deltaTime;
    }
    private void refreshChunks() {
        Vector3Int localPosition = world.GetChunkPos(gameObject.transform.position);

        int lx = localPosition.x;
        int ly = localPosition.y;
        int lz = localPosition.z;

        int range = 3;

        for (int x = lx - range; x < lx + range; x++) {
            for (int y = ly - range; y < ly + range; y++) {
                for (int z = lz - range; z < lz + range; z++) {
                    world.LoadChunk(new Vector3Int(x, y, z));
                }
            }
        }
    }
}