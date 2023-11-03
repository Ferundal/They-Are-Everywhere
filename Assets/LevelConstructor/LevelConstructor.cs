using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class LevelConstructor : MonoBehaviour
    {
        [SerializeField] private SerializedLevel serializedLevel;

        [SerializeField] public Level Level;
        public LevelConstructorRaycaster Raycaster;
        [HideInInspector] public List<Voxel> voxelPrefabs;
        [HideInInspector][SerializeField] private List<Voxel> voxels;

        private void OnEnable()
        {
            LoadVoxelPrefabs();
            Raycaster = new LevelConstructorRaycaster(this);
        }

        public Vector3 VoxelPositionToWorldPosition(Vector3Int voxelPosition)
        {
            
            return new Vector3(voxelPosition.x, voxelPosition.y, voxelPosition.z);
        }

        public void AddVoxel(Voxel voxelPrefab, Vector3Int voxelPosition)
        {
            var worldVoxelPosition = VoxelPositionToWorldPosition(voxelPosition);
            var voxelInstance = Instantiate(voxelPrefab, worldVoxelPosition, voxelPrefab.transform.rotation, transform);
            voxelInstance.position = voxelPosition;
            voxelInstance.levelConstructor = this;
            voxels.Add(voxelInstance);
        }

        private void LoadVoxelPrefabs()
        {
            voxelPrefabs = new List<Voxel>();
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{PathUtility.VoxelPrefabsPath}/CubeTile.prefab");
            voxelPrefabs.Add(prefab.GetComponent<Voxel>());
        }
    }
}
