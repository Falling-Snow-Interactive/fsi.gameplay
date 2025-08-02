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

		[SerializeField]
		private int weight = 1;

		[SerializeField]
		private T value;
		public int Weight => weight;
		public T Value => value;

		public void OnBeforeSerialize()
		{
			name = $"{value} - {weight}";
		}

		public void OnAfterDeserialize()
		{
		}
	}
}