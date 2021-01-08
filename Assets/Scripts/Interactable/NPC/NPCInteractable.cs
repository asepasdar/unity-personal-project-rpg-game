using System.Collections;
using UnityEngine;

namespace RPG.Interact.Base.NPC
{
    public class NPCInteractable : Interactable
    {
        Outline _outline;
        #region All Override Function
        protected override void Start()
        {
            base.Start();
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }
        protected override IEnumerator Interact()
        {
            yield return base.Interact();
            Debug.Log("test");
        }
        protected override void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player"))
            {
                OnFocus();
            }
            //TODO : Show dialoge
        }
        protected override void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnDefocus();
            }
        }
        public override void OnFocus()
        {
            base.OnFocus();
            _outline.enabled = true;
        }

        public override void OnDefocus() {
            base.OnDefocus();
            _outline.enabled = false;
        }
        #endregion
    }
}
