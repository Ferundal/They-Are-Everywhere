using System;
using UnityEditor;
using UnityEngine;


namespace LevelGeneration
{
    [CreateAssetMenu(fileName = "NewVoxelType", menuName = "Voxel Type/New Voxel Type")]
    public class VoxelType : ScriptableObject
    {
        //TODO can be replaced with Zenject?
        [SerializeField] private MonoScript meshGeneratorScript;
        public IVoxelMeshGenerator MeshGenerator;

        private void OnEnable()
        {
            CreateMeshGenerator();
        }

        private void CreateMeshGenerator()
        {
            if (meshGeneratorScript == null)
            {
                Debug.LogWarning($"Mesh Generator Script is Missing");
            }
            
            meshGeneratorScript.GetClass();
            object instance = Activator.CreateInstance(meshGeneratorScript.GetClass());
            
            try
            {
                MeshGenerator = (IVoxelMeshGenerator)instance;
            }
            catch (InvalidCastException)
            {
                Debug.LogWarning("The Mesh Generator Script does not implement the IVoxelMeshGenerator interface.");
            }
        }
    }
}