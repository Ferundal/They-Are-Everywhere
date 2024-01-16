using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelConstructor
{
    [Serializable]
    public class VoxelModel
    {
        public string voxelType;
        public Vector3Int position;
        public Shape Shape;
        public VoxelModel(Voxel voxel)
        {
            voxelType = voxel.voxelType;
            position = voxel.position;
        }

        public void AddSide(Side side)
        {
            side.Parent = this;
        }
    }
}