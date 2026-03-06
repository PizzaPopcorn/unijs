using UnityEditor;
using UnityEngine;

namespace UniJS.Editor
{
    public class UniJSConfig : ScriptableObject
    {
        [Tooltip("Include the CDN in exported html after build.")]
        public bool includeCDN = true;
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
            //Debug.Log("<color=#4CAF50><b>UniJs:</b> Archivo de configuración listo en " + FullPath + "</color>");
        }
    }
}