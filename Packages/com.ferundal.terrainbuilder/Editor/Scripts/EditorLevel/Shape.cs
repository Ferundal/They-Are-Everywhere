using TerrainGeneration;
using TerrainGeneration.ModelLevel;
using UnityEngine;

namespace Editor.Scripts.EditorLevel
{
    [ExecuteInEditMode]
    public class Shape : MonoBehaviour
    {
        [HideInInspector] public TerrainGeneration.ModelLevel.Shape shapeSO;
        private LevelConstructor _levelConstructor;

        public static Shape Create(TerrainGeneration.ModelLevel.Shape shapeSO, LevelConstructor levelConstructor, ThreeDimensionalMatrix<Voxel> voxelMatrix)
        {
            var shapeGameObject = new GameObject(shapeSO.shapeName);
            var shape = (Shape)shapeGameObject.AddComponent(typeof(Shape));
            shape.shapeSO = shapeSO;
            shape._levelConstructor = levelConstructor;
                
            shapeGameObject.transform.SetParent(levelConstructor.gameObject.transform);

            foreach (var voxelSO in shapeSO.voxels)
            {
                var voxel = Voxel.Create(voxelSO, shapeGameObject, levelConstructor, voxelMatrix);
            }

            return shape;
        }

        public Voxel AddVoxel(VoxelType voxelType, Vector3Int position)
        {
            var voxelSO = new TerrainGeneration.ModelLevel.Voxel
            {
                VoxelType = voxelType,
                voxelTypeName = voxelType.name,
                position = position,
                ParentShape = shapeSO
            };
            
            shapeSO.voxels.Add(voxelSO);
            shapeSO.ParentLevel.VoxelMatrix[voxelSO.position] = voxelSO;
            AddSides(voxelSO);
            var voxel = Voxel.Create(voxelSO, gameObject, _levelConstructor, _levelConstructor.EditorLevel.VoxelMatrix);
            return voxel;
        }

        private void AddSides(TerrainGeneration.ModelLevel.Voxel voxelSO)
        {
            // TODO: [#1] Should be done in one array
            foreach (var sideDirection in TerrainGeneration.ModelLevel.Voxel.SideDirections)
            {
                var neighbourVoxelPosition = voxelSO.position + sideDirection;
                
                var neighbourVoxel = shapeSO.ParentLevel.VoxelMatrix[neighbourVoxelPosition];
                if (neighbourVoxel != null)
                {
                    continue;
                }

                var sideSO = new TerrainGeneration.ModelLevel.Side
                {
                    ParentVoxel = voxelSO,
                    sideDirection = sideDirection
                };

                voxelSO.sides.Add(sideSO);
            }
            voxelSO.ParentShape.ParentLevel.AddPointsToSides(voxelSO.sides);
        }
    }
}