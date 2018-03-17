using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshExtensions {
    public static void SetTriangle(this int[] triangleArray, int a, int b, int c, ref int counter) {
        triangleArray[counter] = a;
        triangleArray[counter + 1] = b;
        triangleArray[counter + 2] = c;
        counter += 3;
    }
}
