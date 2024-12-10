using System.Collections.Generic;
using Fsi.Gameplay.SceneManagement.Settings;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Gameplay
{
    public class FsiSceneManagerSettingsProvider : SettingsProvider
    {
        private const string SETTINGS_PATH = "Fsi/SceneManager";
        
        private SerializedObject serializedSettings;
        
        public FsiSceneManagerSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new FsiSceneManagerSettingsProvider(SETTINGS_PATH, SettingsScope.Project);
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = FsiSceneManagerSettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("debugLog"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}
