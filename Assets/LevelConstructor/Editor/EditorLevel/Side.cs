using System;
using System.Text;
using UnityEngine;

namespace LevelConstructor
{
    public class Side : MonoBehaviour
    {
        private static LevelGeneration.Voxel _centeredVoxel;
        private LevelGeneration.Side sideSO;

        public static Side Create(LevelGeneration.Side sideSO, GameObject parentGameObject)
        {
            var sideGameObject = new GameObject($"Side (direction = {sideSO.sideDirection.ToString()})");
            
            var side = (Side)sideGameObject.AddComponent(typeof(Side));
            side.sideSO = sideSO;
            
            sideGameObject.transform.SetParent(parentGameObject.transform);

            AddMesh(sideGameObject, side);
            sideGameObject.transform.localPosition = Vector3.zero;

            return side;
        }

        public LevelGeneration.Side GetCenteredVoxelSide()
        {
            if (_centeredVoxel == null)
            {
                _centeredVoxel = new LevelGeneration.Voxel();
                _centeredVoxel.voxelTypeName = sideSO.ParentVoxel.voxelTypeName;
                var shapeSO = new LevelGeneration.Shape();
                shapeSO.voxels.Add(_centeredVoxel);
                var levelSO = new LevelGeneration.Level();
                levelSO.shapes.Add(shapeSO);
                levelSO.zeroVoxelWorldOffset = Vector3.zero;

                foreach (var direction in LevelGeneration.Voxel.SideDirections)
                {
                    var side = new LevelGeneration.Side();
                    side.sideDirection = direction;
                    _centeredVoxel.sides.Add(side);
                }
                levelSO.Initialize();
            }
            else
            {
                _centeredVoxel.voxelTypeName = sideSO.ParentVoxel.voxelTypeName;
            }
            _centeredVoxel.ParentShape.ParentLevel.voxelSize = sideSO.ParentVoxel.ParentShape.ParentLevel.voxelSize;

            foreach (var side in _centeredVoxel.sides)
            {
                if (side.sideDirection == sideSO.sideDirection)
                {
                    return side;
                }
            }

            return null;
        }
        
        public Vector3 OffsetToVoxel
        {
            get
            {
                var doubleSizeSideOffset = new Vector3(sideSO.sideDirection.x, sideSO.sideDirection.y, sideSO.sideDirection.z);
                return doubleSizeSideOffset * (sideSO.ParentVoxel.ParentShape.ParentLevel.voxelSize / 2.0f);
            }
        }

        private static void AddMesh(GameObject gameObject, Side side)
        {
            var mesh = new Mesh();

            var meshInfo = side.GetCenteredVoxelSide().MeshInfo;
            mesh.vertices = meshInfo.PointsAsVector3s;
            mesh.triangles = meshInfo.TrianglesVertexIndexes.ToArray();

            mesh.RecalculateNormals();

            var meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();

            var defaultMaterial  = new Material(Shader.Find("Standard"));
            meshRenderer.material = defaultMaterial;
        }
        
    }
}