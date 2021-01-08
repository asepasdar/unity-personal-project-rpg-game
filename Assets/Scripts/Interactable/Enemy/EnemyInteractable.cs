using RPG.Movement.Base.Enemy;
using System.Collections;
using UnityEngine;

namespace RPG.Interact.Base.Enemy
{
    public class EnemyInteractable : Interactable
    {
        EnemyMovement _movement;
        Outline _outline;
        protected override void Start()
        {
            base.Start();
            _movement = GetComponent<EnemyMovement>();
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _movement.ChangeState();
                OnFocus();
                StartCoroutine(Interact());
            }
        }
        protected override void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _movement.ChangeState();
                OnDefocus();
            }
        }

        protected override IEnumerator Interact()
        {
            return base.Interact();
        }

        public override void OnDefocus()
        {
            base.OnDefocus();
            _outline.enabled = false;
        }

        public override void OnFocus()
        {
            base.OnFocus();
            _outline.enabled = true;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }
    }
}
