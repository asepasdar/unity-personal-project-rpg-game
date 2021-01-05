using RPG.Stats.Base;
using System.Collections;
using UnityEngine;

namespace RPG.Combat.Base {
    [RequireComponent(typeof(BaseStats))]
    public class CombatCharacter : MonoBehaviour
    {
        BaseStats MyStats;
        float attackColdown = 0f;
        readonly float attackDelay = .6f;
        private void Start()
        {
            MyStats = GetComponent<BaseStats>();
        }

        private void Update()
        {
            attackColdown -= Time.deltaTime;
        }

        public void Attack(BaseStats target, Transform faceTo) {
            if (attackColdown <= 0) {
                transform.LookAt(faceTo);
                Debug.Log(gameObject.name + " ASPD : " + MyStats.Stats.AttackSpeed);
                StartCoroutine(DoDamage(target, attackDelay));
                attackColdown = 3f / MyStats.Stats.AttackSpeed;
            }
        }

        IEnumerator DoDamage(BaseStats target, float delay) {
            yield return new WaitForSeconds(delay);
            target.TakeDamage(MyStats.Stats.Damage);
        }
    } 
}
