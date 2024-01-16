using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class FSM
    {
        public State CurrentState { get; private set; } = null;
        public VisualElement Root { get; private set; } = new();

        public StyleSheet StyleSheet;

        private readonly VisualElement _navigationRoot = new();
        private State _pausedState = null;

        public FSM()
        {
            CreateNavigationPanel();
        }

        public void OnDestroy()
        {
            CurrentState.OnExit();
        }

        public void Add(State newState)
        {
            _navigationRoot.Add(newState.Panel.NavigationButton);
            newState.Panel.NavigationButton.clicked += () => Transition(newState);

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
            Root.Add(CurrentState.Root);
            CurrentState.OnEnter();
        }

        public void OnSceneGUI()
        {
            CurrentState?.OnSceneGUI();
        }

        public void SetPause(bool isPauseOn)
        {
            if (isPauseOn && CurrentState != null)
            {
                _pausedState = CurrentState;
                CurrentState.OnExit();
                CurrentState = null;
                return;
            }

            if (!isPauseOn && _pausedState != null)
            {
                CurrentState = _pausedState;
                CurrentState.OnEnter();
                _pausedState = null;
                return;
            }
        }
        
        private void CreateNavigationPanel()
        {
            _navigationRoot.style.flexDirection = FlexDirection.Row;
            Root.Add(_navigationRoot);
        }
    }
}