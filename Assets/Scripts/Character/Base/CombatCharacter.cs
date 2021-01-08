using RPG.Animation.Base;
using RPG.Stats.Base;
using System.Collections;
using UnityEngine;

namespace RPG.Combat.Base {
    [RequireComponent(typeof(BaseStats))]
    public class CombatCharacter : MonoBehaviour
    {
        BaseStats MyStats;
        BaseAnimation anim;
        float attackColdown = 0f;
        float attackDelay = 1f;
        private void Start()
        {
            MyStats = GetComponent<BaseStats>();
            anim = GetComponent<BaseAnimation>();
        }

        private void Update()
        {
            attackColdown -= Time.deltaTime;
        }

        public bool Attack(BaseStats target, Transform faceTo, float distance, System.Action callback = null) {
            bool response = false;
            if (attackColdown <= 0) {
                response = true;
                transform.LookAt(faceTo);
                float animSpeed = (3f / MyStats.Stats.AttackSpeed) / 2;
                attackColdown = animSpeed * 2;
                anim.Attack(animSpeed);
                if (distance < 4f)
                    StartCoroutine(DoDamage(target, attackDelay, callback));
                else
                    StartCoroutine(NoTarget(animSpeed, callback));
            }

            return response;
        }

        IEnumerator DoDamage(BaseStats target, float delay, System.Action callback = null) {
            yield return new WaitForSeconds(delay);
            target.TakeDamage(MyStats.Stats.Damage);
            callback?.Invoke();
        }

        IEnumerator NoTarget(float delay, System.Action callback = null) {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    } 
}
