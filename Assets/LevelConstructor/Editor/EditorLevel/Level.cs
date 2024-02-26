using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;

namespace LevelConstructor
{
    public class Level
    {
        public List<Shape> Shapes = new();
        public ThreeDimensionalMatrix<Voxel> VoxelMatrix = new ();

        private LevelGeneration.Level _levelSO;
        private GameObject _rootGameObject;
        private LevelConstructor _levelConstructor;

        public Level(LevelGeneration.Level levelSO, LevelConstructor levelConstructor)
        {
            _levelSO = levelSO;
            _rootGameObject = levelConstructor.gameObject;
            _levelConstructor = levelConstructor;
            
            CreateShapes(levelConstructor, levelSO);
        }

        public Shape AddShape(string shapeName)
        {
            var shapeSO = new LevelGeneration.Shape();
            shapeSO.shapeName = shapeName;
            shapeSO.ParentLevel = _levelSO;
            _levelSO.shapes.Add(shapeSO);
            
            var newShape = Shape.Create(shapeSO, _rootGameObject, _levelConstructor);
            Shapes.Add(newShape);
            return newShape;
        }

        private void CreateShapes(LevelConstructor levelConstructor, LevelGeneration.Level level)
        {
            foreach (var shape in level.shapes)
            {
                var newShape = Shape.Create(shape, levelConstructor.gameObject, levelConstructor);
                Shapes.Add(newShape);
            }
        }
    }
}