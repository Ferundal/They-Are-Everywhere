using UnityEditor;
using UnityEngine;

namespace LevelConstructor
{
    public class Brush
    {
        public Voxel VoxelPrefab;
        public PlacementMarker BrushPlacementMarker;

        public Brush(string brushName)
        {
            VoxelPrefab = LoadVoxelPrefab(brushName);
//            BrushPlacementMarker = new PlacementMarker(VoxelPrefab.gameObject);
        }
        
        private Voxel LoadVoxelPrefab(string prefabName)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{PathUtility.VoxelPrefabsPath}/{prefabName}");
            return prefab.GetComponent<Voxel>();
        }
    }
}