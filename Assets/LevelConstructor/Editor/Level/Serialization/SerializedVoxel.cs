using System;
using UnityEngine;

namespace LevelConstructor.Editor.Level.Serialization
{
    [Serializable]
    public class SerializedVoxel
    {
        public Vector3Int Position;

        public SerializedVoxel(Vector3Int position)
        {
            Position = new Vector3Int(position.x, position.y, position.z);
        }
    }
}