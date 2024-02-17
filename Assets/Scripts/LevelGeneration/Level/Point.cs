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
                var voxelPosition = ParentVoxel.position;
                var worldVoxelPosition = new Vector3(voxelPosition.x, voxelPosition.y, voxelPosition.z);
                var voxelSize = ParentVoxel.ParentShape.ParentLevel.voxelSize;
                var pointOffset = new Vector3(DirectionToPoint.x, DirectionToPoint.y, DirectionToPoint.z) * (voxelSize / 2);
                var zeroVoxelWorldOffset = ParentVoxel.ParentShape.ParentLevel.zeroVoxelWorldOffset * voxelSize;
                return worldVoxelPosition - (zeroVoxelWorldOffset - pointOffset);
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