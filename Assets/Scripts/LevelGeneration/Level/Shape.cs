using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace LevelGeneration
{
    [Serializable]
    public class Shape
    {
        [NonSerialized] public Level ParentLevel;
        
        public string shapeName;
        public List<Voxel> voxels = new();

        public MeshInfo MeshInfo
        {
            get
            {
                MeshInfo meshInfo = new();

                var multiviewProjection = MultiviewProjection;

                foreach (var view in multiviewProjection)
                {
                    var viewMesh = new MeshInfo(view.MeshInfo);
                    viewMesh.AddOffsetToIndexes(meshInfo.Points.Count);
                    meshInfo.Points.AddRange(viewMesh.Points);
                    meshInfo.TrianglesVertexIndexes.AddRange(viewMesh.TrianglesVertexIndexes);
                }

                return meshInfo;
            }
        }

        public List<Surface> MultiviewProjection
        {
            get
            {
                var multiviewProjection = new Dictionary<Vector3Int, List<Side>>();

                foreach (var voxel in voxels)
                {
                    foreach (var side in voxel.sides)
                    {
                        if (!multiviewProjection.ContainsKey(side.sideDirection))
                        {
                            multiviewProjection.Add(side.sideDirection, new List<Side>());
                        }

                        multiviewProjection[side.sideDirection].Add(side);
                    }
                }

                var surfaces = new List<Surface>();

                foreach (var view in multiviewProjection)
                {
                    surfaces.Add(new Surface(view.Value));
                }
                return surfaces;
            }
        }
    }
}