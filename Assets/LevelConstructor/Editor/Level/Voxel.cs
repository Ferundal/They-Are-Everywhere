using System;
using UnityEngine;

namespace LevelConstructor
{
    [Serializable]
    public class Voxel : MonoBehaviour
    {
        public LevelConstructor levelConstructor;
        public string voxelType;
        public Vector3Int position;

        public VoxelDirection ClosestSideDirection(Vector3 point)
        {
            Vector3 cubeCenter = transform.position;
            
            Vector3 fromCenterToPoint = point - cubeCenter;
            
            Vector3 absDifference = new Vector3(Mathf.Abs(fromCenterToPoint.x), Mathf.Abs(fromCenterToPoint.y), Mathf.Abs(fromCenterToPoint.z));
            
            float maxDifference = Mathf.Max(absDifference.x, absDifference.y, absDifference.z);
            
            if (maxDifference == absDifference.x)
            {
                return (fromCenterToPoint.x > 0) ? VoxelDirection.Right : VoxelDirection.Left;
            }

            if (maxDifference == absDifference.y)
            {
                return (fromCenterToPoint.y > 0) ? VoxelDirection.Up : VoxelDirection.Down;
            }
            
            return (fromCenterToPoint.z > 0) ? VoxelDirection.Forward : VoxelDirection.Backward;
        }
    }
}