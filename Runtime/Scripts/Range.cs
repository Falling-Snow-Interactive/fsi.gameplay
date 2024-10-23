using System;
using UnityEngine;

namespace Fsi.Gameplay
{
    [Serializable]
    public abstract class Range<T>
    {
        [SerializeField]
        private T min;

        public T Min => min;

        [SerializeField]
        private T max;

        public T Max => max;

        protected Range()
        {
            min = max = default;
        }
    
        protected Range(T min, T max)
        {
            this.min = min;
            this.max = max;
        }

        public abstract T Random();
    }

    [Serializable]
    public class RangeInt : Range<int>
    {
        public RangeInt(int min, int max) : base(min, max) { }

        public override int Random()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }

    [Serializable]
    public class RangeFloat : Range<float>
    {
        public RangeFloat(float min, float max) : base(min, max) { }

        public override float Random()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
    
    [Serializable]
    public class RangeVector3 : Range<Vector3>
    {
        public RangeVector3(Vector3 min, Vector3 max) : base(min, max) { }

        public override Vector3 Random()
        {
            float x = UnityEngine.Random.Range(Min.x, Max.x);
            float y = UnityEngine.Random.Range(Min.y, Max.y);
            float z = UnityEngine.Random.Range(Min.z, Max.z);
            return new Vector3(x, y, z);
        }
    }
}
