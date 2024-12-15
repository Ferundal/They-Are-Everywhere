using System;
using LevelConstructor;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;
using Object = UnityEngine.Object;

namespace Editor.Scripts.FSM.States.NavMeshBaker
{
    public class NavMeshBaker : State
    {
        private const string SurfacesGameObjectName = "surfaces";
        
        private LevelConstructorEditor _levelConstructorEditor;
        private Button _createSurfacesButton;
        private Action _createSurfaces;
        private Button _createShapesButton;
        private Action _createShapes;

        public override VisualElement Root => Panel.Body;

        public NavMeshBaker(VisualPanel panel, LevelConstructorEditor levelConstructorEditor) : base(panel)
        {
            _levelConstructorEditor = levelConstructorEditor;

            _createSurfacesButton = Panel.Body.Q<Button>("create_surfaces");
            _createShapesButton = Panel.Body.Q<Button>("create_shapes");

            _createSurfaces = () => CreateSurfaces();
            _createShapes = () => CreateShapes();
        }

        public override void OnEnter()
        {
            _createSurfacesButton.clicked += _createSurfaces;
            _createShapesButton.clicked += _createShapes;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _createSurfacesButton.clicked -= _createSurfaces;
            _createShapesButton.clicked -= _createShapes;
            base.OnExit();
        }

        private void CreateShapes()
        {
            var shapes = _levelConstructorEditor._levelConstructor.EditorLevel.Shapes;
            foreach (var shape in shapes)
            {
                var shapeTransform =
                    _levelConstructorEditor._levelConstructor.transform.Find($"baked-{shape.shapeSO.shapeName}");
                GameObject shapeGameObject;
                if (shapeTransform == null)
                {
                    shapeGameObject = new GameObject($"baked-{shape.shapeSO.shapeName}");
                }
                else
                {
                    shapeGameObject = shapeTransform.gameObject;
                }
                
                shapeGameObject.transform.SetParent(_levelConstructorEditor._levelConstructor.transform);
                
                var mesh = shape.shapeSO.MeshInfo.Mesh;
                    
                MeshUtility.AddMesh(shapeGameObject, mesh);
                MeshUtility.AddMaterial(shapeGameObject, new Material(Shader.Find("Standard")));
            }
        }

        private void CreateSurfaces()
        {
            var shapes = _levelConstructorEditor._levelConstructor.EditorLevel.Shapes;
            
            foreach (var shape in shapes)
            {
                var surfaces = shape.shapeSO.MultiviewProjection;
                
                if (surfaces.Count < 1) return;

                var surfacesTransform =
                    _levelConstructorEditor._levelConstructor.transform.Find(SurfacesGameObjectName);
                GameObject surfacesParent;
                if (surfacesTransform == null)
                {
                    surfacesParent = new GameObject($"{SurfacesGameObjectName}");
                }
                else
                {
                    surfacesParent = surfacesTransform.gameObject;
                    foreach (Transform child in surfacesParent.transform) {
                        Object.DestroyImmediate(child.gameObject);
                    }
                }
                
                surfacesParent.transform.SetParent(_levelConstructorEditor._levelConstructor.transform);
                
                foreach (var surface in surfaces)
                {
                    var surfaceGameObject = new GameObject($"Surface ({surface.Direction})");
                    surfaceGameObject.transform.SetParent(surfacesParent.transform);

                    var mesh = surface.MeshInfo.Mesh;
                    
                    MeshUtility.AddMesh(surfaceGameObject, mesh);
                    MeshUtility.AddMaterial(surfaceGameObject, new Material(Shader.Find("Standard")));
                }
            }
        }

    }
}