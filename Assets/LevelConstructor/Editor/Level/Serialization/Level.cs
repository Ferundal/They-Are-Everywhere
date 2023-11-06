using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelConstructor
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
    public class Level : ScriptableObject
    {
        public int size;
        public List<SerializedVoxel> voxels = new List<SerializedVoxel>();

        public LevelModel Deserialize(LevelConstructor levelConstructor)
        {
            Debug.Log("Deserialize");
            var level = new LevelModel(size);
            foreach (var voxel in voxels)
            {
                var voxelPrefab = levelConstructor.voxelPrefabs.FirstOrDefault(currentVoxel => currentVoxel.voxelType == voxel.voxelType);
            }
            return level;
        }

        public void Serialize(LevelModel levelModel)
        {
            Debug.Log(levelModel.Size);
            size = levelModel.Size;
            voxels.Clear();
            var position = Vector3Int.zero;
            for (int x = 0; x < levelModel.Size; x++)
            {
                for (int y = 0; y < levelModel.Size; y++)
                {
                    for (int z = 0; z < levelModel.Size; z++)
                    {
                        position.x = x;
                        position.y = y;
                        position.z = z;
                        var voxel = levelModel.GetVoxel(position);
                        if (voxel != null)
                        {
                            voxels.Add(new SerializedVoxel(voxel));
                        }
                    }
                }
            }
        }
    }
}