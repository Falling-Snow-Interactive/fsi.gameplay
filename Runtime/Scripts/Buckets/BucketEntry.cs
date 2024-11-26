namespace Fsi.Gameplay.Buckets
{
    public abstract class BucketEntry<T>
    {
        public abstract int Weight { get; }
        public abstract T Value { get; }
    }
}