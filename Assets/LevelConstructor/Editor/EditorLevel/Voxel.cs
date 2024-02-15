using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelConstructor
{
    [Serializable]
    public class Voxel : MonoBehaviour
    {
        [NonSerialized] public LevelGeneration.Voxel VoxelSO;
        public List<Side> sides = new();

        private Voxel() { }
        
        public Vector3 Position
        {
            get
            {
                var floatPosition = new Vector3(VoxelSO.position.x, VoxelSO.position.y, VoxelSO.position.z);
                var aboveGroundPosition = floatPosition + new Vector3(0.5f, 0.5f, 0.5f);
                var scaledPosition = aboveGroundPosition * VoxelSO.ParentShape.ParentLevel.voxelSize;
                return scaledPosition;
            }
        }

        public static Voxel Create(LevelGeneration.Voxel voxelSO, GameObject parentGameObject)
        {
            var voxelGameObject = new GameObject($"Voxel (position = {voxelSO.position.ToString()})");
            
            voxelGameObject.transform.SetParent(parentGameObject.transform);
            var voxel = (Voxel)voxelGameObject.AddComponent(typeof(Voxel));
            voxel.VoxelSO = voxelSO;

            voxelGameObject.transform.localPosition = voxel.Position;

            foreach (var sideSO in voxelSO.sides)
            {
                var side = Side.Create(sideSO, voxelGameObject);
                
                voxel.sides.Add(side);
            }

            return voxel;
        }
    }
}