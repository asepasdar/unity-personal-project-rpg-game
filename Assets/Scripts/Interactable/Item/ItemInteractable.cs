using System.Collections;
using UnityEngine;
using RPG.Scriptable.Base;
using RPG.Scriptable.Base.Loot;
using RPG.Scriptable.Base.Equipment;
using RPG.Data.Inventory;
using RPG.Scriptable.Base.Event.Audio;

namespace RPG.Interact.Base.Item
{
    public class ItemInteractable : Interactable
    {
        public BaseItem Item;
        public AudioClip ClipEffect;

        [Header("Event Channel")]
        public EventAudio AudioChannel;
        protected override IEnumerator Interact()
        {
            yield return new WaitUntil(() => PickUp());
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) StartCoroutine(Interact());
        }

        bool PickUp() {
            bool result = InventoryData.instance.Add(Item);
            if (result)
            {
                AudioChannel.RaiseEvent(ClipEffect);
                Destroy(gameObject);
            }
            return true;
        }
    }
}
