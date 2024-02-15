using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    [CustomEditor(typeof(LevelConstructor))]
    public class LevelConstructorEditor : Editor
    {
        public LevelConstructor _levelConstructor;

        private SerializedProperty _levelSerializedProperty;
        private PropertyField _levelPropertyField;

        private VisualElement _root;
        private FSM _fsm;

        private EventHandler _eventHandler;
        private void OnEnable()
        {
            _levelConstructor = target as LevelConstructor;
            _root = new();
            _eventHandler = _levelConstructor.Handler;
            _fsm = new FSM();
            SceneView.duringSceneGui += OnDuringSceneGui;
            _eventHandler.HasUnprocessedGUIStart = true;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnDuringSceneGui;
            _eventHandler.OnAfterDeserialize -= CheckLevelSerializedProperty;
            _fsm.OnDestroy();
        }

        public override VisualElement CreateInspectorGUI()
        {
            _levelSerializedProperty = serializedObject.FindProperty("levelSO");
            _levelPropertyField = new PropertyField(_levelSerializedProperty);
            _root.Add(_levelPropertyField);
            
            CreateFSMStates();
            CheckLevelSerializedProperty();

            _eventHandler.OnAfterDeserialize += CheckLevelSerializedProperty;
            
            return _root;
        }

        private void OnSceneGUI()
        {
            //base.OnInspectorGUI();
            _eventHandler.ProcessEvent(Event.current);
            _fsm.OnSceneGUI();
        }
        
        
        
        private void CreateFSMStates()
        {
            var visualPanel = new VisualPanel(
                $"{PathUtility.PanelsPath}/VoxelEditorPanel.uxml", 
                $"{PathUtility.PanelsPath}/Panel.uss");
            var voxelEditor = new VoxelEditor(visualPanel, _eventHandler, _levelConstructor);
            _fsm.Add(voxelEditor);

            visualPanel = new VisualPanel(
                $"{PathUtility.PanelsPath}/SideEditorPanel.uxml",
                $"{PathUtility.PanelsPath}/Panel.uss");
            var sideEditor = new SideEditor(visualPanel, this);
            _fsm.Add(sideEditor);
            
            visualPanel = new VisualPanel(
                $"{PathUtility.PanelsPath}/NavMeshBakerPanel.uxml",
                $"{PathUtility.PanelsPath}/Panel.uss");
            var navMeshBaker = new NavMeshBaker(visualPanel);
            _fsm.Add(navMeshBaker);
        }

        private void CheckLevelSerializedProperty()
        {
            if (_levelSerializedProperty.objectReferenceValue == null)
            {
                _fsm.SetPause(true);
                if (_root.childCount > 1)
                {
                    _root.RemoveAt(1);
                }
            }
            else
            {
                _fsm.SetPause(false);
                if (_root.childCount < 2)
                {
                    _root.Add(_fsm.Root);
                }
            }
        }
        
        
        void OnDuringSceneGui(SceneView sceneView)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
    }
}
