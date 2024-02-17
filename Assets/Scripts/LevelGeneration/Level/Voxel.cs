using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelGeneration
{
    [Serializable]
    public class Voxel
    {
        public static Vector3Int[] SideDirections = new Vector3Int[6]
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.forward,
            Vector3Int.back,
            Vector3Int.right,
            Vector3Int.left
        };

        [NonSerialized] public Shape ParentShape;
        
        public string voxelTypeName;
        [NonSerialized] public VoxelType VoxelType;
        public Vector3Int position;
        public List<Side> sides = new();

        public Dictionary<Vector3Int, Point> Points = new();

        public Voxel() {}
        public Voxel(string voxelTypeName = "Empty")
        {
            this.voxelTypeName = voxelTypeName;
        }

        public static Vector3Int[] FindDirectionsToPointNeighbors(Vector3Int directionToVoxelPoint)
        {
            var directions = new Vector3Int[7];
            var index = 0;
            
            for (var x = 0; x <= 1; x++)
            {
                for (var y = 0; y <= 1; y++)
                {
                    for (var z = 0; z <= 1; z++)
                    {
                        if (x == 0 && y == 0 && z == 0)
                        {
                            continue;
                        }

                        directions[index] = new Vector3Int(
                            directionToVoxelPoint.x * x,
                            directionToVoxelPoint.y * y,
                            directionToVoxelPoint.z * z);
                        index++;
                    }
                }
            }

            return directions;
        }
    }
}