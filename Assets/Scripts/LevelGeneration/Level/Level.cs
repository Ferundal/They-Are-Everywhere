using System.Collections.Generic;
using LevelConstructor;
using UnityEngine;

namespace LevelGeneration
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
    public class Level : ScriptableObject
    {
        public float voxelSize;
        public List<Shape> shapes = new();
        private ThreeDimensionalMatrix<Voxel> _voxelMatrix;
        private readonly VoxelPalette _voxelPalette = new();

        private void OnEnable()
        {
            _voxelMatrix = new ThreeDimensionalMatrix<Voxel>();
            LoadVoxelsIntoMatrixAndEstablishConnections();
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
                        Debug.LogWarning($"$Voxel type \"{voxel.typeName}\" not found. Voxel do not added to level");
                        continue;
                    }
                    
                    _voxelMatrix[voxel.position] = voxel;

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
            var voxelType = _voxelPalette.GetVoxelType(voxel.typeName);

            if (voxelType == null)
            {
                return false;
            }

            voxel.VoxelType = voxelType;
            return true;
        }

        private void AddPointsToSides(List<Side> sides)
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
        

        private bool TryGetPointFromNeighborVoxels(Side side, Vector3Int pointDirection, out Point point)
        {
            point = null;
            foreach (var directionsToPointNeighbor in Voxel.FindDirectionsToPointNeighbors(pointDirection))
            {
                var neighborPosition = side.ParentVoxel.position + directionsToPointNeighbor;
                var neighbor = _voxelMatrix[neighborPosition];

                if (neighbor == null)
                {
                    continue;
                }

                var neighborDirectionToPoint = (directionsToPointNeighbor * 2) - pointDirection;
                        
                if (neighbor.Points.TryGetValue(neighborDirectionToPoint, out point))
                {
                    break;
                }
            }

            return point != null;
        }
        
        public class VoxelPalette
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