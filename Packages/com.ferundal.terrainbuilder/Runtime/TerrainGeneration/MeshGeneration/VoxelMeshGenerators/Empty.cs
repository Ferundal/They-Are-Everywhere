using System.Linq;
using TerrainGeneration.MeshGeneration;
using TerrainGeneration.ModelLevel;
using UnityEngine;

namespace LevelGeneration
{
    public class Empty : IVoxelMeshGenerator
    {
        public MeshInfo GenerateSideMeshInfo(Side side)
        {
            var meshInfo = new MeshInfo();

            return meshInfo;
        }

        public void GenerateMesh(Voxel voxel)
        {
            foreach (var side in voxel.sides)
            {
                
            }
        }
    }
}