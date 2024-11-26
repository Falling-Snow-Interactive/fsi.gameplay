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

    private int cachedWeight = -1;

    public int TotalWeight
    {
        get
        {
            if (cachedWeight < 0)
            {
                cachedWeight = 0;
                foreach (var entry in Entries)
                {
                    cachedWeight += entry.Weight;
                }
            }

            return cachedWeight;
        }
    }

    public TValue GetRandom()
    {
        if (Entries.Count <= 0)
        {
            throw new IndexOutOfRangeException($"No entry in Bucket");
        }

        float roll = Random.Range(0, TotalWeight);
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
    }
}