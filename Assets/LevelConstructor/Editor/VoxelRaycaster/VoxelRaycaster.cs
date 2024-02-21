using System;
using UnityEditor;
using UnityEngine;

namespace LevelConstructor
{
    public class VoxelRaycaster
    {
        private Ray _ray;
        private RaycastHit _hitInfo;
        private Vector3 _groundTouchPosition;

        private readonly LevelConstructor _levelConstructor;

        public VoxelRaycaster(LevelConstructor levelConstructor)
        {
            _levelConstructor = levelConstructor;
        }
        
        public bool Raycast(
            Vector2 screenPosition,
            ref VoxelHit voxelHit)
        {
            _ray = HandleUtility.GUIPointToWorldRay(screenPosition);

            if (Physics.Raycast(_ray, out _hitInfo) 
                && _hitInfo.collider.gameObject.TryGetComponent<Side>(out var side)
                && side.SideSO.ParentVoxel.ParentShape.ParentLevel == _levelConstructor.levelSO)
            {
                voxelHit.Position = side.SideSO.ParentVoxel.position;
                voxelHit.Direction = side.SideSO.sideDirection;
                
                if (_levelConstructor.levelSO.GetVoxel(voxelHit.Position + voxelHit.Direction) != null)
                {
                    return false;
                }

                return true;
            }

            if (!FindGroundTouchPosition()) return false;
            
            var cellSize = _levelConstructor.levelSO.voxelSize;
            voxelHit.Position = new Vector3Int(
                (int)(Math.Ceiling(_groundTouchPosition.x / cellSize) - 1), 
                -2,
                (int)(Math.Ceiling(_groundTouchPosition.z / cellSize)) - 1);
            voxelHit.Direction = Vector3Int.up;
            return true;

        }

        private bool FindGroundTouchPosition()
        {
            if (_ray.direction.z == 0f) return false;

            var levelConstructorPosition = _levelConstructor.transform.position;
            float rayDistance = (levelConstructorPosition.y - _ray.origin.y) / _ray.direction.y;
            _groundTouchPosition = _ray.origin + _ray.direction * rayDistance;
            _groundTouchPosition -= levelConstructorPosition;
            return true;
        }
    }
}