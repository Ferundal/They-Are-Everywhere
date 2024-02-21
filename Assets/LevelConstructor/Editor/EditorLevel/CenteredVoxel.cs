using LevelGeneration;
using UnityEngine;

namespace LevelConstructor
{
    //TODO replace static with Zenject?
    
    public class CenteredVoxel
    {
        private static LevelGeneration.Voxel _centeredVoxel;
        
        static CenteredVoxel()
        {
            _centeredVoxel = new LevelGeneration.Voxel();
            var shapeSO = new LevelGeneration.Shape();
            shapeSO.voxels.Add(_centeredVoxel);
            
            var levelSO = new LevelGeneration.Level();
            levelSO.shapes.Add(shapeSO);
            levelSO.zeroVoxelWorldOffset = Vector3.zero;

            foreach (var direction in LevelGeneration.Voxel.SideDirections)
            {
                var side = new LevelGeneration.Side();
                side.sideDirection = direction;
                _centeredVoxel.sides.Add(side);
            }
            levelSO.Initialize();
        }
        
        public static LevelGeneration.Side Side(Vector3Int sideDirection, VoxelType voxelType, float voxelSize)
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

        public static LevelGeneration.Shape Shape(VoxelType voxelType, float voxelSize)
        {
            _centeredVoxel.VoxelType = voxelType;
            _centeredVoxel.ParentShape.ParentLevel.voxelSize = voxelSize;
            return _centeredVoxel.ParentShape;
        }
    }
}