using System;
using UnityEngine;


namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class LevelConstructor : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] private LevelGeneration.Level levelSO;
        [HideInInspector] public Level EditorLevel;

        public EventHandler Handler { get; } = new();

        private void OnEnable()
        {
            Handler.OnAfterDeserialize += RebuildEditorLevel;
        }

        private void OnDisable()
        {
            Handler.OnAfterDeserialize -= RebuildEditorLevel;
        }


        public void OnBeforeSerialize()
        {
            Handler.HasUnprocessedSerialization = true;
        }

        public void OnAfterDeserialize()
        {
            Handler.HasUnprocessedDeserialization = true;
        }

        private void RebuildEditorLevel()
        {
            foreach (Transform child in transform) {
                DestroyImmediate(child.gameObject);
            }
            if (levelSO == null) return;
            
            EditorLevel = new Level(levelSO, gameObject);
        }
    }
}
