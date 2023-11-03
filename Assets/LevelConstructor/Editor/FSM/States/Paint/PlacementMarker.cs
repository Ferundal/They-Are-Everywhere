using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class PlacementMarker
    {
        public bool IsActive;
        private MeshRenderer _meshRenderer;
        private Mesh _mesh;
        private Matrix4x4 _localToWorldMatrix;

        private Vector3 _position;

        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                Matrix4x4 newMatrix = Matrix4x4.TRS(_position, Quaternion.identity, Vector3.one);
                _localToWorldMatrix = newMatrix * _localToWorldMatrix;
            }
        }

        public PlacementMarker(GameObject placementMarkerPrefab)
        {
            Position = Vector3.zero;
            
            var prefabLocalToWorldMatrix = placementMarkerPrefab.transform.localToWorldMatrix;
            _localToWorldMatrix = new Matrix4x4(
                prefabLocalToWorldMatrix.GetRow(0),
                prefabLocalToWorldMatrix.GetRow(1),
                prefabLocalToWorldMatrix.GetRow(2),
                prefabLocalToWorldMatrix.GetRow(3));
            _meshRenderer = placementMarkerPrefab.GetComponent<MeshRenderer>();
            var filter = _meshRenderer.GetComponent<MeshFilter>();
            _mesh = filter.sharedMesh;
        }

        public void Render()
        {
            Debug.Log("Render");
            if (!IsActive) return;
            _meshRenderer.sharedMaterial.SetPass(0);
            Graphics.DrawMeshNow(_mesh, _localToWorldMatrix, 0);
        }
    }
}