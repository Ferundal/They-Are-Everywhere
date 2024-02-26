using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    /// <summary>
    /// Unity Editor tools for level construction
    /// </summary>
    [CustomEditor(typeof(LevelConstructor))]
    public class LevelConstructorEditor : Editor
    {
        public LevelConstructor _levelConstructor;

        private SerializedProperty _levelSerializedProperty;
        private PropertyField _levelPropertyField;

        private VisualElement _root;
        private FSM _fsm;

        private EventHandler _eventHandler;
        
        /// <summary>
        /// Initialization of objects independent from the parent script
        /// </summary>
        private void OnEnable()
        {
            _levelConstructor = target as LevelConstructor;
            
            _root = new();
            _levelSerializedProperty = serializedObject.FindProperty("levelSO");
            _levelPropertyField = new PropertyField(_levelSerializedProperty);
            _root.Add(_levelPropertyField);
            
            _eventHandler = _levelConstructor.Handler;
            _fsm = new FSM();
            SceneView.duringSceneGui += OnDuringSceneGui;
        }
        
        /// <summary>
        /// Initialization of objects dependent from the parent script
        /// </summary>
        /// <remarks>
        /// Some FSM states depend on the EditorLevel field in the parent script, so we ensure their deferred initialization in this method.
        /// </remarks>
        public override VisualElement CreateInspectorGUI()
        { 
            CreateFSMStates();
            
            // CheckLevelSerializedProperty method use EditorLevel field in the parent script so we can't subscribe it early
            _eventHandler.OnAfterDeserialize += CheckLevelSerializedProperty;
            _eventHandler.OnGUIStart += CheckLevelSerializedProperty;
            
            // Triggers "OnGUIStart" event here
            _eventHandler.HasUnprocessedGUIStart = true;
            return _root;
        }
        
        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnDuringSceneGui;
            _eventHandler.OnAfterDeserialize -= CheckLevelSerializedProperty;
            _eventHandler.OnGUIStart -= CheckLevelSerializedProperty;
            _fsm.OnDestroy();
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
            var voxelEditor = new VoxelEditor(visualPanel, _levelConstructor);
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

        /// <summary>
        /// The method responsible for toggling the level editor interface based on the presence of a reference to the level object
        /// </summary>
        private void CheckLevelSerializedProperty()
        {
            if (_levelSerializedProperty.objectReferenceValue == null)
            {
                _fsm.IsActive = false;
                if (_root.childCount > 1)
                {
                    _root.RemoveAt(1);
                }
            }
            else
            {
                
                _fsm.IsActive = true;
                if (_root.childCount < 2)
                {
                    _root.Add(_fsm.Root);
                }
            }
        }
        
        /// <summary>
        /// Disables the standard Unity editor controls
        /// </summary>
        void OnDuringSceneGui(SceneView sceneView)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
    }
}
