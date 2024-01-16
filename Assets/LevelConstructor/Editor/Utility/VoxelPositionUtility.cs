using UnityEngine;

namespace LevelConstructor
{
    public class VoxelPositionUtility
    {
        public static Vector3Int NextInDirection(Vector3Int position, VoxelDirection voxelDirection)
        {
            var next = new Vector3Int(position.x, position.y, position.z);
            switch (voxelDirection)
            {
                case VoxelDirection.Up:
                    ++next.y;
                    break;
                case VoxelDirection.Down:
                    --next.y;
                    break;
                case VoxelDirection.Right:
                    ++next.x;
                    break;
                case VoxelDirection.Left:
                    --next.x;
                    break;
                case VoxelDirection.Forward:
                    ++next.z;
                    break;
                case VoxelDirection.Backward:
                    --next.z;
                    break;
            }

            return next;
        }

        public static VoxelDirection GetOpposite(VoxelDirection voxelDirection)
        {
            VoxelDirection direction = new();
            switch (voxelDirection)
            {
                case VoxelDirection.Up:
                    direction = VoxelDirection.Down;
                    break;
                case VoxelDirection.Down:
                    direction =  VoxelDirection.Up;
                    break;
                case VoxelDirection.Right:
                    direction =  VoxelDirection.Left;
                    break;
                case VoxelDirection.Left:
                    direction =  VoxelDirection.Left;
                    break;
                case VoxelDirection.Forward:
                    direction =  VoxelDirection.Backward;
                    break;
                case VoxelDirection.Backward:
                    direction =  VoxelDirection.Forward;
                    break;
            }

            return direction;
        }
    }
}