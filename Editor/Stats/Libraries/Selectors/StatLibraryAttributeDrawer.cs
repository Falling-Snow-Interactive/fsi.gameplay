using Fsi.DataSystem.Libraries;
using Fsi.Gameplay.Stats.Settings;
using UnityEditor;

namespace Fsi.Gameplay.Stats.Libraries.Selectors
{
    [CustomPropertyDrawer(typeof(StatLibraryAttribute))]
    public class StatLibraryAttributeDrawer : LibraryAttributeDrawer<string,StatData>
    {
        protected override Library<string,StatData> GetLibrary() => StatSettings.Stats;
    }
}