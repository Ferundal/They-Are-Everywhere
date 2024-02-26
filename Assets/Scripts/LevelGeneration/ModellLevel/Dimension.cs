using System.Collections.Generic;

namespace LevelGeneration
{
    public class Dimension<T> where T : new()
    {
        private readonly List<T> _store = new();
        private int _minIndex;

        public Dimension()
        {
            _minIndex = 0;
        }

        public T this[int index]
        {
            get
            {
                var storeIndex = index - _minIndex;
                return _store[storeIndex];
            }
            set
            {
                var storeIndex = index - _minIndex;
                if (storeIndex < 0)
                {
                    AddPrependElement(-storeIndex, value);
                    _minIndex = index;
                }
                else
                {
                    if (_store.Count <= storeIndex)
                    {
                        AddAppendElement(storeIndex, value);
                    }

                    _store[storeIndex] = value;
                }
            }
        }

        private void AddPrependElement(int prependSize, T element)
        {
            var prependElements = new List<T>(prependSize);
            prependElements.Add(element);
            for (int i = prependSize; i > 1; i--)
            {
                prependElements.Add(default(T));
            }
            _store.InsertRange(0, prependElements);
        }
        
        private void AddAppendElement(int appendStoreIndex, T element)
        {
            if (_store.Capacity <= appendStoreIndex)
            {
                _store.Capacity = appendStoreIndex + 1;
            }

            for (int i = _store.Count; i < appendStoreIndex; i++)
            {
                _store.Add(default(T));
            }
            _store.Add(element);
        }
    }
}