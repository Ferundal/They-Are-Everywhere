using System;
using System.Collections.Generic;

namespace LevelGeneration
{
    [Serializable]
    public class Shape
    {
        [NonSerialized] public Level ParentLevel;
        
        public string shapeName;
        public List<Voxel> voxels;
    }
}