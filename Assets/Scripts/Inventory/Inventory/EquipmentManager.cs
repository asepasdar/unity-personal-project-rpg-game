﻿using RPG.Data.Inventory;
using RPG.Data.Player;
using RPG.Resources.Inventory;
using RPG.Scriptable.Base;
using RPG.Scriptable.Base.Equipment;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory.Base.Slot
{
    public class EquipmentManager : MonoBehaviour
    {
        public Sprite DefaultIcon;

        public List<Image> Icons = new List<Image>(){
            null, null, null, null, null, null, null
        };

        public List<Sprite> DefaultIcons = new List<Sprite>() {};


        public readonly List<ItemEquipment> MySlot = new List<ItemEquipment>(){
            null, null, null, null, null, null, null
        };
        readonly Dictionary<EquipmentType, int> IndexSlot = new Dictionary<EquipmentType, int> {
            { EquipmentType.Head, 0 },
            { EquipmentType.OneHandWeapon, 1 },
            { EquipmentType.TwoHandWeapon, 1 },
            { EquipmentType.Body, 2 },
            { EquipmentType.Shield, 3 },
            { EquipmentType.Ring, 4 },
            { EquipmentType.Accessories, 5 },
            { EquipmentType.Scroll, 6 },
        };
        InventoryData _inventory;
        int _selectedParam = 0;
        GameObject _currentWeapon;
        void Start()
        {
            _inventory = InventoryData.instance;
            _inventory.onEquipmentCallback += UpdateUI;
        }

        public void SelectEquipment(int param)
        {
            _selectedParam = param;
            ShowInfo();
        }

        public void Unequip()
        {
            if (MySlot[_selectedParam] != null) {
                bool result = _inventory.Add(MySlot[_selectedParam]);
                if (result)
                {
                    List<ItemEquipment> list = new List<ItemEquipment> { MySlot[_selectedParam] };
                    MySlot[_selectedParam] = null;
                    if (_inventory.onEquipmentCallback != null)
                        _inventory.onEquipmentCallback.Invoke(null, list);
                }
            }
        }

        public List<ItemEquipment> SwapItem(ItemInfo item)
        {
            List<ItemEquipment> response = new List<ItemEquipment>();
            ItemEquipment equipment = (ItemEquipment)item.Item;

            IndexSlot.TryGetValue(equipment.Slot, out int myIndex);

            if (myIndex >= 0)
            {
                if (MySlot[myIndex] != null)
                    response.Add(MySlot[myIndex]);

                MySlot[myIndex] = equipment;

                if (equipment.Slot == EquipmentType.TwoHandWeapon && MySlot[IndexSlot[EquipmentType.Shield]] != null)
                {
                    response.Add(MySlot[IndexSlot[EquipmentType.Shield]]);
                    MySlot[IndexSlot[EquipmentType.Shield]] = null;
                }
                else if (equipment.Slot == EquipmentType.Shield && MySlot[IndexSlot[EquipmentType.TwoHandWeapon]] != null
                    && MySlot[IndexSlot[EquipmentType.TwoHandWeapon]].Slot == EquipmentType.TwoHandWeapon)
                {
                    response.Add(MySlot[IndexSlot[EquipmentType.TwoHandWeapon]]);
                    MySlot[IndexSlot[EquipmentType.TwoHandWeapon]] = null;
                }
            }
            return response;
        }

        #region Private helper function

        private void UpdateUI(ItemEquipment equip, List<ItemEquipment> unequip)
        {
            foreach (ItemEquipment data in unequip) {
                Icons[IndexSlot[data.Slot]].sprite = DefaultIcons[IndexSlot[data.Slot]];
                if (data.Slot == EquipmentType.Shield && equip != null && equip.Slot == EquipmentType.TwoHandWeapon)
                    Icons[IndexSlot[data.Slot]].sprite = DefaultIcon;

                if(data.Slot == EquipmentType.TwoHandWeapon)
                {
                    _currentWeapon = null;
                    Destroy(_currentWeapon);
                }
                    
            }
            if (equip != null)
            {
                Icons[IndexSlot[equip.Slot]].sprite = equip.ItemIcon;
                if (equip.Slot == EquipmentType.TwoHandWeapon)
                    _currentWeapon = Instantiate(equip.Object, PlayerData.instance.Resources.WeaponPosition);
            }
        }

        private void ShowInfo()
        {
            if (MySlot[_selectedParam] != null)
            {
                InventoryResources resources = _inventory.Resources;

                resources.InfoIcon.enabled = true;
                resources.InfoIcon.sprite = MySlot[_selectedParam].ItemIcon;
                resources.InfoName.text = MySlot[_selectedParam].ItemName;
                resources.InfoDescription.text = MySlot[_selectedParam].ItemDescription;

                resources.ActionUnequip.gameObject.SetActive(true);
                resources.ActionEquip.gameObject.SetActive(false);
                resources.ActionRemove.interactable = false;

            }
        }
        #endregion
    }
}
