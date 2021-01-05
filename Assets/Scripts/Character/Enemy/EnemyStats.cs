using RPG.Data.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats.Base.Enemy
{
    public class EnemyStats : BaseStats
    {
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public override void Die()
        {
            base.Die();
            EnemyData.instance.Enemies.Remove(transform);
            Destroy(gameObject);
        }
    }
}
