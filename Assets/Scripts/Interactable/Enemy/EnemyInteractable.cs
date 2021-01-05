using RPG.Movement.Base.Enemy;
using System.Collections;
using UnityEngine;

namespace RPG.Interact.Base.Enemy
{
    public class EnemyInteractable : Interactable
    {
        EnemyMovement _movement;
        protected override void Start()
        {
            base.Start();
            _movement = GetComponent<EnemyMovement>();
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _movement.ChangeState();
                StartCoroutine(Interact());
            }
        }
        protected override void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _movement.ChangeState();
        }

        protected override IEnumerator Interact()
        {
            return base.Interact();
        }

        public override void OnDefocus()
        {
            base.OnDefocus();
        }

        public override void OnFocus()
        {
            base.OnFocus();
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }
    }
}
