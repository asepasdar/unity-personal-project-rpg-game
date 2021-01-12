using RPG.Movement.Base.Enemy;
using System.Collections;
using UnityEngine;

namespace RPG.Interact.Base.Enemy
{
    public class EnemyInteractable : Interactable
    {
        public EnemyMovement Movement;
        public Outline Outline;
        protected override void Start()
        {
            base.Start();
            Outline.enabled = false;
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Movement.ChangeState();
            }
        }
        protected override void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Movement.ChangeState();
                Outline.enabled = false;
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
        }

        public override void OnFocus()
        {
            base.OnFocus();
            Outline.enabled = true;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }
    }
}
