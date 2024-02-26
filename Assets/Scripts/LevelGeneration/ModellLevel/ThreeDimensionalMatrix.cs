using System;
using UnityEngine;

namespace LevelGeneration
{
    
    // TODO replace exception
    public class ThreeDimensionalMatrix<T> where T : new()
    {
        private Dimension<Dimension<Dimension<T>>> _store = new();

        public T this[int xIndex, int yIndex, int zIndex]
        {
            get => GetElement(xIndex, yIndex, zIndex);
            set => SetElement(xIndex, yIndex, zIndex, value);
        }

        public T this[Vector3Int index]
        {
            get => GetElement(index.x, index.y, index.z);
            set => SetElement(index.x, index.y, index.z, value);
        }

        private T GetElement(int xIndex, int yIndex, int zIndex)
        {
            try
            {
                return _store[xIndex][yIndex][zIndex];
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException or NullReferenceException)
            {
                return default(T);
            }
        }
        

        private void SetElement(int xIndex, int yIndex, int zIndex, T value)
        {
            Dimension<Dimension<T>> yDimension;
            try
            {
                yDimension = _store[xIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                yDimension = null;
            }

            if (yDimension == null)
            {
                _store[xIndex] = new Dimension<Dimension<T>>();
            }

            Dimension<T> zDimension;
            try
            {
                zDimension = _store[xIndex][yIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                zDimension = null;
            }

            if (zDimension == null)
            {
                _store[xIndex][yIndex] = new();
            }
            
            _store[xIndex][yIndex][zIndex] = value;

        }
    }
}