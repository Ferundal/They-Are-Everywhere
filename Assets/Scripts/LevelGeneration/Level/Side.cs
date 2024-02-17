using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Utility;

namespace LevelGeneration
{
    [Serializable]
    public class Side
    {
        public Vector3Int sideDirection;
        [NonSerialized]
        public Voxel ParentVoxel;

        public Dictionary<Vector3Int, Point> Points = new(4);

        public Vector3Int[] PointDirections
        {
            get
            {
                var direction = new Vector3Int(0, 1, 0);
                var pointDirections = new Vector3Int[]
                {
                    new Vector3Int(1, 1, 1),
                    new Vector3Int(1, 1, -1),
                    new Vector3Int(-1, 1, 1),
                    new Vector3Int(-1, 1, -1)
                };
                
                var rotation = Quaternion.FromToRotation(direction, sideDirection);

                for (var index = 0; index < pointDirections.Length; index++)
                {
                    pointDirections[index] = Vector3IntUtility.RotateVector(rotation, pointDirections[index]);
                }

                return pointDirections;
            }
        }
        
        

        public MeshInfo MeshInfo => ParentVoxel.VoxelType.MeshGenerator.GenerateSideMeshInfo(this);
    }
}