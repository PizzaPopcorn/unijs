using UnityEditor;
using UnityEngine;

namespace UniJS.Editor
{
    public class UniJSConfig : ScriptableObject
    {
        [Tooltip("If true, the UniJS library will be injected in the exported index.html using a CDN.")]
        public bool includeCDN = true;
        [Tooltip("The version of the CDN to use. Leave empty to use the latest version.")]
        public string targetCDNVersion = "";
        [Tooltip("If true, an additional script tag will be injected in the exported index.html. A script with that name can be created by the user and used to control the Unity instance.")]
        public bool includeControllerScript = true;
        [Tooltip("The name of the script that will be injected, don't include the \".js\" extension. Will use \"unityController\" if empty.")]
        public string controllerScriptName = "unityController";
        
        private const string FolderPath = "UniJS/Editor"; //Inside Assets folder
        private const string FileName = "UniJS_config.asset";
        private const string FullPath = "Assets/" + FolderPath + "/" + FileName;

        public static UniJSConfig GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<UniJSConfig>(FullPath);
            if (settings == null)
            {
                settings = CreateInstance<UniJSConfig>();
                if (!AssetDatabase.IsValidFolder(FolderPath))
                {
                    var dirs = FolderPath.Split('/');
                    var subPath = "Assets";
                    foreach (var dir in dirs)
                    {
                        if(!AssetDatabase.IsValidFolder($"{subPath}/{dir}"))
                            AssetDatabase.CreateFolder(subPath, dir);
                        subPath = $"{subPath}/{dir}";
                    }
                }
                AssetDatabase.CreateAsset(settings, FullPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }
        
        [MenuItem("UniJS/Create or Focus Settings")]
        public static void CreateSettingsManually()
        {
            var settings = GetOrCreateSettings();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = settings;
            Debug.Log("<color=#4CAF50><b>[UniJS]</b> Focused config file at " + FullPath + "</color>");
        }
    }
}