using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class VisualPanel
    {
        public Button NavigationButton { get; private set; }
        public VisualElement Body { get; private set; }

        public VisualPanel(string visualTreeAssetPath, string stylesheetPath)
        {
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(visualTreeAssetPath);
            var instantiatedTemplate = visualTreeAsset.Instantiate();

            NavigationButton = instantiatedTemplate.Q<Button>("navigation-button");
            Body = instantiatedTemplate.Q<VisualElement>("body");
            
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(stylesheetPath);
            NavigationButton.styleSheets.Add(stylesheet);
            Body.styleSheets.Add(stylesheet);
        }
    }
}