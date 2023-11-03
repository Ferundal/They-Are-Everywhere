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
        
        public bool ScreenPositionToVoxelPosition(Vector2 screenPosition, ref Vector3Int voxelPosition, ref VoxelDirection voxelDirection)
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
                voxelPosition = new Vector3Int((int)_groundTouchPosition.x, 0, (int)_groundTouchPosition.z);
                voxelDirection = VoxelDirection.Up;
                return true;
            }
            
            return false;
        }

        private bool FindGroundTouchPosition()
        {
            if (_ray.direction.z == 0f) return false;

            float rayDistance = (_levelConstructor.transform.position.y - _ray.origin.y) / _ray.direction.y;
            _groundTouchPosition = _ray.origin + _ray.direction * rayDistance;

            return true;
        }
    }
}