using System.Collections;
using UnityEngine;

namespace RPG.Interact.Base.NPC
{
    public class NPCInteractable : Interactable
    {
        #region All Override Function
        protected override IEnumerator Interact()
        {
            yield return base.Interact();
            Debug.Log("test");
        }
        protected override void OnTriggerEnter(Collider other) {
            base.OnTriggerEnter(other);
            //TODO : Show dialoge
        }
        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            //TODO : Hide dialoge
        }
        public override void OnFocus()
        {
            base.OnFocus();
            Debug.Log("TODO : Set texture on NPC");
            //TODO : Set Texture on NPC
        }

        public override void OnDefocus() {
            base.OnDefocus();
            Debug.Log("TODO : Remove texture on NPC");
            //TODO : Remove texture on NPC
        }
        #endregion
    }
}
