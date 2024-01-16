using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class LevelModel
    {
        private VoxelModel[,,] _voxels;
        private Vector3Int _minPosition;
        private Vector3Int _maxPosition;
        private List<VoxelModel> _voxelsList = new ();
        private List<Shape> _shapes = new();

        public LevelModel(List<Voxel> voxels)
        {
            FindBoundingBox(voxels);
            var arraySize = _maxPosition - _minPosition;
            _voxels = new VoxelModel[arraySize.x, arraySize.y, arraySize.z];
            foreach (var voxel in voxels)
            {
                Add(voxel);
            }
        }

        private void Add(Voxel voxel)
        {
            Vector3Int arrayIndex = voxel.position + _minPosition;
            var modelVoxel = new VoxelModel(voxel);
            _voxels[arrayIndex.x, arrayIndex.y, arrayIndex.z] = modelVoxel;
            _voxelsList.Add(modelVoxel);
        }
        
        public VoxelModel GetVoxel(Vector3Int position)
        {
            Vector3Int arrayIndex = position + _minPosition;
            if (Vector3Int.Min(position, _minPosition) != _minPosition ||
                Vector3Int.Max(position, _maxPosition) != _maxPosition)
            {
                return null;
            }

            return _voxels[arrayIndex.x, arrayIndex.y, arrayIndex.z];
        }

        
        
        public void CreateMesh()
        {
            foreach (var voxel in _voxelsList)
            {
                SetShapesAndSides(voxel);
            }

            foreach (var shape in _shapes)
            {
                shape.CreateSurfaces();
            }
        }

        private void SetShapesAndSides(VoxelModel voxelModel)
        {
            VoxelDirection[] directions = (VoxelDirection[])Enum.GetValues(typeof(VoxelDirection));
            foreach (var direction in directions)
            {
                var neighborVoxel = GetVoxel(VoxelPositionUtility.NextInDirection(voxelModel.position, direction));
                if (neighborVoxel != null
                    && voxelModel.Shape != null
                    && neighborVoxel.voxelType == voxelModel.voxelType)
                {
                    neighborVoxel.Shape.Add(voxelModel);
                }
                else
                {
                    var side = new Side(direction);
                    voxelModel.AddSide(side);
                }
            }

            if (voxelModel.Shape == null)
            {
                Shape newShape = new(this);
                newShape.Add(voxelModel);
                _shapes.Add(newShape);
            }
        }
        
        private void FindBoundingBox(List<Voxel> voxels)
        {
            if (voxels.Count < 1) return;
            
            _minPosition = voxels[0].position;
            _maxPosition = voxels[0].position;
            for (int index = 1; index < voxels.Count; ++index)
            {
                _minPosition = Vector3Int.Min(_minPosition, voxels[index].position);
                _maxPosition = Vector3Int.Min(_maxPosition, voxels[index].position);
            }
        }


    }
}