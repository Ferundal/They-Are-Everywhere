using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class Bake : State
    {
        private LevelConstructorEditor _levelConstructorEditor;
        private Button _button;
        private bool _isBaking = false;
        private ProgressBar _progressBar;

        public override VisualElement Root { get; protected set; } = new();
        
        private string ProgressBarText {
            get
            {
                return _progressBar.title;
            }
            set
            {
                _progressBar.title = value;
            }
        }

        public Bake(VisualPanel panel, LevelConstructorEditor levelConstructorEditor) : base(panel)
        {
            _levelConstructorEditor = levelConstructorEditor;

            _button = Panel.Body.Q<Button>("bake");
            _button.clicked += () => StartBake();
            
            _progressBar = Panel.Body.Q<ProgressBar>("progress_bar");
            
            AssembleBodyPanel();
        }

        public override void OnEnter()
        {
            _levelConstructorEditor._levelConstructor.Handler.OnAfterDeserialize += OnAfterDeserialize;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _levelConstructorEditor._levelConstructor.Handler.OnAfterDeserialize -= OnAfterDeserialize;
            base.OnExit();
        }

        private void StartBake()
        {
            _isBaking = true;
            Root.RemoveAt(1);
            AssembleOptionalBakingStatusPanels();
        }

        private void AssembleBodyPanel()
        {
            Root.Add(_levelConstructorEditor.levelPropertyField);
            AssembleOptionalPanels();
        }

        private void AssembleOptionalPanels()
        {
            if (_levelConstructorEditor.levelScriptableObject.objectReferenceValue == null) return;
            
            AssembleOptionalBakingStatusPanels();
        }

        private void AssembleOptionalBakingStatusPanels()
        {
            if (_isBaking) 
            {
                Root.Add(_progressBar);
            }
            else
            {
                Root.Add(_button);
            }
        }

        private void OnAfterDeserialize()
        {
            if (Root.childCount > 1)
            {
                Root.RemoveAt(1);
            }
            AssembleOptionalPanels();
        }
    }
}