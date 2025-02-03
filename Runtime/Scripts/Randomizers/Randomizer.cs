using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fsi.Gameplay.Randomizers
{
    public class Randomizer<TValue, TEntry>
        where TEntry : RandomizerEntry<TValue>
    {
        public virtual List<TEntry> Entries { get; }

        public int TotalWeight => GetWeight(Entries);
        
        public Randomizer()
        {
            Entries = new List<TEntry>();
        }

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
            var weight = 0;
            foreach (TEntry entry in entries)
            {
                weight += entry.Weight;
            }
            return weight;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public TValue Randomize(List<TEntry> entries, int totalWeight)
        {
            int roll = Random.Range(0, totalWeight);
            var weight = 0;
            foreach (TEntry entry in entries)
            {
                weight += entry.Weight;
                if (roll < weight)
                {
                    return entry.Value;
                }
            }

            Debug.LogError($"Randomizer {typeof(TValue).Name} is out of range. Roll: {roll} - Total: {TotalWeight}.");
            return default;
        }

        public TValue Randomize()
        {
            return Randomize(Entries, TotalWeight);
        }

        public List<TValue> Randomize(int amount, bool repeats)
        {
            if (repeats)
            {
                List<TValue> entries = new();
                for (var i = 0; i < amount; i++)
                {
                    entries.Add(Randomize());
                }
                return entries;
            }
            else
            {
                List<TValue> entries = new();
                for (var i = 0; i < amount; i++)
                {
                    var adjusted = new List<TEntry>();
                    foreach (TEntry e in Entries)
                    {
                        if (!entries.Contains(e.Value))
                        {
                            adjusted.Add(e);
                        }
                    }

                    if (adjusted.Count == 0)
                    {
                        return entries;
                    }
                    
                    int weight = GetWeight(adjusted);
                    TValue selected = Randomize(adjusted, weight);
                    entries.Add(selected);
                }

                return entries;
            }
        }
    }
}
