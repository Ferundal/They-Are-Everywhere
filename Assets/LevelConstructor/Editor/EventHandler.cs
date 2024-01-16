using System;
using UnityEngine;

namespace LevelConstructor
{
    public class EventHandler
    {
        public Action<Event> OnMouseUp;
        public Action<Event> OnMouseDown;
        public Action<Event> OnMouseMove;
        
        public bool HasUnprocessedSerialization = false;
        public Action OnBeforeSerialize;
        
        public bool HasUnprocessedDeserialization = false;
        public Action OnAfterDeserialize;
        
        public bool HasUnprocessedGUIStart = true;
        public Action OnGUIStart;

        public void ProcessEvent(Event currentEvent)
        {
            switch (currentEvent.type)
            {
                case EventType.MouseUp:
                    OnMouseUp?.Invoke(currentEvent);
                    break;
                case EventType.MouseDown:
                    OnMouseDown?.Invoke(currentEvent);
                    break;
                case EventType.MouseMove:
                    OnMouseMove?.Invoke(currentEvent);
                    break;
                case EventType.Layout:
                    CheckAndInvokeSubEvents();
                    break;
            }
        }

        private void CheckAndInvokeSubEvents()
        {
            if (HasUnprocessedGUIStart)
            {
                HasUnprocessedGUIStart = false;
                OnGUIStart?.Invoke();
            }

            if (HasUnprocessedDeserialization)
            {
                HasUnprocessedDeserialization = false;
                OnAfterDeserialize?.Invoke();
            }
            
            if (HasUnprocessedSerialization)
            {
                HasUnprocessedSerialization = false;
                OnBeforeSerialize?.Invoke();
            }
        }
    }
}