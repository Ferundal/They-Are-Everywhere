using UnityEngine.UIElements;

namespace LevelConstructor
{
    public abstract class State
    {
        public VisualPanel Panel;
        public virtual VisualElement Root { get; protected set; } = new();

        public State(VisualPanel panel)
        {
            Panel = panel;
        }

        public virtual void OnEnter()
        {
            
        }
        
        public virtual void OnExit()
        {
            
        }
        
        public virtual void OnSceneGUI()
        {
            
        }
    }
}
