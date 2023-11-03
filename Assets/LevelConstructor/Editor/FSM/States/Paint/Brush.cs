namespace LevelConstructor
{
    public class Brush
    {
        public Voxel VoxelPrefab;
        public PlacementMarker BrushPlacementMarker;

        public Brush(Voxel voxel)
        {
            VoxelPrefab = voxel;
            BrushPlacementMarker = new PlacementMarker(VoxelPrefab.gameObject);
        }
    }
}