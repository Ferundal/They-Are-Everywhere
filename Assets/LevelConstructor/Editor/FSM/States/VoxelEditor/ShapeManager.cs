using System;
using System.Linq;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class ShapeManager
    {
        private Level _level;
        private TextField _newShapeName;
        private Button _newShapeButton;
        private DropdownField _shapeSelector;
        private BrushSelector _brushSelector;
        private Action _clickAction;
        
        public Shape CurrentShape { get; private set; }
        
        public ShapeManager(VisualPanel visualPanel, LevelConstructor levelConstructor)
        {
            _level = levelConstructor.EditorLevel;
            SetUpNewShapeCreation(visualPanel);
            SetUpShapeSelection(visualPanel);
        }

        private void SetUpShapeSelection(VisualPanel visualPanel)
        {
            _shapeSelector = visualPanel.Body.Q<DropdownField>("shape_selector");
            _shapeSelector.choices.Clear();
            
            foreach (var shape in _level.shapes)
            {
                _shapeSelector.choices.Add(shape.shapeSO.shapeName);
            }
            
            _shapeSelector.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                _shapeSelector.value = evt.newValue;
                CurrentShape = _level.shapes.FirstOrDefault(shape => shape.shapeSO.shapeName == evt.newValue);
            });
        }

        private void SetUpNewShapeCreation(VisualPanel visualPanel)
        {
            _newShapeName = visualPanel.Body.Q<TextField>("new_shape_name");
            _newShapeButton = visualPanel.Body.Q<Button>("new_shape_button");
            _clickAction = () => AddNewShape(_newShapeName.value);
            _newShapeButton.clicked += _clickAction;
        }
        
        private void AddNewShape(string shapeName)
        {
            var newShape = _level.AddShape(shapeName);
            CurrentShape = newShape;
            _shapeSelector.choices.Add(newShape.shapeSO.shapeName);
            _shapeSelector.value = newShape.shapeSO.shapeName;
        }

        public void OnDestroy()
        {
            _newShapeButton.clicked -= _clickAction;
        }
    }
}