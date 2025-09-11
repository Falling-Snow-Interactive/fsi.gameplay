using System;
using UnityEngine;

namespace Fsi.Gameplay.Healths
{
	[Serializable]
	public class Health
	{
		public int current;
		public int max;

		public Health(int health)
		{
			current = health;
			max = health;
		}

		public bool IsAlive => current > 0;
		public bool IsDead => current <= 0;

		public float Normalized => (float)current / max;
		public event Action Changed;
		public event Action Died;

		public void Initialize(int maxHealth)
		{
			max = maxHealth;
			current = maxHealth;

			Changed?.Invoke();
		}

		public int Damage(int damage)
		{
			if (IsDead) return 0;

			int damaged = Mathf.Clamp(damage, 0, current);
			current -= damaged;
			Changed?.Invoke();

			if (IsDead) Died?.Invoke();

			return damaged;
		}

		public int Heal(int heal)
		{
			if (IsDead) return 0;

			int healed = Mathf.Clamp(heal, 0, max - current);
			current += healed;
			Changed?.Invoke();

			return healed;
		}

		public void SetMax(int max)
		{
			this.max = max;
			current = Mathf.Clamp(current, 0, max);

			Changed?.Invoke();
		}
	}
}