using System;
using Random = UnityEngine.Random;

namespace Fsi.Gameplay.Numbers
{
	[Serializable]
	public class Percent
	{
		public float value;

		public Percent()
		{
			value = 0.7f;
		}

		public bool Roll()
		{
			return Random.value <= value;
		}
		
		#region Operators
		
		public static bool operator >(Percent p, float f)
		{
			return p.value > f;
		}

		public static bool operator <(Percent p, float f)
		{
			return p.value < f;
		}

		public static bool operator >=(Percent p, float f)
		{
			return p.value >= f;
		}

		public static bool operator <=(Percent p, float f)
		{
			return p.value <= f;
		}

		public static bool operator ==(Percent p, float f)
		{
			return p.value == f;
		}

		public static bool operator !=(Percent p, float f)
		{
			return !(p == f);
		}
		
		protected bool Equals(Percent other)
		{
			return value.Equals(other.value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Percent)obj);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}
		
		#endregion
	}
}