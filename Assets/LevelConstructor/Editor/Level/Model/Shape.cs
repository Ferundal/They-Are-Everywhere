using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelConstructor
{
    public class Shape
    {
        private LevelModel _levelModel;
        
        private HashSet<VoxelModel> _voxels = new();
        private Vector3Int _minPosition;
        private Vector3Int _maxPosition;

        public Shape(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }
        
        public void Add(VoxelModel voxelModel)
        {
            _voxels.Add(voxelModel);
            voxelModel.Shape = this;
            _minPosition = Vector3Int.Min(_minPosition, voxelModel.position);
            _maxPosition = Vector3Int.Max(_maxPosition, voxelModel.position);
        }

        
        
        public void CreateSurfaces()
        {

        }
    }
}