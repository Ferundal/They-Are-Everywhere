using System;
using UnityEngine;

namespace LevelConstructor
{
    public class Side : MonoBehaviour
    {
        private LevelGeneration.Side sideSO;

        public static Side Create(LevelGeneration.Side sideSO, GameObject parentGameObject)
        {
            var sideGameObject = new GameObject($"Side (direction = {sideSO.sideDirection.ToString()})");
            
            var side = (Side)sideGameObject.AddComponent(typeof(Side));
            side.sideSO = sideSO;
            
            sideGameObject.transform.SetParent(parentGameObject.transform);

            AddMesh(sideGameObject, side);
            sideGameObject.transform.localPosition = side.OffsetToVoxel;

            return side;
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
            
            Debug.Log(side.sideSO.MeshInfo.ToString());

            mesh.vertices = side.sideSO.MeshInfo.PointsAsVector3s;
            mesh.triangles = side.sideSO.MeshInfo.TrianglesVertexIndexes.ToArray();
            

            mesh.RecalculateNormals();

            var meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();

            var defaultMaterial  = new Material(Shader.Find("Standard"));
            meshRenderer.material = defaultMaterial;
            meshFilter.mesh = mesh;
        }
        
    }
}