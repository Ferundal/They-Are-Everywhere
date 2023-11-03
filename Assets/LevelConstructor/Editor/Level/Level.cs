using System;
using UnityEngine;

namespace LevelConstructor
{
    public class Level
    {
        private Voxel[,,] _voxels;
        
        public int Size => _voxels.GetLength(0);

        public delegate void OnChangedHandler();
        public event OnChangedHandler OnChanged;
        
        public Level(int size)
        {
            _voxels = new Voxel[size, size, size];
        }

        public void Add(Voxel voxel)
        {
            var voxelPosition = voxel.position;
            _voxels[voxelPosition.x, voxelPosition.y, voxelPosition.z] = voxel;
            OnChanged?.Invoke();
        }

        public Voxel GetVoxel(Vector3Int position)
        {
            return _voxels[position.x, position.y, position.z];
        }
    }
}