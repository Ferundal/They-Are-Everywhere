using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Screen = UnityEngine.Device.Screen;

namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class LevelConstructor : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] private Level level;

        [SerializeField] public LevelModel LevelModel;
        [HideInInspector] public List<Voxel> voxelPrefabs;
        [HideInInspector] public LevelConstructorRaycaster Raycaster { get; private set; }
        [HideInInspector][SerializeField] private List<Voxel> voxels;

        public EventHandler Handler { get; } = new();

        private void OnEnable()
        {
            LoadVoxelPrefabs();
            Raycaster = new LevelConstructorRaycaster(this);
            OptimizeVoxelsList();
        }

        public Vector3 VoxelPositionToWorldPosition(Vector3Int voxelPosition, Voxel voxel)
        {

            var worldPosition = new Vector3(
                voxelPosition.x * voxel.size.x - voxel.size.x / 2,
                voxelPosition.y * voxel.size.y + voxel.size.y / 2,
                voxelPosition.z * voxel.size.z - voxel.size.z / 2);
            worldPosition += transform.position;
            return worldPosition;
        }

        public void AddVoxel(Voxel voxelPrefab, Vector3Int voxelPosition)
        {
            var worldVoxelPosition = VoxelPositionToWorldPosition(voxelPosition, voxelPrefab);
            var voxelInstance = Instantiate(voxelPrefab, worldVoxelPosition, voxelPrefab.transform.rotation, transform);
            voxelInstance.position = voxelPosition;
            voxelInstance.levelConstructor = this;
            voxels.Add(voxelInstance);
        }

        private void LoadVoxelPrefabs()
        {
            voxelPrefabs = new List<Voxel>();
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{PathUtility.VoxelPrefabsPath}/GrayBlock.prefab");
            voxelPrefabs.Add(prefab.GetComponent<Voxel>());
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{PathUtility.VoxelPrefabsPath}/RedBlock.prefab");
            voxelPrefabs.Add(prefab.GetComponent<Voxel>());
        }

        private void OptimizeVoxelsList()
        {
            voxels.RemoveAll(voxel => voxel == null);
        }

        public void OnBeforeSerialize()
        {
            //Debug.Log("OnBeforeSerialize");
        }

        public void OnAfterDeserialize()
        {
            Handler.HasUnprocessedDeserialization = true;
        }
    }
}
