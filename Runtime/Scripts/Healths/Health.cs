using System;
using UnityEngine;

namespace Fsi.Gameplay.Healths
{
    [Serializable]
    public class Health
    {
        public event Action Changed;
        public event Action Died;

        public bool IsAlive => current > 0;
        public bool IsDead => current <= 0;
        
        public int current;
        public int max;
        
        public float Normalized => (float)current / max;

        public Health(int health)
        {
            current = health;
            max = health;
        }

        public void Initialize(int maxHealth)
        {
            max = maxHealth;
            current = maxHealth;
            
            Changed?.Invoke();
        }

        public void Damage(int damage)
        {
            if (IsDead)
            {
                return;
            }
            
            current = Mathf.Clamp(current - damage, 0, max);
            Changed?.Invoke();

            if (IsDead)
            {
                Died?.Invoke();
            }
        }

        public void Heal(int heal)
        {
            if (IsDead)
            {
                return;
            }
            
            current = Mathf.Clamp(current + heal, 0, max);
            Changed?.Invoke();
        }
    }
}