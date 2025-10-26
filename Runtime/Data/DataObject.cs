using Fsi.Localization;
using UnityEngine;

namespace Fsi.Gameplay.Data
{
	public class DataObject<TId> : ScriptableObject
	{
		[SerializeField]
		private TId id;
		public TId Id => id;

		[SerializeField]
		private LocEntry locName;
		public string Name => locName.GetLocalizedString("no_name");

		[SerializeField]
		private LocEntry locDescription;
		public string Description => locDescription.GetLocalizedString("no_desc");

		public override string ToString()
		{
			return Name;
		}
	}
}