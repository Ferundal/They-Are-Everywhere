
using LevelConstructor.Editor;
using LevelConstructor.Editor.Level;
using LevelConstructor.Editor.Level.Serialization;
using UnityEngine;

namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class LevelConstructor : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] private SerializedLevel serializedLevel;

        private Level _level;
        

        private void Update()
        {
            
        }

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            if (serializedLevel == null) return;
            _level = serializedLevel.Deserialize();
            _level.OnChanged += SaveLevel;
        }

        private void SaveLevel()
        {
            serializedLevel.Serialize(_level);
        }
    }
}
