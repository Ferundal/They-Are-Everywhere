using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace LevelConstructor
{
    [ExecuteInEditMode]
    public class LevelConstructor : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] public LevelGeneration.Level levelSO;
        [HideInInspector] public Level EditorLevel;
        [HideInInspector] public bool IsReload { get; private set; } = false;

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
            IsReload = true;
            foreach (Transform child in transform) {
                DestroyImmediate(child.gameObject);
            }

            IsReload = false;
            if (levelSO == null) return;
            levelSO.Initialize();
            EditorLevel = new Level(levelSO, this);
        }
    }
}
