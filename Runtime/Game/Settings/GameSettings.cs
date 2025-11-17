using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fsi.Gameplay.Game.Settings
{
    public class GameSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/GameSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static GameSettings settings;
        public static GameSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Launch")]

        [SerializeField]
        private List<GameObject> launchObjects;

        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameLaunch()
        {
            foreach (GameObject obj in Settings.launchObjects)
            {
                Instantiate(obj);
            }
        }
    
        #region Settings

        private static GameSettings GetOrCreateSettings()
        {
            GameSettings settings = Resources.Load<GameSettings>(ResourcePath);

            #if UNITY_EDITOR
            if (!settings)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Settings");
                }

                settings = CreateInstance<GameSettings>();
                AssetDatabase.CreateAsset(settings, FullPath);
                AssetDatabase.SaveAssets();
            }
            #endif

            return settings;
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