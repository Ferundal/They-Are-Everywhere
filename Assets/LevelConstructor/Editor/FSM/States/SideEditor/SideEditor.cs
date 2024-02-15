using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class SideEditor : State
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

        public SideEditor(VisualPanel panel, LevelConstructorEditor levelConstructorEditor) : base(panel)
        {
            _levelConstructorEditor = levelConstructorEditor;

            _button = Panel.Body.Q<Button>("create_sides");
            _button.clicked += () => CreateSides();
            
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

        private void CreateSides()
        {
            _isBaking = true;
            Root.RemoveAt(0);
            AssembleOptionalBakingStatusPanels();
        }
        
        

        private void AssembleBodyPanel()
        {
            AssembleOptionalPanels();
        }

        private void AssembleOptionalPanels()
        {
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