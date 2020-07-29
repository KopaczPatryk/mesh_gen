using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreeGrid : MonoBehaviour {
    public GameObject prefab;
    public int GridXSize = 5;
    public int GridYSize = 5;
    public int GridSpacing = 5;
    void Start() {
        if (prefab != null) {
            for (int x = 0; x < GridXSize; x++) {
                for (int y = 0; y < GridYSize; y++) {
                    var pos = new Vector3(x * GridSpacing, 0, y * GridSpacing);
                    Instantiate(prefab, pos, Quaternion.identity);
                }
            }
        }
    }
}