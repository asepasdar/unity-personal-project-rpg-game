using RPG.Animation.Base.Enemy;
using RPG.Combat.Base;
using RPG.Data.Enemy;
using RPG.Data.Player;
using RPG.Stats.Base;
using System.Collections;
using UnityEngine;

namespace RPG.Movement.Base.Enemy
{
    [RequireComponent(typeof(EnemyAnimation))]
    [RequireComponent(typeof(CombatCharacter))]
    public class EnemyMovement : Movement
    {
        public float Radius = 8f;
        public CombatCharacter Mycombat;
        
        BaseStats _targetStats;
        Transform _target;
        bool _inAttackRadius = false;
        bool _inRadius = false;
        protected override void Start()
        {
            base.Start();
            _targetStats = PlayerData.instance.Player.GetComponent<BaseStats>();
            _target = PlayerData.instance.Player.GetComponent<Transform>();
            UpdateManager.instance.Movements.Add(this);
            EnemyData.instance.Enemies.Add(transform);
            StartCoroutine(WaitAndChase());
        }

        public override void FixedUpdateMe()
        {
            base.FixedUpdateMe();
        }

        public void Chase(Vector3 target) {
            MoveToObject(target, 3f);
        }

        IEnumerator WaitAndChase()
        {
            yield return new WaitUntil(() => {
                while (!_inRadius || !_inAttackRadius) {
                    float distance = Vector3.Distance(transform.position, _target.position);
                    if (distance <= Radius)
                    {
                        _inRadius = true;
                        MoveToObject(_target.position, 3f);
                    }
                    return false;
                }
                return true;
            });
            StartCoroutine(AttackTarget());
        }

        IEnumerator AttackTarget() {
            yield return new WaitUntil(() =>
            {
                while (_inAttackRadius)
                {
                    Mycombat.Attack(_targetStats, _target, 1f);
                    return false;
                }
                return true;
            });
            StartCoroutine(WaitAndChase());
        }

        public void ChangeState(bool status) {
            _inAttackRadius = status;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }

        private void OnDestroy()
        {
            UpdateManager.instance.Movements.Remove(this);
        }
    }
}
