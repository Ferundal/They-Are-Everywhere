namespace LevelGeneration
{
    public interface IVoxelMeshGenerator
    {
        public MeshInfo GenerateSideMeshInfo(Side side);

        public void GenerateMesh(Voxel voxel);
    }
}