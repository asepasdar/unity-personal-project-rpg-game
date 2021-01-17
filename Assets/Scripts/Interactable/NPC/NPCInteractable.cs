using RPG.Interact.Base.NPC.Dialogue;
using RPG.Scriptable.Base.Event.Audio;
using System.Collections;
using UnityEngine;

namespace RPG.Interact.Base.NPC
{
    public class NPCInteractable : Interactable
    {
        public DialogueText Dialogue;
        public Outline _outline;
        #region All Override Function
        protected override void Start()
        {
            base.Start();
            _outline.enabled = false;
        }
        protected override IEnumerator Interact()
        {
            yield return base.Interact();
            DialogueManager.instance.StartDialogue(Dialogue);
        }
        protected override void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player") && _isFocus)
            {
                _outline.enabled = true;
                StartCoroutine(Interact());
            }
        }
        protected override void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnDefocus();
                _outline.enabled = false;
            }
        }
        public override void OnFocus()
        {
            base.OnFocus();
        }

        public override void OnDefocus() {
            base.OnDefocus();
        }
        #endregion
    }
}
