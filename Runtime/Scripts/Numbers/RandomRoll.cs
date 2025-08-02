using System;
using Random = UnityEngine.Random;

namespace Fsi.Gameplay.Numbers
{
	[Serializable]
	public class RandomRoll
	{
		public float roll;

		public RandomRoll()
		{
			roll = 0.7f;
		}

		public bool Roll()
		{
			return Random.value <= roll;
		}

		#region Operators

		public static bool operator >(RandomRoll p, float f)
		{
			return p.roll > f;
		}

		public static bool operator <(RandomRoll p, float f)
		{
			return p.roll < f;
		}

		public static bool operator >=(RandomRoll p, float f)
		{
			return p.roll >= f;
		}

		public static bool operator <=(RandomRoll p, float f)
		{
			return p.roll <= f;
		}

		public static bool operator ==(RandomRoll p, float f)
		{
			return p.roll == f;
		}

		public static bool operator !=(RandomRoll p, float f)
		{
			return !(p == f);
		}

		protected bool Equals(RandomRoll other)
		{
			return roll.Equals(other.roll);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((RandomRoll)obj);
		}

		public override int GetHashCode()
		{
			return roll.GetHashCode();
		}

		#endregion
	}
}