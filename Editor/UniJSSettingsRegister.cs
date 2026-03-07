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
                    
                    var includeCdnProp = so.FindProperty("includeCDN");
                    var includeControllerProp = so.FindProperty("includeControllerScript");
                    
                    EditorGUILayout.BeginHorizontal();
                    
                    GUILayout.Space(10); 
                    
                    EditorGUILayout.BeginVertical();
                    
                    EditorGUIUtility.labelWidth = 260;
                    
                    EditorGUILayout.PropertyField(includeCdnProp, new GUIContent("Include UniJS CDN in template"), GUILayout.ExpandWidth(true));
                    
                    if (includeCdnProp.boolValue)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.DelayedTextField(so.FindProperty("targetCDNVersion"), new GUIContent("Target CDN version"), GUILayout.ExpandWidth(true));
                        EditorGUI.indentLevel--;
                    }

                    EditorGUILayout.Space(10);
                    
                    EditorGUILayout.PropertyField(includeControllerProp, new GUIContent("Inject controller script in template"), GUILayout.ExpandWidth(true));

                    if (includeControllerProp.boolValue)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.DelayedTextField(so.FindProperty("controllerScriptName"), new GUIContent("Script name"), GUILayout.ExpandWidth(true));
                        EditorGUI.indentLevel--;
                    }
                    
                    EditorGUILayout.EndVertical();
    
                    GUILayout.Space(10);
                    EditorGUILayout.EndHorizontal();

                    if (so.ApplyModifiedProperties()) AssetDatabase.SaveAssets();
                },
                keywords = new System.Collections.Generic.HashSet<string>(new[] { "UniJs", "WebGL" })
            };
            return provider;
        }
    }
}