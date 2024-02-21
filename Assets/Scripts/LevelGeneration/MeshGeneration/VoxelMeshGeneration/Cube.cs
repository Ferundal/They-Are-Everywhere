using System;
using System.Linq;
using UnityEngine;

namespace LevelGeneration
{
    public class Cube : IVoxelMeshGenerator
    {
        public MeshInfo GenerateSideMeshInfo(Side side)
        {
            var meshInfo = new MeshInfo();

            foreach (var point in side.Points)
            {
                meshInfo.Points.Add(point.Value);
            }
            
            meshInfo.TrianglesVertexIndexes.AddRange(new [] { 0, 1, 2, 1, 3, 2 });
            
            return meshInfo;
        }

        public void GenerateMesh(Voxel voxel)
        {
            
        }
    }
}