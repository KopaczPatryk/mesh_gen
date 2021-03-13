namespace MeshGen {
    public interface IChunkMeshGenerator {
        RawMesh GenerateMesh(Chunk chunkModel);
    }
}