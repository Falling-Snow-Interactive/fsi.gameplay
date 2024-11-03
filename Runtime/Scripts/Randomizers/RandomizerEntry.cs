namespace Fsi.Gameplay.Randomizers
{
    public abstract class RandomizerEntry<T>
    {
        public abstract int Weight { get; }
        public abstract T Value { get; }
    }
}