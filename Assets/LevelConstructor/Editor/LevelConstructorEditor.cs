using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using LevelConstructor.Editor.FSM.States;
using LevelConstructor.Editor.Utility;

namespace LevelConstructor
{
    [CustomEditor(typeof(LevelConstructor))]
    public class LevelConstructorEditor : UnityEditor.Editor
    {
        private readonly EventHandler _eventHandler = new();
        
        private VisualElement _root;
        private VisualElement _levelPropertyField;
        private VisualElement _noLevelObjectWarning;
        private VisualElement _editorRoot;

        private FSM _fsm;
        

        private void Awake()
        {
            _fsm = new FSM();
            CreateStates();
        }

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();

            var levelScriptableObject = serializedObject.FindProperty("serializedLevel");
            
            _levelPropertyField = new PropertyField(levelScriptableObject);;
            _root.Add(_levelPropertyField);

            _noLevelObjectWarning = CreateNoLevelObjectWarning();
            _editorRoot = _fsm.Root;

            if (levelScriptableObject.objectReferenceValue != null)
            {
                _root.Add(_editorRoot);
            }
            else
            {
                _root.Add(_noLevelObjectWarning);
            }
            
            return _root;
        }

        private void OnSceneGUI()
        {
            //base.OnInspectorGUI();
            _eventHandler.ProcessEvent(Event.current);
        }
        
        private VisualElement CreateNoLevelObjectWarning()
        {
            return new Label("Add level scriptable object to work");
        }

        private void CreateStates()
        {
            var visualPanel = new VisualPanel(
                $"{PathUtility.PanelsPath}/PaintPanel.uxml", 
                $"{PathUtility.PanelsPath}/Panel.uss");
            
            var paintState = new Paint(visualPanel);
            _fsm.Add(paintState);
        }
        
        private void SetLevelEditorActive(bool isActive)
        {
            
        }
    }
}
