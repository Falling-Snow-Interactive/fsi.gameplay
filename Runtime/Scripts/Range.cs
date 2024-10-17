using System;
using UnityEngine;

namespace Fsi.Gameplay
{
    public abstract class Range<T>
    {
        public abstract T Min { get; }
        public abstract T Max { get; }

        public abstract T Random();
    }

    [Serializable]
    public class FloatRange : Range<float>
    {
        [SerializeField]
        private float min = 0;

        [SerializeField]
        private float max = 10;

        public override float Min => min;
        public override float Max => max;

        public override float Random()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
    
    [Serializable]
    public class IntRange : Range<int>
    {
        [SerializeField]
        private int min = 0;

        [SerializeField]
        private int max = 10;
        
        public override int Min => min;
        public override int Max => max;
        
        public override int Random()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
    
    [Serializable]
    public class Vector3Range : Range<Vector3>
    {
        [SerializeField]
        private Vector3 min = Vector3.zero;

        [SerializeField]
        private Vector3 max = Vector3.one;
        
        public override Vector3 Min => min;
        public override Vector3 Max => max;
        
        public override Vector3 Random()
        {
            float x = UnityEngine.Random.Range(Min.x, Max.x);
            float y = UnityEngine.Random.Range(Min.y, Max.y);
            float z = UnityEngine.Random.Range(Min.z, Max.z);
            return new Vector3(x, y, z);
        }
    }
}
