using UnityEditor;
using UnityEngine;

namespace Fsi.Gameplay.SceneManagement.Settings
{
	public class FsiSceneManagerSettings : ScriptableObject
	{
		private const string RESOURCE_PATH = "Settings/FsiSceneManagerSettings";
		private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";

		[Header("Debugging")]
		[SerializeField]
		private bool debugLog;

		public bool DebugLog => debugLog;

		public static FsiSceneManagerSettings GetOrCreateSettings()
		{
			var settings = Resources.Load<FsiSceneManagerSettings>(RESOURCE_PATH);

			#if UNITY_EDITOR
			if (!settings)
			{
				if (!AssetDatabase.IsValidFolder("Assets/Resources")) AssetDatabase.CreateFolder("Assets", "Resources");

				if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
					AssetDatabase.CreateFolder("Assets/Resources", "Settings");

				settings = CreateInstance<FsiSceneManagerSettings>();
				AssetDatabase.CreateAsset(settings, FULL_PATH);
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
	}
}