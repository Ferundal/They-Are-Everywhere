using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class Paint : State
    {
        private EventHandler _eventHandler;
        private LevelConstructor _levelConstructor;
        private Brush [] _brushes;
        private Brush _currentBrush;
        private Vector3Int _touchedVoxelPosition = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
        private Vector3Int _newTouchedVoxelPosition = new();
        private VoxelDirection _voxelDirection = new();
        private VoxelDirection _newVoxelDirection = new();
        private Vector3Int _brushPosition;

        public override VisualElement Root => Panel.Body;

        public Paint(VisualPanel panel, EventHandler eventHandler, LevelConstructor levelConstructor) : base(panel)
        {
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
            
            _levelConstructor.AddVoxel(_currentBrush.VoxelPrefab, _brushPosition);
        }

        private void ChangeBrushPosition(Event currentEvent)
        {
            if (!_levelConstructor.Raycaster.ScreenPositionToVoxelPosition(
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
            _currentBrush.BrushPlacementMarker.Position = _levelConstructor.VoxelPositionToWorldPosition(_brushPosition, _currentBrush.VoxelPrefab);
        }
        
        public override void OnSceneGUI()
        {
            _currentBrush.BrushPlacementMarker.Render();
        }

        private void InitiateBrushes()
        {
            var voxelPrefabs = _levelConstructor.voxelPrefabs;
            _brushes = voxelPrefabs.Select(item => new Brush(item)).ToArray();
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
            });
        }
        
        private void OnDropdownValueChanged(int index)
        {
            Debug.Log("Выбран элемент с индексом: " + index);
        }
    }
}