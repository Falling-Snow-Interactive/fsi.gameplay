using System;
using UnityEngine;

namespace Fsi.Gameplay.Buckets
{
	[Serializable]
	public abstract class BucketEntry<T> : ISerializationCallbackReceiver
	{
		[HideInInspector]
		[SerializeField]
		private string name;

		public abstract T Value { get; }
		public abstract int Weight { get; }

		public void OnBeforeSerialize()
		{
			name = ToString();
		}

		public void OnAfterDeserialize() { }

		public override string ToString()
		{
			string v = Value != null ? Value.ToString() : "No value";
			string w = Weight.ToString();
			return $"{v} - {w}";
		}
	}
}