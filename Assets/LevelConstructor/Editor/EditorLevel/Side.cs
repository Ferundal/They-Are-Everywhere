using System;
using UnityEngine;

namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class Side : MonoBehaviour
    {
        private static LevelGeneration.Voxel _centeredVoxel;
        private LevelGeneration.Side _sideSO;
        private LevelConstructor _levelConstructor;
        
        public LevelGeneration.Side SideSO => _sideSO;
        

        public static Side Create(LevelGeneration.Side sideSO, GameObject parentGameObject, LevelConstructor levelConstructor)
        {
            var sideGameObject = new GameObject($"Side (direction = {sideSO.sideDirection.ToString()})");
            sideGameObject.transform.SetParent(parentGameObject.transform);
            
            var side = (Side)sideGameObject.AddComponent(typeof(Side));
            side._sideSO = sideSO;
            side._levelConstructor = levelConstructor;

            var mesh = CenteredVoxel.Side(
                side.SideSO.sideDirection,
                side.SideSO.ParentVoxel.VoxelType,
                side.SideSO.ParentVoxel.ParentShape.ParentLevel.voxelSize).MeshInfo.Mesh;
            MeshUtility.AddMesh(sideGameObject, mesh);
            
            MeshUtility.AddMaterial(sideGameObject, side._sideSO.ParentVoxel.VoxelType.Material);
            
            MeshUtility.AddCollider(sideGameObject, mesh);

            sideGameObject.transform.localPosition = Vector3.zero;

            return side;
        }

        public Vector3 OffsetToVoxel
        {
            get
            {
                var doubleSizeSideOffset = new Vector3(_sideSO.sideDirection.x, _sideSO.sideDirection.y, _sideSO.sideDirection.z);
                return doubleSizeSideOffset * (_sideSO.ParentVoxel.ParentShape.ParentLevel.voxelSize / 2.0f);
            }
        }

        private void OnDisable()
        {
            if (_levelConstructor.IsReload) return;

            _sideSO.ParentVoxel.sides.Remove(_sideSO);
        }

    }
}