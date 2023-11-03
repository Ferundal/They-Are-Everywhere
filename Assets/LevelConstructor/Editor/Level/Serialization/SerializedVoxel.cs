using System;
using UnityEngine;

namespace LevelConstructor
{
    [Serializable]
    public class SerializedVoxel
    {
        public string voxelType;
        public Vector3Int position;

        public SerializedVoxel(Voxel voxel)
        {
            voxelType = voxel.voxelType;
            position = new Vector3Int(voxel.position.x, voxel.position.y, voxel.position.z);
        }
    }
}