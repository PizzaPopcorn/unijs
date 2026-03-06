using UnityEditor;
using UnityEngine;

namespace UniJS.Editor
{
    static class UniJSSettingsRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateUniJsSettingsProvider()
        {
            var provider = new SettingsProvider("Project/UniJS", SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    var settings = UniJSConfig.GetOrCreateSettings();
                    var so = new SerializedObject(settings);
                    
                    EditorGUILayout.PropertyField(so.FindProperty("includeCDN"), new GUIContent("Include UniJS CDN in exported html after build"));

                    if (so.ApplyModifiedProperties()) AssetDatabase.SaveAssets();
                },
                keywords = new System.Collections.Generic.HashSet<string>(new[] { "UniJs", "WebGL" })
            };
            return provider;
        }
    }
}