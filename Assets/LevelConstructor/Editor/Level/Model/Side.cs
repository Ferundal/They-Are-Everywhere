using System.Collections.Generic;

namespace LevelConstructor
{
    public class Side
    {
        public VoxelModel Parent;
        public VoxelDirection Direction { get; private set; }
        public Surface Surface;

        public Side(VoxelDirection direction)
        {
            Direction = direction;
        }
    }
}