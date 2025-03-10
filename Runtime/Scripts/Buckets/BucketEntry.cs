using System;
using System.Runtime.Serialization;
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
        public int Weight => weight;

        [SerializeField]
        private T value;
        public T Value => value;
        
        public void OnBeforeSerialize()
        {
            name = $"{value} - {weight}";
        }

        public void OnAfterDeserialize() { }
    }
}