using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;

namespace LevelConstructor
{
    public class Level
    {
        public int size;
        public List<Shape> shapes = new();

        private LevelGeneration.Level _levelSO;
        private GameObject _rootGameObject;

        public Level(LevelGeneration.Level levelSO, GameObject rootGameObject)
        {
            _levelSO = levelSO;
            _rootGameObject = rootGameObject;
            
            CreateShapes(rootGameObject, levelSO);
        }

        public void AddVoxel(VoxelType voxelType, Vector3Int position, Shape shape)
        {
            
        }

        private void CreateShapes(GameObject rootGameObject, LevelGeneration.Level level)
        {
            foreach (var shape in level.shapes)
            {
                var newShape = new Shape(shape, rootGameObject);
                shapes.Add(newShape);
            }
        }
    }
}