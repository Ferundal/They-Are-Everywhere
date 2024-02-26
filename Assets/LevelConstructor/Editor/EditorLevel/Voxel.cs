using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class Voxel : MonoBehaviour
    {
        [NonSerialized] public LevelGeneration.Voxel VoxelSO;
        public List<Side> sides = new();

        private LevelConstructor _levelConstructor;

        public Voxel() { }

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

        public static Voxel Create(LevelGeneration.Voxel voxelSO, GameObject parentGameObject, LevelConstructor levelConstructor)
        {
            var voxelGameObject = new GameObject($"Voxel (position = {voxelSO.position.ToString()})");
            
            voxelGameObject.transform.SetParent(parentGameObject.transform);
            var voxel = (Voxel)voxelGameObject.AddComponent(typeof(Voxel));
            voxel.VoxelSO = voxelSO;
            voxel._levelConstructor = levelConstructor;
            levelConstructor.EditorLevel.VoxelMatrix[voxel.VoxelSO.position] = voxel;

            voxelGameObject.transform.localPosition = voxel.Position;

            foreach (var sideSO in voxelSO.sides)
            {
                var side = Side.Create(sideSO, voxelGameObject, levelConstructor);
                
                voxel.sides.Add(side);
            }

            return voxel;
        }
        
        private void OnDisable()
        {
            if (_levelConstructor.IsReload) return;
            
            DeleteVoxel();
        }

        private void DeleteVoxel()
        {
            var levelSO = VoxelSO.ParentShape.ParentLevel;
            levelSO.VoxelMatrix[VoxelSO.position] = null;
            VoxelSO.ParentShape.voxels.Remove(VoxelSO);
        }
    }
}