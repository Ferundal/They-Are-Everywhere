using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class VoxelEditor : State
    {
        
        public VoxelRaycaster Raycaster { get; private set; }
        
        private EventHandler _eventHandler;
        private LevelConstructor _levelConstructor;
        private BrushPalette _brushPalette;
        private VoxelHit _lastHit = new();
        private VoxelHit _newHit = new();

        public override VisualElement Root => Panel.Body;

        public VoxelEditor(VisualPanel panel, LevelConstructor levelConstructor) : base(panel)
        {
            Raycaster = new VoxelRaycaster(levelConstructor);
            _eventHandler = levelConstructor.Handler;
            _levelConstructor = levelConstructor;
            _brushPalette = new BrushPalette(panel, _levelConstructor);
        }

        public override void OnEnter()
        {
            _eventHandler.OnMouseMove += ChangeBrushPosition;
            _eventHandler.OnMouseDown += UseBrush;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _eventHandler.OnMouseMove -= ChangeBrushPosition;
            _eventHandler.OnMouseDown -= UseBrush;
            base.OnExit();
        }

        private void UseBrush(Event currentEvent)
        {
            //_brushPalette.CurrentBrush.UseBrush();
        }

        private void ChangeBrushPosition(Event currentEvent)
        {
            if (!Raycaster.Raycast(currentEvent.mousePosition, ref _newHit))
            {
                _brushPalette.CurrentBrush.IsActive = false;
                return;
            }
            if (_lastHit == _newHit)
            {
                return;
            }

            _lastHit.Position = _newHit.Position;
            _lastHit.Direction = _newHit.Direction;
            
            _brushPalette.CurrentBrush.Position = _newHit.Position + _newHit.Direction;
            _brushPalette.CurrentBrush.IsActive = true;
        }
        
        public override void OnSceneGUI()
        {
            _brushPalette.CurrentBrush.Render();
        }
        
        private class BrushPalette
        {
            private readonly VoxelType[] _voxelTypes;
            public Brush CurrentBrush;
            public BrushPalette(VisualPanel visualPanel, LevelConstructor levelConstructor)
            {
                var objects = Resources.LoadAll($"{ResourcesPathUtility.VoxelTypeFolder}", typeof(VoxelType));
                _voxelTypes = new VoxelType [objects.Length];
                for (int i = 0; i < objects.Length; i++)
                {
                    _voxelTypes[i] = (VoxelType)objects[i];
                }
                
                var brushesSelector = visualPanel.Body.Q<DropdownField>("brush_selector");

                CurrentBrush = new Brush(_voxelTypes[0], levelConstructor);
                
                foreach (var voxelType in _voxelTypes)
                {
                    brushesSelector.choices.Add(voxelType.name);
                }
                
                brushesSelector.RegisterCallback<ChangeEvent<string>>((evt) =>
                {
                    brushesSelector.value = evt.newValue;
                    CurrentBrush.VoxelType = _voxelTypes[brushesSelector.index];
                });
            }
        }
    }
}