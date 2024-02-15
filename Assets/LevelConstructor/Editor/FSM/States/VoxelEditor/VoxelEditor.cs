using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class VoxelEditor : State
    {
        
        public VoxelEditorRaycaster Raycaster { get; private set; }
        
        private EventHandler _eventHandler;
        private LevelConstructor _levelConstructor;
        private List<Brush> _brushes = new();
        private Brush _currentBrush;
        private Vector3Int _touchedVoxelPosition = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
        private Vector3Int _newTouchedVoxelPosition = new();
        private Vector3Int _brushPosition;

        public override VisualElement Root => Panel.Body;

        public VoxelEditor(VisualPanel panel, EventHandler eventHandler, LevelConstructor levelConstructor) : base(panel)
        {
            Raycaster = new VoxelEditorRaycaster(levelConstructor);
            _eventHandler = eventHandler;
            _levelConstructor = levelConstructor;
            InitiateBrushes();
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
            if (_currentBrush == null) return;
            if (!_currentBrush.BrushPlacementMarker.IsActive) return;
            
        }

        private void ChangeBrushPosition(Event currentEvent)
        {
            /*if (!_levelConstructor.Raycaster.ScreenPositionToVoxelPosition(
                    currentEvent.mousePosition, 
                    ref _newTouchedVoxelPosition, 
                    ref _newVoxelDirection,
                    _currentBrush))
            {
                _currentBrush.BrushPlacementMarker.IsActive = false;
                return;
            }
            if (_newTouchedVoxelPosition == _touchedVoxelPosition && _newVoxelDirection == _voxelDirection)
            {
                return;
            }
            _touchedVoxelPosition = _newTouchedVoxelPosition;
            _voxelDirection = _newVoxelDirection;
            _currentBrush.BrushPlacementMarker.IsActive = true;
            _brushPosition = VoxelPositionUtility.NextInDirection(_touchedVoxelPosition, _voxelDirection);
            _currentBrush.BrushPlacementMarker.Position = _levelConstructor.VoxelPositionToWorldPosition(_brushPosition, _currentBrush.VoxelPrefab);*/
        }
        
        public override void OnSceneGUI()
        {
//            _currentBrush.BrushPlacementMarker.Render();
        }

        private void InitiateBrushes()
        {
            /*_brushes.Add(new Brush("GrayBlock.prefab"));
            _brushes.Add(new Brush("RedBlock.prefab"));
            _brushes.Add(new Brush("Floor.prefab"));
            var panelSelector = Panel.Body.Q<DropdownField>("brush_selector");
            panelSelector.choices.Clear();
            foreach (var brush in _brushes)
            {
                panelSelector.choices.Add(brush.VoxelPrefab.voxelType);
            }
            _currentBrush = _brushes[0];
            panelSelector.value = _currentBrush.VoxelPrefab.voxelType;
            panelSelector.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                panelSelector.value = evt.newValue;
                _currentBrush = _brushes[panelSelector.index];
            });*/
        }
    }
}