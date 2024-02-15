namespace LevelGeneration
{
    public interface IVoxelMeshGenerator
    {
        public MeshInfo GenerateSideMeshInfo(Side side, Voxel voxel);

        public void GenerateMesh(Voxel voxel);
    }
}