using System.Collections.Generic;
using UnityEngine;
using RPG.Data.Inventory;
using RPG.Resources.Inventory;
using RPG.Scriptable.Base;
using System.Linq;

namespace RPG.Inventory.Base.Slot
{
    public class SlotsManager : MonoBehaviour
    {
        public List<InventorySlot> Slots;

        ItemInfo _selectedItem = null;
        int _selectedIndex = -1;
        InventoryData _inven;
        private void Start()
        {
            _inven = InventoryData.instance;
            _inven.onItemChangedCallback += UpdateUI;
            _inven.onItemInfoCallback += ShowInfo;
        }

        void UpdateUI() {
            for(int i = 0; i < _inven.space; i++)
            {
                Slots[i].UpdateSlot(_inven.Items.ElementAtOrDefault(i) != null ? _inven.Items[i] : null);
            }
            ClearInfo();
        }

        void ShowInfo(ItemInfo item, int index) {
            InventoryResources resources = InventoryData.instance.Resources;
            resources.InfoIcon.enabled = true;
            resources.InfoIcon.sprite = item.Item.ItemIcon;
            resources.InfoName.text = item.Item.ItemName;
            resources.InfoDescription.text = item.Item.ItemDescription;

            resources.ActionUnequip.gameObject.SetActive(false);
            resources.ActionEquip.gameObject.SetActive(true);

            resources.ActionRemove.interactable = true;
            resources.ActionEquip.interactable = item.Item.ItemType == IType.Equipment;

            _selectedIndex = index;
            _selectedItem = item;
        }

        void ClearInfo() {
            InventoryResources resources = InventoryData.instance.Resources;
            resources.InfoIcon.enabled = false;
            resources.InfoIcon.sprite = null;
            resources.InfoName.text = "-";
            resources.InfoDescription.text = "";

            resources.ActionRemove.interactable = false;
            resources.ActionEquip.interactable = false;

            _selectedItem = null;
            _selectedIndex = -1;
        }

        public void UseItem() {

            if (_selectedItem != null && _selectedIndex != -1)
            {
                var (status, message) = InventoryData.instance.EquipItem(_selectedItem);
                if (!status)
                    Debug.Log(message);
            }
        }
    }
}
