using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelConstructor
{
    [Serializable]
    public class Shape
    {
        public string shapeName;
        public List<Voxel> voxels = new();

        public Shape(LevelGeneration.Shape shapeSO, GameObject parentGameObject)
        {
            var shapeGameObject = new GameObject(shapeSO.shapeName);
                
            shapeGameObject.transform.SetParent(parentGameObject.transform);

            foreach (var voxelSO in shapeSO.voxels)
            {
                var voxel = Voxel.Create(voxelSO, shapeGameObject);
                voxels.Add(voxel);
            }
        }
    }
}