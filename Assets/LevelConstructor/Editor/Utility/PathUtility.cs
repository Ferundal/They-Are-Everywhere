using UnityEditor;

namespace LevelConstructor
{
    public class PathUtility
    {
        public static string GetLevelConstructorFolder()
        {
            return GetPathToFolder("Assets", "LevelConstructor");
        }

        public static string GetPathToFolder(string initialRelativePath, string targetFolderName)
        {
            var folderPaths = AssetDatabase.GetSubFolders(initialRelativePath);

            foreach (var folderPath in folderPaths)
            {
                if (folderPath.EndsWith(targetFolderName))
                {
                    return folderPath;
                }
            }

            return null;
        }
        
        public static string PanelsPath => $"{PathUtility.GetLevelConstructorFolder()}/Editor/Panels";

        public static string VoxelPrefabsPath => $"{PathUtility.GetLevelConstructorFolder()}/Editor/Prefabs";
    }
}