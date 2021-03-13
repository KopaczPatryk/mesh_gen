namespace Assets.Scripts.WorldGen.MeshGenerators {
    public interface IChunkMeshGenerator {
        RawMesh GenerateMesh(Chunk chunkModel);
    }
}