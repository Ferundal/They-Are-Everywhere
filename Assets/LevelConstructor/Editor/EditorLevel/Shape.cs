using System;
using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;

namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class Shape : MonoBehaviour
    {
        [HideInInspector] public LevelGeneration.Shape shapeSO;
        private LevelConstructor _levelConstructor;

        public static Shape Create(LevelGeneration.Shape shapeSO, GameObject parentGameObject, LevelConstructor levelConstructor)
        {
            var shapeGameObject = new GameObject(shapeSO.shapeName);
            var shape = (Shape)shapeGameObject.AddComponent(typeof(Shape));
            shape.shapeSO = shapeSO;
            shape._levelConstructor = levelConstructor;
                
            shapeGameObject.transform.SetParent(parentGameObject.transform);

            foreach (var voxelSO in shapeSO.voxels)
            {
                var voxel = Voxel.Create(voxelSO, shapeGameObject, levelConstructor);
            }

            return shape;
        }

        public Voxel AddVoxel(VoxelType voxelType, Vector3Int position)
        {
            var voxelSO = new LevelGeneration.Voxel
            {
                VoxelType = voxelType,
                voxelTypeName = voxelType.name,
                position = position,
                ParentShape = shapeSO
            };
            
            shapeSO.voxels.Add(voxelSO);
            shapeSO.ParentLevel.VoxelMatrix[voxelSO.position] = voxelSO;
            AddSides(voxelSO);
            var voxel = Voxel.Create(voxelSO, gameObject, _levelConstructor);
            return voxel;
        }

        private void AddSides(LevelGeneration.Voxel voxelSO)
        {
            // TODO: [#1] Should be done in one array
            foreach (var sideDirection in LevelGeneration.Voxel.SideDirections)
            {
                var neighbourVoxelPosition = voxelSO.position + sideDirection;
                
                var neighbourVoxel = shapeSO.ParentLevel.VoxelMatrix[neighbourVoxelPosition];
                if (neighbourVoxel != null)
                {
                    continue;
                }

                var sideSO = new LevelGeneration.Side
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