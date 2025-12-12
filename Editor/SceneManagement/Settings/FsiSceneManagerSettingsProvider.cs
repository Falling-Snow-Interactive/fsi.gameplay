using Fsi.Settings;
using UnityEditor;

namespace Fsi.Gameplay.SceneManagement.Settings
{
	public static class FsiSceneManagerSettingsProvider
	{
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			SerializedObject settingsProp = FsiSceneManagerSettings.GetSerializedSettings();
			return SettingsEditorUtility.CreateSettingsProvider<FsiSceneManagerSettings>("Scene Manager", 
			                                                                            "Falling Snow Interactive/Scene Manager",
			                                                                            settingsProp);
		}
	}
}