using Fsi.Settings;
using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Gameplay.Stats.Settings
{
    public static class StatSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return SettingsEditorUtility.CreateSettingsProvider("Stats", "Falling Snow Interactive/Stats", OnActivate);
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject settingsProp = StatSettings.GetSerializedSettings();
            root.Add(SettingsEditorUtility.CreateSettingsPage( settingsProp, "Stats"));
        }
    }
}