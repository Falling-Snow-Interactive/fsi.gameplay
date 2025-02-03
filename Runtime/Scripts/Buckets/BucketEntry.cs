using System;
using UnityEngine;

namespace Fsi.Gameplay.Buckets
{
    [Serializable]
    public abstract class BucketEntry<T>
    {
        [SerializeField]
        private int weight = 1;
        public int Weight => weight;

        [SerializeField]
        private T value;
        public T Value => value;
    }
}