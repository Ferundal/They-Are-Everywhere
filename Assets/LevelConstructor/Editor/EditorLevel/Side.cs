using UnityEngine;

namespace LevelConstructor
{
    public class Side : MonoBehaviour
    {
        private static LevelGeneration.Voxel _centeredVoxel;
        private LevelGeneration.Side _sideSO;
        
        public LevelGeneration.Side SideSO => _sideSO;

        public static Side Create(LevelGeneration.Side sideSO, GameObject parentGameObject)
        {
            var sideGameObject = new GameObject($"Side (direction = {sideSO.sideDirection.ToString()})");
            sideGameObject.transform.SetParent(parentGameObject.transform);
            
            var side = (Side)sideGameObject.AddComponent(typeof(Side));
            side._sideSO = sideSO;

            var mesh = CenteredVoxel.Side(
                side.SideSO.sideDirection,
                side.SideSO.ParentVoxel.VoxelType,
                side.SideSO.ParentVoxel.ParentShape.ParentLevel.voxelSize).MeshInfo.Mesh;
            AddMesh(sideGameObject, mesh);
            
            AddMaterial(sideGameObject, side._sideSO.ParentVoxel.VoxelType.Material);
            
            AddCollider(sideGameObject, mesh);

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

        private static void AddMesh(GameObject gameObject, Mesh mesh)
        {
            var meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
        }
        
        private static void AddMaterial(GameObject gameObject, Material material)
        {
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
        }
        
        private static void AddCollider(GameObject gameObject, Mesh mesh)
        {
            var meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }
        
    }
}