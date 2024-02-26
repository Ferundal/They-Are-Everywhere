using System;
using LevelGeneration;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace LevelConstructor
{
    public class NavMeshBaker : State
    {
        private const string SurfacesGameObjectName = "surfaces";
        
        private LevelConstructorEditor _levelConstructorEditor;
        private Button _button;
        private bool _isBaking = false;
        private ProgressBar _progressBar;
        private Action _createSurfaces;

        public override VisualElement Root { get; protected set; } = new();

        private string ProgressBarText {
            get
            {
                return _progressBar.title;
            }
            set
            {
                _progressBar.title = value;
            }
        }

        public NavMeshBaker(VisualPanel panel, LevelConstructorEditor levelConstructorEditor) : base(panel)
        {
            _levelConstructorEditor = levelConstructorEditor;

            _button = Panel.Body.Q<Button>("create_surfaces");
            _progressBar = Panel.Body.Q<ProgressBar>("progress_bar");

            _createSurfaces = () => CreateSurfaces();
            
            AssembleBodyPanel();
        }

        public override void OnEnter()
        {
            _button.clicked += _createSurfaces;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _button.clicked -= _createSurfaces;
            base.OnExit();
        }

        private void CreateSurfaces()
        {
            var shapes = _levelConstructorEditor._levelConstructor.EditorLevel.shapes;
            
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
                    surfacesParent = _levelConstructorEditor._levelConstructor.transform.Find(SurfacesGameObjectName).gameObject;
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
        

        private void AssembleBodyPanel()
        {
            AssembleOptionalPanels();
        }

        private void AssembleOptionalPanels()
        {
            AssembleOptionalBakingStatusPanels();
        }

        private void AssembleOptionalBakingStatusPanels()
        {
            if (_isBaking) 
            {
                Root.Add(_progressBar);
            }
            else
            {
                Root.Add(_button);
            }
        }
        
    }
}