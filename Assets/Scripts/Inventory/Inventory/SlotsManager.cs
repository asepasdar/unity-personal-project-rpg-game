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

            _inven.Resources.InfoIcon.enabled = true;
            _inven.Resources.InfoIcon.sprite = item.Item.ItemIcon;
            _inven.Resources.InfoName.text = item.Item.ItemName;
            _inven.Resources.InfoDescription.text = item.Item.ItemDescription;

            _inven.Resources.ActionUnequip.gameObject.SetActive(false);
            _inven.Resources.ActionEquip.gameObject.SetActive(true);

            _inven.Resources.ActionRemove.interactable = true;
            _inven.Resources.ActionEquip.interactable = item.Item.ItemType == IType.Equipment;

            _selectedIndex = index;
            _selectedItem = item;
        }

        void ClearInfo() {
            _inven.Resources.InfoIcon.enabled = false;
            _inven.Resources.InfoIcon.sprite = null;
            _inven.Resources.InfoName.text = "-";
            _inven.Resources.InfoDescription.text = "";

            _inven.Resources.ActionRemove.interactable = false;
            _inven.Resources.ActionEquip.interactable = false;

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
