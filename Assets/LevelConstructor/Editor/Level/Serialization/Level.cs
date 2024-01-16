using System.Collections.Generic;
using UnityEngine;

namespace LevelConstructor
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level/New Level")]
    public class Level : ScriptableObject
    {
        public int size;
        public List<Voxel> voxels = new List<Voxel>();
    }
}