using Fsi.Settings;
using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Gameplay.SceneManagement.Settings
{
	public static class FsiSceneManagerSettingsProvider
	{
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			return SettingsEditorUtility.CreateSettingsProvider("Scene Manager", "Falling Snow Interactive/Scene Manager",
			                                                    OnActivate);
		}

		private static void OnActivate(string searchContext, VisualElement root)
		{
			SerializedObject settingsProp = FsiSceneManagerSettings.GetSerializedSettings();
			root.Add(SettingsEditorUtility.CreateSettingsPage(settingsProp, "Scene Manager"));
		}
	}
}