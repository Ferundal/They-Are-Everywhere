using UnityEngine;

namespace LevelConstructor
{
    public class MeshUtility
    {
        public static void AddMesh(GameObject gameObject, Mesh mesh)
        {
            var meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
        }
        
        public static void AddMaterial(GameObject gameObject, Material material)
        {
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
        }
        
        public static void AddCollider(GameObject gameObject, Mesh mesh)
        {
            var meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }
    }
}