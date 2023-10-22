using System;
using UnityEngine;

namespace LevelConstructor.Editor.Level
{
    public class Level
    {
        private Voxel[,,] _voxels;
        private int _offset;
        
        public delegate void OnChangedHandler();
        public event OnChangedHandler OnChanged;

        public int Size
        { 
            get => _voxels.Length;
            set
            {
                var newSize = value;
                var voxels = new Voxel[newSize, newSize, newSize];
                var resizeOffset = (newSize - _voxels.Length) / 2;
                int x = 0;
                int y = 0;
                int z = 0;
                int horizontalMaxIterator = newSize;
                int verticalMaxIterator = newSize;

                if (resizeOffset < 0)
                {
                    x = -resizeOffset;
                    z = -resizeOffset;
                    horizontalMaxIterator = _voxels.Length - resizeOffset;
                    verticalMaxIterator = _voxels.Length;
                }
                
                for (; x < horizontalMaxIterator; x++)
                {
                    for (; y < verticalMaxIterator; y++)
                    {
                        for (; z < horizontalMaxIterator; z++)
                        {
                            voxels[x, y, x] = _voxels[x + resizeOffset, y, z + resizeOffset];
                        }
                    }
                }

                _voxels = voxels;
                _offset = newSize / 2;
                OnChanged?.Invoke();
            }
        }
        public Level(int size)
        {
            _voxels = new Voxel[size, size, size];
            _offset = size / 2;
        }

        public void Add(Voxel voxel, Vector3Int position)
        {
            var x = position.x + _offset;
            var z = position.z + _offset;
            _voxels[x, position.y, z] = voxel;
            OnChanged?.Invoke();
        }

        public Voxel GetVoxel(Vector3Int position)
        {
            return _voxels[position.x, position.y, position.z];
        }
    }
}