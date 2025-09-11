using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fsi.Gameplay.Randomizers
{
	public class Randomizer<TValue, TEntry>
		where TEntry : RandomizerEntry<TValue>
	{
		public Randomizer()
		{
			Entries = new List<TEntry>();
		}

		public virtual List<TEntry> Entries { get; }

		public int TotalWeight => GetWeight(Entries);

		public void Add(TEntry value)
		{
			Entries.Add(value);
		}

		public void Remove(TEntry value)
		{
			Entries.Remove(value);
		}

		public int GetWeight(List<TEntry> entries)
		{
			int weight = 0;
			foreach (TEntry entry in entries) weight += entry.Weight;
			return weight;
		}
		
		// ReSharper disable Unity.PerformanceAnalysis
		public TValue Randomize(List<TEntry> entries, int totalWeight)
		{
			if (entries.Count == 0 || totalWeight == 0)
			{
				Debug.LogWarning("There is nothing to randomize.");
				return default(TValue);
			}
			int roll = Random.Range(0, totalWeight);
			int weight = 0;
			foreach (TEntry entry in entries)
			{
				weight += entry.Weight;
				if (roll < weight) return entry.Value;
			}

			Debug.LogError($"Randomizer {typeof(TValue).Name} is out of range. Roll: {roll} - Total: {TotalWeight}.");
			return default;
		}

		public TValue Randomize()
		{
			if (Entries.Count == 0 || TotalWeight == 0)
			{
				return default(TValue);
			}
			return Randomize(Entries, TotalWeight);
		}

		public List<TValue> Randomize(int amount, bool repeats)
		{
			if (Entries.Count == 0 || TotalWeight == 0　|| amount == 0)
			{
				return new();
			}
			
			if (repeats)
			{
				List<TValue> entries = new();
				for (int i = 0; i < amount; i++) entries.Add(Randomize());
				return entries;
			}
			else
			{
				List<TValue> entries = new();
				for (int i = 0; i < amount; i++)
				{
					var adjusted = new List<TEntry>();
					foreach (TEntry e in Entries)
						if (!entries.Contains(e.Value))
							adjusted.Add(e);

					if (adjusted.Count == 0) return entries;

					int weight = GetWeight(adjusted);
					TValue selected = Randomize(adjusted, weight);
					entries.Add(selected);
				}

				return entries;
			}
		}
	}
}