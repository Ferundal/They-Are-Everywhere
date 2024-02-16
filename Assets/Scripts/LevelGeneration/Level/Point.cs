using UnityEngine;

namespace LevelGeneration
{
    public class Point
    {
        public Voxel ParentVoxel;
        public Vector3Int DirectionToPoint;

        public Point(Voxel parentVoxel, Vector3Int directionToPoint)
        {
            ParentVoxel = parentVoxel;
            DirectionToPoint = directionToPoint;
        }

        public Vector3 Position
        {
            get
            {
                var doubleVoxelPosition = ParentVoxel.position * 2;
                var aboveGroundDoubleVoxelPosition = doubleVoxelPosition + Vector3Int.one;
                var doublePointPosition = aboveGroundDoubleVoxelPosition - DirectionToPoint;
                var voxelSize = ParentVoxel.ParentShape.ParentLevel.voxelSize;
                var floatPointPosition = new Vector3(doublePointPosition.x, doublePointPosition.y, doublePointPosition.z) * (voxelSize / 2.0f);
                return floatPointPosition;
            }
        }

        public static Vector3Int[] NeighborVoxelDirections(Vector3Int directionToExcludedVoxel)
        {
            Vector3Int[] directions = new Vector3Int[7];

            int index = 0;
            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                {
                    for (int z = -1; z <= 1; z += 2)
                    {
                        var direction = new Vector3Int(x, y, z);
                        if (direction == directionToExcludedVoxel)
                        {
                            continue;
                        }

                        directions[index] = direction;
                        index++;
                    }
                }
            }

            return directions;
        }
    }
}