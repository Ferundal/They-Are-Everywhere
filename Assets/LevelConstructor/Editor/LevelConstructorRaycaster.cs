using System;
using UnityEditor;
using UnityEngine;

namespace LevelConstructor
{
    public class LevelConstructorRaycaster
    {
        private Ray _ray;
        private RaycastHit _hitInfo;
        private Vector3 _groundTouchPosition;

        private readonly LevelConstructor _levelConstructor;

        public LevelConstructorRaycaster(LevelConstructor levelConstructor)
        {
            _levelConstructor = levelConstructor;
        }
        
        public bool ScreenPositionToVoxelPosition(
            Vector2 screenPosition,
            ref Vector3Int voxelPosition,
            ref VoxelDirection voxelDirection,
            Brush brush)
        {
            _ray = HandleUtility.GUIPointToWorldRay(screenPosition);

            if (Physics.Raycast(_ray, out _hitInfo) 
                && _hitInfo.collider.gameObject.TryGetComponent<Voxel>(out var voxel)
                && voxel.levelConstructor == _levelConstructor)
            {
                voxelPosition = voxel.position;
                voxelDirection = voxel.ClosestSideDirection(_hitInfo.point);
                return true;
            }

            if (FindGroundTouchPosition())
            {
                var brushSize = brush.VoxelPrefab.size;
                voxelPosition = new Vector3Int(
                    (int)(Math.Ceiling(_groundTouchPosition.x / brushSize.x)), 
                    -1,
                    (int)(Math.Ceiling(_groundTouchPosition.z / brushSize.z)));
                Debug.Log(voxelPosition + " " + _groundTouchPosition);
                voxelDirection = VoxelDirection.Up;
                return true;
            }

            return false;
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