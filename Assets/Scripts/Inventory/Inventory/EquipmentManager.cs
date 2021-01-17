using RPG.Data.Inventory;
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
        GameObject _currentWeapon, _currentShield;
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

                if (data.Slot == EquipmentType.TwoHandWeapon || data.Slot == EquipmentType.OneHandWeapon)
                {
                    Destroy(_currentWeapon);
                    _currentWeapon = null;
                }
                else if (data.Slot == EquipmentType.Shield) {
                    Destroy(_currentShield);
                    _currentShield = null;
                }
                    
            }
            if (equip != null)
            {
                Icons[IndexSlot[equip.Slot]].sprite = equip.ItemIcon;
                if (equip.Slot == EquipmentType.TwoHandWeapon || equip.Slot == EquipmentType.OneHandWeapon)
                    _currentWeapon = Instantiate(equip.Object, PlayerData.instance.Resources.WeaponPosition);
                else if(equip.Slot == EquipmentType.Shield)
                    _currentShield = Instantiate(equip.Object, PlayerData.instance.Resources.ShieldPosition);

            }
        }

        private void ShowInfo()
        {
            if (MySlot[_selectedParam] != null)
            {
                _inventory.Resources.InfoIcon.enabled = true;
                _inventory.Resources.InfoIcon.sprite = MySlot[_selectedParam].ItemIcon;
                _inventory.Resources.InfoName.text = MySlot[_selectedParam].ItemName;
                _inventory.Resources.InfoDescription.text = MySlot[_selectedParam].ItemDescription;

                _inventory.Resources.ActionUnequip.gameObject.SetActive(true);
                _inventory.Resources.ActionEquip.gameObject.SetActive(false);
                _inventory.Resources.ActionRemove.interactable = false;

            }
        }
        #endregion
    }
}
