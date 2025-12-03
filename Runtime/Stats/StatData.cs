using Fsi.DataSystem;
using UnityEngine;

namespace Fsi.Gameplay.Stats
{
    [CreateAssetMenu(menuName = Menu, fileName = "New Stat")]
    public class StatData : ScriptableData<string>
    {
        private new const string Menu = ScriptableData<string>.Menu + "Stat";
    }
}