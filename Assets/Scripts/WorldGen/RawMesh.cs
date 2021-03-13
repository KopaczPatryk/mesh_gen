using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMesh {
    public Vector3[] Vertices;
    public Vector2[] Uv;
    public int[] Triangles;

    public int vertexCount { get; set; } = 0;
    public int uvCount { get; set; } = 0;
    public int trianglesCount { get; set; } = 0;
}
