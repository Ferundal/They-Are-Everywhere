using System.Collections.Generic;
using TerrainGeneration.ModelLevel;
using UnityEngine;

namespace Editor.Scripts.EditorLevel
{
    public class Level
    {
        public List<Shape> Shapes = new();
        public ThreeDimensionalMatrix<Voxel> VoxelMatrix = new ();

        private TerrainGeneration.ModelLevel.Level _levelSO;
        private GameObject _rootGameObject;
        private LevelConstructor _levelConstructor;

        public Level(TerrainGeneration.ModelLevel.Level levelSO, LevelConstructor levelConstructor)
        {
            _levelSO = levelSO;
            _rootGameObject = levelConstructor.gameObject;
            _levelConstructor = levelConstructor;
            
            foreach (var shape in _levelSO.shapes)
            {
                var newShape = Shape.Create(shape, levelConstructor, VoxelMatrix);
                Shapes.Add(newShape);
            }
        }

        public Shape AddShape(string shapeName)
        {
            var shapeSO = new TerrainGeneration.ModelLevel.Shape();
            shapeSO.shapeName = shapeName;
            shapeSO.ParentLevel = _levelSO;
            _levelSO.shapes.Add(shapeSO);
            
            var newShape = Shape.Create(shapeSO, _levelConstructor, VoxelMatrix);
            Shapes.Add(newShape);
            return newShape;
        }
    }
}