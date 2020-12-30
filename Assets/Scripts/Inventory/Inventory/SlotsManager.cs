using System.Collections.Generic;
using UnityEngine;
using RPG.Data.Inventory;

namespace RPG.Inventory.Base.Slot
{
    public class SlotsManager : MonoBehaviour
    {
        public List<InventorySlot> Slots;

        private void Start()
        {
            InventoryData.instance.onItemChangedCallback += UpdateUI;
        }

        void UpdateUI(ItemInfo item, int index) {
            Slots[index].AddItem(item);
            Debug.Log("Update UI");
        }
    }
}
