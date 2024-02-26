using LevelGeneration;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class SurfaceTest
    {
        [Test]
        public void TwoSidesSurfacePointsCountTest()
        {
            var shape = new Shape();
            for (int i = 0; i < 2; i++)
            {
                var voxel = new Voxel();
                voxel.voxelTypeName = "Cube";
                voxel.position = new Vector3Int(0, 0, i);
                
                var side = new Side();
                side.sideDirection = Vector3Int.up;
                side.ParentVoxel = voxel;

                voxel.sides.Add(side);
                
                shape.voxels.Add(voxel);
                voxel.ParentShape = shape;
            }

            var level = new Level();
            level.shapes.Add(shape);
            shape.ParentLevel = level;
            
            level.Initialize();

            var meshInfo = shape.MeshInfo;
            var mesh = meshInfo.Mesh;
            
            
            Assert.AreEqual(6, meshInfo.Points.Count);
        }
    }
}