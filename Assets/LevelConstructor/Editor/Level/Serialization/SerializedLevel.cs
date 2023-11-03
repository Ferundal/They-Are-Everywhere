using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelConstructor
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
    public class SerializedLevel : ScriptableObject
    {
        public int size;
        public List<SerializedVoxel> voxels = new List<SerializedVoxel>();

        public Level Deserialize(LevelConstructor levelConstructor)
        {
            Debug.Log("Deserialize");
            var level = new Level(size);
            foreach (var voxel in voxels)
            {
                var voxelPrefab = levelConstructor.voxelPrefabs.FirstOrDefault(currentVoxel => currentVoxel.voxelType == voxel.voxelType);
            }
            return level;
        }

        public void Serialize(Level level)
        {
            Debug.Log(level.Size);
            size = level.Size;
            voxels.Clear();
            var position = Vector3Int.zero;
            for (int x = 0; x < level.Size; x++)
            {
                for (int y = 0; y < level.Size; y++)
                {
                    for (int z = 0; z < level.Size; z++)
                    {
                        position.x = x;
                        position.y = y;
                        position.z = z;
                        var voxel = level.GetVoxel(position);
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