using UnityEngine;
namespace RPG.Stats.Base {
    public class BaseStats : MonoBehaviour
    {
        public Stats Stats;
        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = Stats.MaxHealth;
        }

        public virtual void TakeDamage(int damage) {
            damage -= Stats.BaseDef.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);

            CurrentHealth -= damage;
            if (CurrentHealth <= 0) {
                Die();
            }
        }

        public virtual void Die() { 
        
        }
    }
}
