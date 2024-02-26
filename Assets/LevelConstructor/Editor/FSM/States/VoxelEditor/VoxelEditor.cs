using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class VoxelEditor : State
    {
        
        public VoxelRaycaster Raycaster { get; private set; }
        
        private EventHandler _eventHandler;
        private LevelConstructor _levelConstructor;
        private VisualPanel _visualPanel;
        private ShapeManager _shapeManager;
        private BrushSelector _brushSelector;
        private Vector3Int _lastHit = new();
        private Vector3Int _newHit = new();

        public override VisualElement Root => Panel.Body;

        public VoxelEditor(VisualPanel panel, LevelConstructor levelConstructor) : base(panel)
        {
            Raycaster = new VoxelRaycaster(levelConstructor);
            _eventHandler = levelConstructor.Handler;
            _levelConstructor = levelConstructor;
            _visualPanel = panel;
        }

        public override void OnEnter()
        {
            _brushSelector = new BrushSelector(_visualPanel, _levelConstructor);
            _shapeManager = new ShapeManager(_visualPanel, _levelConstructor);
            _eventHandler.OnMouseMove += ChangeBrushPosition;
            _eventHandler.OnMouseDown += UseBrush;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _shapeManager?.OnDestroy();
            _eventHandler.OnMouseMove -= ChangeBrushPosition;
            _eventHandler.OnMouseDown -= UseBrush;
            base.OnExit();
        }

        private void UseBrush(Event currentEvent)
        {
            _brushSelector.CurrentBrush.UseBrush(_shapeManager.CurrentShape);
        }

        private void ChangeBrushPosition(Event currentEvent)
        {
            if (!Raycaster.Raycast(currentEvent.mousePosition, ref _newHit))
            {
                _brushSelector.CurrentBrush.IsActive = false;
                return;
            }
            if (_lastHit == _newHit)
            {
                return;
            }

            _lastHit = _newHit;
            _brushSelector.CurrentBrush.Position = _newHit;
            _brushSelector.CurrentBrush.IsActive = true;
        }
        
        public override void OnSceneGUI()
        {
            _brushSelector.CurrentBrush.Render();
        }
        

    }
}