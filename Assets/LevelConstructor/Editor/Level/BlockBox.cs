using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelConstructor.Editor.Level
{
    [Serializable]
    public class BlockBox
    {
        [SerializeField] private List<Voxel> blocks;

        public BlockBox()
        {
            blocks = new List<Voxel>();
        }

        public void Add(Voxel newVoxel)
        {

        }
    }
}