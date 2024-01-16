using System.Collections.Generic;
using UnityEditorInternal;

namespace LevelConstructor
{
    public class Surface
    {
        private HashSet<Side> _sides = new();

        public void AddSide(Side side)
        {
            _sides.Add(side);
            side.Surface = this;
        }
    }
}