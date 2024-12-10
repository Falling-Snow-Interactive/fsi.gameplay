using Fsi.Gameplay.SceneManagement.Settings;

namespace Fsi.Gameplay.SceneManagement
{
    public static class FsiSceneManagerUtility
    {
        private static FsiSceneManagerSettings _settings;
        public static FsiSceneManagerSettings Settings => _settings ??= FsiSceneManagerSettings.GetOrCreateSettings();
    }
}