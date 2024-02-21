using LevelGeneration;
using UnityEditor;
using UnityEngine;

namespace LevelConstructor
{
    public class Brush
    {
        public bool IsActive;

        private Mesh _mesh;

        private VoxelType _voxelType;
        private LevelConstructor _levelConstructor;

        private Vector3 _worldPosition;
        private Vector3Int _position;
        public Vector3Int Position
        {
            get => _position;
            set
            {
                _position = value;
                _worldPosition = _levelConstructor.levelSO.VoxelToWorldPosition(_position);
            }
        }
        
        public VoxelType VoxelType
        {
            get => _voxelType;
            set
            {
                _voxelType = value;

                var shape = CenteredVoxel.Shape(_voxelType, _levelConstructor.levelSO.voxelSize);
                _mesh = shape.MeshInfo.Mesh;
            }
        }

        public Brush(VoxelType voxelType, LevelConstructor levelConstructor)
        {
            _levelConstructor = levelConstructor;
            VoxelType = voxelType;
        }

        public void UseBrush(Shape shape)
        {
            _levelConstructor.EditorLevel.AddVoxel(VoxelType, _position, shape);
        }

        public void Render()
        {
            if (!IsActive) return;
            
            VoxelType.Material.SetPass(0);
            Graphics.DrawMeshNow(_mesh, _worldPosition, Quaternion.identity, 1);
            HandleUtility.Repaint();
        }
    }
}