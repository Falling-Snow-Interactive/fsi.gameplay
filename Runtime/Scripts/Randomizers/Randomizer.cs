using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fsi.Gameplay.Randomizers
{
    public class Randomizer<TValue, TEntry>
        where TEntry : RandomizerEntry<TValue>
    {
        public List<TEntry> Entries { get; }
        public int TotalWeight { get; private set; }
        
        public Randomizer()
        {
            Entries = new List<TEntry>();
            TotalWeight = 0;
        }

        public void Add(TEntry value)
        {
            Entries.Add(value);
            TotalWeight += value.Weight;
        }

        public void Remove(TEntry value)
        {
            Entries.Remove(value);
            TotalWeight -= value.Weight;
        }

        public TValue Randomize()
        {
            int roll = Random.Range(0, TotalWeight);
            int weight = 0;
            foreach (var entry in Entries)
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
    }
}
