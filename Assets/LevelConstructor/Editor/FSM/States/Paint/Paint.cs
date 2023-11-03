using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelConstructor
{
    public class Paint : State
    {
        private EventHandler _eventHandler;
        private LevelConstructor _levelConstructor;
        private Brush _currentBrush;
        private Vector3Int _touchedVoxelPosition = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
        private Vector3Int _newTouchedVoxelPosition = new();
        private VoxelDirection _voxelDirection = new();
        private Vector3Int _brushPosition;
        
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
            if (!_currentBrush.BrushPlacementMarker.IsActive) return;
            
            _levelConstructor.AddVoxel(_currentBrush.VoxelPrefab, _brushPosition);
        }

        private void ChangeBrushPosition(Event currentEvent)
        {
            if (!_levelConstructor.Raycaster.ScreenPositionToVoxelPosition(
                    currentEvent.mousePosition, 
                    ref _newTouchedVoxelPosition, 
                    ref _voxelDirection))
            {
                _currentBrush.BrushPlacementMarker.IsActive = false;
                return;
            }
            if (_newTouchedVoxelPosition == _touchedVoxelPosition)
            {
                return;
            }
            _touchedVoxelPosition = _newTouchedVoxelPosition;
            _currentBrush.BrushPlacementMarker.IsActive = true;
            _brushPosition = VoxelPositionUtility.NextInDirection(_touchedVoxelPosition, _voxelDirection);
            _currentBrush.BrushPlacementMarker.Position = _levelConstructor.VoxelPositionToWorldPosition(_brushPosition);
        }
        
        public override void OnSceneGUI()
        {
            _currentBrush.BrushPlacementMarker.Render();
        }

        private void InitiateBrushes()
        {
            var voxelPrefabs = _levelConstructor.voxelPrefabs;
            Brush[] brushes = voxelPrefabs.Select(item => new Brush(item)).ToArray();
            _currentBrush = brushes[0];
        }
    }
}