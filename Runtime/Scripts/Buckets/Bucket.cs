using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fsi.Gameplay.Buckets
{
    public abstract class Bucket<TEntry, TValue> : ScriptableObject
        where TEntry : BucketEntry<TValue> 
    {
        public abstract List<TEntry> Entries { get; }
        
        public int GetWeight()
        {
            int weight = 0;
            foreach (TEntry entry in Entries)
            {
                weight += entry.Weight;
            }

            return weight;
        }

        public TValue GetRandom()
        {
            if (Entries.Count <= 0)
            {
                throw new IndexOutOfRangeException($"No entry in Bucket");
            }

            float roll = Random.Range(0, GetWeight());
            foreach (var entry in Entries)
            {
                roll -= entry.Weight;
                if (roll < 0)
                {
                    return entry.Value;
                }
            }

            return Entries[^1].Value;
        }

        public List<TValue> GetRandom(int count, bool repeats = true)
        {
            List<TValue> result = new();
            for (int i = 0; i < count; i++)
            {
                TValue rand = GetRandom();
                switch (repeats)
                {
                    case false when !result.Contains(rand):
                    case true:
                        result.Add(GetRandom());
                        break;
                }
            }

            return result;
        }
    }
}