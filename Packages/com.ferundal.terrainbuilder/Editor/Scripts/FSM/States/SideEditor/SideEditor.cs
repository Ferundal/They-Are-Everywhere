using Editor.Scripts.EditorLevel;
using LevelConstructor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Scripts.FSM.States.SideEditor
{
    public class SideEditor : State
    {
        private LevelConstructorEditor _levelConstructorEditor;

        public override VisualElement Root { get; protected set; } = new();

        public SideEditor(VisualPanel panel, LevelConstructorEditor levelConstructorEditor) : base(panel)
        {
            _levelConstructorEditor = levelConstructorEditor;
        }

        public override void OnEnter()
        {
            _levelConstructorEditor._levelConstructor.Handler.OnMouseDown += DestroySide;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _levelConstructorEditor._levelConstructor.Handler.OnMouseDown -= DestroySide;
            base.OnExit();
        }
        

        private void DestroySide(Event currentEvent)
        {
            var ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit)
                && raycastHit.collider.gameObject.TryGetComponent<Side>(out var side))
            {
                side.Destroy();
            }
        }
    }
}