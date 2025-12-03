using Fsi.Gameplay.Stats.Libraries;
using UnityEditor;
using UnityEngine;

namespace Fsi.Gameplay.Stats.Settings
{
    public class StatSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/StatSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static StatSettings settings;
        public static StatSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Libraries")]

        [SerializeField]
        private StatLibrary stats;
        public static StatLibrary Stats => Settings.stats;

        #region Settings

        private static StatSettings GetOrCreateSettings()
        {
            StatSettings s = Resources.Load<StatSettings>(ResourcePath);

            #if UNITY_EDITOR
            if (!s)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Settings");
                }

                s = CreateInstance<StatSettings>();
                AssetDatabase.CreateAsset(s, FullPath);
                AssetDatabase.SaveAssets();
            }
            #endif

            return s;
        }

        #if UNITY_EDITOR
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        #endif

        #endregion
    }
}