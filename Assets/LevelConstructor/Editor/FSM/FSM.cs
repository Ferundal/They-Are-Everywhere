using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class FSM
    {
        public State CurrentState { get; private set; }
        public VisualElement Root { get; private set; }

        public StyleSheet StyleSheet;

        private VisualElement _NavigationRoot;

        public FSM()
        {
            Root = new VisualElement();
            CreateNavigationPanel();
        }

        public void Add(State newState)
        {
            _NavigationRoot.Add(newState.Panel.NavigationButton);
            if (CurrentState == null)
            {
                CurrentState = newState;
                Root.Add(newState.Panel.Body);
                CurrentState.OnEnter();
            }
        }

        public void Transition(State state)
        {
            CurrentState?.OnExit();
            CurrentState = state;
            Root.RemoveAt(1);
            Root.Add(CurrentState.Panel.Body);
            CurrentState.OnEnter();
        }

        public void OnSceneGUI()
        {
            CurrentState.OnSceneGUI();
        }

        private void CreateNavigationPanel()
        {
            _NavigationRoot = new VisualElement();
            Root.Add(_NavigationRoot);
        }
    }
}