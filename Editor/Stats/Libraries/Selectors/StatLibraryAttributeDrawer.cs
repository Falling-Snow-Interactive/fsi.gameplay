using System.Collections.Generic;
using Fsi.DataSystem.Selectors;
using Fsi.Gameplay.Stats.Settings;
using UnityEditor;

namespace Fsi.Gameplay.Stats.Libraries.Selectors
{
    [CustomPropertyDrawer(typeof(StatLibraryAttribute))]
    public class StatLibraryAttributeDrawer : SelectorAttributeDrawer<string,StatData>
    {
        protected override List<StatData> GetEntries() => StatSettings.Stats.Entries;
    }
}