using System.Collections;
using UnityEngine;
using RPG.Scriptable.Base;
using RPG.Scriptable.Base.Loot;
using RPG.Scriptable.Base.Equipment;
using RPG.Data.Inventory;

namespace RPG.Interact.Base.Item
{
    public class ItemInteractable : Interactable
    {
        public BaseItem Item;
        protected override IEnumerator Interact()
        {
            yield return new WaitUntil(() => PickUp());
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) StartCoroutine(Interact());
        }

        bool PickUp() {
            bool result;
            switch (Item.ItemType) {
                case IType.Equipment:
                    ItemEquipment equipment = (ItemEquipment)Item;
                    result = InventoryData.instance.Add(equipment);
                    break;

                default:
                    ItemLoot loot = (ItemLoot)Item;
                    result = InventoryData.instance.Add(loot);
                    break;
            }
            if (result) Destroy(gameObject);
            InventoryData.instance.ShowQty();
            return true;
        }
    }
}
