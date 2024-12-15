using TerrainGeneration;
using UnityEngine;

namespace Editor.Scripts.EditorLevel
{
    //TODO replace static with Zenject?
    
    public class CenteredVoxel
    {
        private static TerrainGeneration.ModelLevel.Voxel _centeredVoxel;
        
        static CenteredVoxel()
        {
            _centeredVoxel = new TerrainGeneration.ModelLevel.Voxel();
            var shapeSO = new TerrainGeneration.ModelLevel.Shape();
            shapeSO.voxels.Add(_centeredVoxel);
            
            var levelSO = new TerrainGeneration.ModelLevel.Level();
            levelSO.shapes.Add(shapeSO);
            levelSO.zeroVoxelWorldOffset = Vector3.zero;

            foreach (var direction in TerrainGeneration.ModelLevel.Voxel.SideDirections)
            {
                var side = new TerrainGeneration.ModelLevel.Side();
                side.sideDirection = direction;
                _centeredVoxel.sides.Add(side);
            }
            levelSO.Initialize();
        }
        
        public static TerrainGeneration.ModelLevel.Side Side(Vector3Int sideDirection, VoxelType voxelType, float voxelSize)
        {
            _centeredVoxel.VoxelType = voxelType;
            _centeredVoxel.ParentShape.ParentLevel.voxelSize = voxelSize;
            
            foreach (var side in _centeredVoxel.sides)
            {
                if (side.sideDirection == sideDirection)
                {
                    return side;
                }
            }

            return null;
        }

        public static TerrainGeneration.ModelLevel.Shape Shape(VoxelType voxelType, float voxelSize)
        {
            _centeredVoxel.VoxelType = voxelType;
            _centeredVoxel.ParentShape.ParentLevel.voxelSize = voxelSize;
            return _centeredVoxel.ParentShape;
        }
    }
}