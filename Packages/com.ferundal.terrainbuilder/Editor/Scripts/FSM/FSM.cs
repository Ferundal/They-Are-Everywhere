using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class FSM
    {
        public State CurrentState { get; private set; } = null;
        public VisualElement Root { get; private set; } = new();

        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    CurrentState.OnEnter();
                }
                else
                {
                    CurrentState.OnExit();
                }

            }
        }

        private readonly VisualElement _navigationRoot = new();

        public FSM()
        {
            CreateNavigationPanel();
        }

        public void OnDestroy()
        {
            CurrentState?.OnExit();
        }

        public void Add(State newState)
        {
            _navigationRoot.Add(newState.Panel.NavigationButton);
            newState.Panel.NavigationButton.clicked += () => Transition(newState);

            if (CurrentState != null) return;
            
            CurrentState = newState;
            Root.Add(newState.Panel.Body);
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
            if (!IsActive) return;
            CurrentState?.OnSceneGUI();
        }

        private void CreateNavigationPanel()
        {
            _navigationRoot.style.flexDirection = FlexDirection.Row;
            Root.Add(_navigationRoot);
        }
    }
}