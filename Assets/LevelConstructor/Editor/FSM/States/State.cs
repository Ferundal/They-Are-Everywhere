using UnityEngine.UIElements;

namespace LevelConstructor.Editor.FSM.States
{
    public abstract class State
    {
        public VisualPanel Panel;

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
    }
}
