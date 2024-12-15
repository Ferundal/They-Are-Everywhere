using Editor.Scripts.EditorLevel;
using UnityEngine;

namespace Editor.Scripts
{
    [ExecuteInEditMode]
    public class LevelConstructor : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] public TerrainGeneration.ModelLevel.Level levelSO;
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
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
            if (levelSO == null) return;

            levelSO.Initialize();
            EditorLevel = new Level(levelSO, this);
        }
    }
}
