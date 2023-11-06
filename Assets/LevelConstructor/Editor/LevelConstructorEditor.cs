using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    [CustomEditor(typeof(LevelConstructor))]
    public class LevelConstructorEditor : UnityEditor.Editor
    {
        public LevelConstructor _levelConstructor;
        
        public SerializedProperty levelScriptableObject;
        public PropertyField levelPropertyField;

        private FSM _fsm;
        private State _initialState;
        
        private EventHandler _eventHandler;
        private void OnEnable()
        {
            _levelConstructor = target as LevelConstructor;
            _eventHandler = _levelConstructor.Handler;
            _fsm = new FSM();
            SceneView.duringSceneGui += OnDuringSceneGui;
            _eventHandler.OnGUIStart += SetInitialState;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnDuringSceneGui;
        }

        public override VisualElement CreateInspectorGUI()
        {
            levelScriptableObject = serializedObject.FindProperty("level");
            levelPropertyField = new PropertyField(levelScriptableObject);
            CreateFSMStates();
            _eventHandler.HasUnprocessedGUIStart = true;
            return _fsm.Root;
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
                $"{PathUtility.PanelsPath}/PaintPanel.uxml", 
                $"{PathUtility.PanelsPath}/Panel.uss");
            var paintState = new Paint(visualPanel, _eventHandler, _levelConstructor);
            _initialState = paintState;
            _fsm.Add(paintState);

            visualPanel = new VisualPanel(
                $"{PathUtility.PanelsPath}/BakePanel.uxml",
                $"{PathUtility.PanelsPath}/Panel.uss");
            var bakeState = new Bake(visualPanel, this);
            _fsm.Add(bakeState);
            
            _fsm.Transition(bakeState);
        }

        void SetInitialState()
        {
            _eventHandler.OnGUIStart -= SetInitialState;
            _fsm.Transition(_initialState);
        }
        
        void OnDuringSceneGui(SceneView sceneView)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
    }
}
