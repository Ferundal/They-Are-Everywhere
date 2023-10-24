using System.Collections.Generic;
using UnityEngine;

namespace LevelConstructor.Editor.Level.Serialization
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
    public class SerializedLevel : ScriptableObject
    {
        public int size;
        public List<SerializedVoxel> SerializedVoxels = new List<SerializedVoxel>();

        public Level Deserialize()
        {
            var level = new Level(size);
            foreach (var serializedVoxel in SerializedVoxels)
            {
                level.Add(new Voxel(), serializedVoxel.Position);
            }
            return level;
        }

        public void Serialize(Level level)
        {
            SerializedVoxels.Clear();
            int offset = level.Size / 2;
            Vector3Int position = new Vector3Int();
            for (int x = 0; x < level.Size; x++)
            {
                for (int y = 0; y < level.Size; y++)
                {
                    for (int z = 0; z < level.Size; z++)
                    {
                        position.x = x + offset;
                        position.y = y;
                        position.z = z + offset;
                        if (level.GetVoxel(position) != null)
                        {
                            SerializedVoxels.Add(new SerializedVoxel(position));
                        }
                    }
                }
            }
        }
    }
}