using System;
using UnityEngine;

namespace LevelConstructor
{
    public class EventHandler
    {
        public Action<Event> OnMouseUp;
        public Action<Event> OnMouseDown;

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
            }
        }
    }
}