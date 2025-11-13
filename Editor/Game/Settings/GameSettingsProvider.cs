using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Gameplay.Game.Settings
{
    public static class GameSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fsi/Game", SettingsScope.Project)
            {
                label = "Game",
                activateHandler = OnActivate,
            };
        
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            root.style.marginTop = 5;
            root.style.marginRight = 5;
            root.style.marginLeft = 5;
            root.style.marginBottom = 5;
    
            SerializedObject settingsProp = GameSettings.GetSerializedSettings();
        
            Label title = new("Game Settings");
            root.Add(title);
        
            root.Add(new Spacer());
        
            root.Add(new InspectorElement(settingsProp));
        
            root.Bind(settingsProp);
        }
    }
}