using System;
using System.Collections.Generic;
using LevelConstructor;
using UnityEngine;

namespace LevelGeneration
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
    public class Level : ScriptableObject
    {
        public float voxelSize;
        public Vector3 zeroVoxelWorldOffset = new Vector3(0.5f, 0.5f, 0.5f);
        public List<Shape> shapes = new();
        public ThreeDimensionalMatrix<Voxel> VoxelMatrix;
        private readonly VoxelPalette _voxelPalette = new();

        private void OnEnable()
        {
            Initialize();
        }

        public void Initialize()
        {
            VoxelMatrix = new ThreeDimensionalMatrix<Voxel>();
            LoadVoxelsIntoMatrixAndEstablishConnections();
        }

        public Vector3 VoxelToWorldPosition(Vector3Int position)
        {
            var worldVoxelPosition = (new Vector3(position.x, position.y, position.z) + zeroVoxelWorldOffset) * voxelSize;
            return worldVoxelPosition;
        }

        public void AddPointsToSides(List<Side> sides)
        {
            foreach (var side in sides)
            {
                foreach (var pointDirection in side.PointDirections)
                {
                    if (side.Points.ContainsKey(pointDirection))
                    {
                        continue;
                    }

                    var parentVoxel = side.ParentVoxel;

                    if (parentVoxel.Points.TryGetValue(pointDirection, out var newPoint))
                    {
                        side.Points.Add(pointDirection, newPoint);
                        continue;
                    }

                    if (!TryGetPointFromNeighborVoxels(side, pointDirection, out newPoint))
                    {
                        newPoint = new Point(parentVoxel, pointDirection);
                    }

                    side.Points.Add(pointDirection, newPoint);
                    parentVoxel.Points.Add(pointDirection, newPoint);
                }
            }
            
        }

        private void LoadVoxelsIntoMatrixAndEstablishConnections()
        {
            List<Side> sides = new();
            
            foreach (var shape in shapes)
            {
                foreach (var voxel in shape.voxels)
                {
                    if (!TrySetVoxelType(voxel))
                    {
                        Debug.LogWarning($"$Voxel type \"{voxel.voxelTypeName}\" not found. Voxel do not added to level");
                        continue;
                    }
                    
                    VoxelMatrix[voxel.position] = voxel;

                    foreach (var side in voxel.sides)
                    {
                        side.ParentVoxel = voxel;
                        sides.Add(side);
                    }

                    voxel.ParentShape = shape;
                }

                shape.ParentLevel = this;
            }
            
            AddPointsToSides(sides);
        }

        private bool TrySetVoxelType(Voxel voxel)
        {
            var voxelType = _voxelPalette.GetVoxelType(voxel.voxelTypeName);

            if (voxelType == null)
            {
                return false;
            }

            voxel.VoxelType = voxelType;
            return true;
        }


        private bool TryGetPointFromNeighborVoxels(Side side, Vector3Int pointDirection, out Point point)
        {
            point = null;
            foreach (var directionsToPointNeighbor in Voxel.FindDirectionsToPointNeighbors(pointDirection))
            {
                var neighborPosition = side.ParentVoxel.position + directionsToPointNeighbor;
                var neighbor = VoxelMatrix[neighborPosition];

                if (neighbor == null)
                {
                    continue;
                }

                var neighborDirectionToPoint = pointDirection - (directionsToPointNeighbor * 2);
                        
                if (neighbor.Points.TryGetValue(neighborDirectionToPoint, out point))
                {
                    break;
                }
            }

            return point != null;
        }

        private class VoxelPalette
        {
            private readonly Dictionary<string, VoxelType> _voxelTypes = new ();
            
            public VoxelType GetVoxelType(string desiredTypeName)
            {
                foreach (var voxelType in _voxelTypes)
                {
                    if (voxelType.Key == desiredTypeName)
                    {
                        return voxelType.Value;
                    }
                }
                
                var desiredType = Resources.Load($"{ResourcesPathUtility.VoxelTypeFolder}/{desiredTypeName}") as VoxelType;
                if (desiredType != null)
                {
                    _voxelTypes.Add(desiredTypeName, desiredType);
                }
                
                return desiredType;
            }
        }
    }
}