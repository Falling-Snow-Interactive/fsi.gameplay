using System;

namespace Fsi.Gameplay.Healths
{
	public interface IDamageable
	{
		event Action Damaged;
		int Damage(int damage);
	}
}