using System;

namespace Fsi.Gameplay.Healths
{
    public interface IDamageable
    {
        event Action Damaged;
        void Damage(int damage);
    }
}