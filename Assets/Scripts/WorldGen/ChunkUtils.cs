using UnityEngine;

public static class ChunkUtils {

    public static bool IsInChunkBounds(Vector3Int vec, int chunkSize) {
        if (vec.x < 0 || vec.y < 0 || vec.z < 0 || vec.x >= chunkSize || vec.y >= chunkSize || vec.z >= chunkSize) {
            return false;
        } else {
            return true;
        }
    }
    public static bool IsInChunkBounds(int x, int y, int z, int chunkSize) {
        if (x < 0 || y < 0 || z < 0 || x >= chunkSize || y >= chunkSize || z >= chunkSize) {
            return false;
        } else {
            return true;
        }
    }

    public static bool IsInsideOfChunk(Vector3Int vec, int chunkSize, int shellThickness = 1) {
        if (vec.x < shellThickness || vec.y < shellThickness || vec.z < shellThickness || vec.x >= chunkSize - shellThickness || vec.y >= chunkSize - shellThickness || vec.z >= chunkSize - shellThickness) {
            return false;
        } else {
            return true;
        }
    }
    public static bool IsInsideOfChunk(int x, int y, int z, int chunkSize, int shellThickness = 1) {
        if (x < shellThickness || y < shellThickness || z < shellThickness || x >= chunkSize - shellThickness || y >= chunkSize - shellThickness || z >= chunkSize - shellThickness) {
            return false;
        } else {
            return true;
        }
    }
    public static bool IsAtChunkBoundary(Vector3Int vec, int chunkSize) {
        if (vec.x % chunkSize == 0 && vec.y % chunkSize == 0 && vec.z % chunkSize == 0) {
            return true;
        } else {
            return false;
        }
    }
}