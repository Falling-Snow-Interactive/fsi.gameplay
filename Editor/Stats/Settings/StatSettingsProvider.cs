using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Spacer = Fsi.Ui.Dividers.Spacer;

namespace Fsi.Gameplay.Stats.Settings
{
    public static class StatSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Falling Snow Interactive/Stats", SettingsScope.Project)
                                        {
                                            label = "Stats",
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
        
            ScrollView scroll = new();
            root.Add(scroll);
    
            SerializedObject settingsProp = StatSettings.GetSerializedSettings();
        
            Label title = new("Stat Settings");
            scroll.Add(title);
            scroll.Add(new Spacer());
            scroll.Add(new InspectorElement(settingsProp));
            scroll.Bind(settingsProp);
        }
    }
}