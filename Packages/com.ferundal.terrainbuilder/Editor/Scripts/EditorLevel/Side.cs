using UnityEngine;
using Utility;

namespace Editor.Scripts.EditorLevel
{
    [ExecuteInEditMode]
    public class Side : MonoBehaviour
    {
        private static TerrainGeneration.ModelLevel.Voxel _centeredVoxel;
        private TerrainGeneration.ModelLevel.Side _sideSO;
        private LevelConstructor _levelConstructor;
        
        public TerrainGeneration.ModelLevel.Side SideSO => _sideSO;
        

        public static Side Create(TerrainGeneration.ModelLevel.Side sideSO, GameObject parentGameObject, LevelConstructor levelConstructor)
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

        public void Destroy()
        {
            
            _levelConstructor.EditorLevel.VoxelMatrix[_sideSO.ParentVoxel.position].RemoveSide(this);
            DestroyImmediate(gameObject);
        }
    }
}