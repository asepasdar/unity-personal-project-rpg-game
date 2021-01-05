using RPG.Scriptable.Base;
using RPG.Resources.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Data.Player;
using RPG.Scriptable.Base.Equipment;

namespace RPG.Data.Inventory
{
    public class ItemInfo
    {
        public BaseItem Item;
        public int Qty;
    }
    public class InventoryData : MonoBehaviour
    {
        public static InventoryData instance;
        #region Singleton
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one inventory instance");
                return;
            }
            instance = this;
        }
        #endregion

        public InventoryResources Resources;
        public int space = 24;
        public List<ItemInfo> Items = new List<ItemInfo>();

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        public delegate void OnItemInfo(ItemInfo item, int index);
        public OnItemInfo onItemInfoCallback;

        public delegate void OnEquipmentChanged(ItemEquipment Equip, List<ItemEquipment> Unequip);
        public OnEquipmentChanged onEquipmentCallback;

        public (bool, string) EquipItem(ItemInfo item) {
            bool status;
            string message;
            (status, message) = Equip(item);
            return (status, message);
        }

        public bool Add(BaseItem item)
        {
            if (Items.Count >= space) return false;

            FindSimiliar(item);
            return true;
        }

        public void Remove(ItemInfo item) {
            Items.Remove(item);

            onItemChangedCallback?.Invoke();
        }

        public ItemInfo GetItem(ItemInfo item)
        {
            int index = Items.FindIndex(s => s.Item == item.Item);
            if (index >= 0)
                onItemInfoCallback?.Invoke(item, index);
            return null;
        }


        #region Private function to help process
        private bool FindSimiliar(BaseItem item)
        {
            if (item.ItemType == IType.Loot)
            {
                int[] indexLocation = Items
                    .Select((b, i) => b.Item.ItemName.Equals(item.name) && b.Qty < 99 ? i : -1)
                    .Where(i => i != -1).ToArray();

                if (indexLocation.Length > 0)
                {
                    Items[indexLocation[0]].Qty += 1;
                    onItemChangedCallback?.Invoke();
                    return true;
                }
            }
            ItemInfo slot = new ItemInfo
            {
                Item = item,
                Qty = 1
            };
            Items.Add(slot);
            onItemChangedCallback?.Invoke();

            return true;
        }

        private (bool, string) Equip(ItemInfo item) {
            bool status = true;
            string message = "";
            List<ItemEquipment> list = PlayerData.instance.Equipment.SwapItem(item);
            if ((Items.Count-1  + list.Count) <= space)
            {
                Items.Remove(item);
                ItemEquipment equipment = (ItemEquipment)item.Item;
                foreach (var data in list)
                {
                    ItemInfo add = new ItemInfo
                    {
                        Item = data,
                        Qty = 1,
                    };
                    Items.Add(add);
                }

                onEquipmentCallback?.Invoke(equipment, list);
                onItemChangedCallback?.Invoke();
            }
            else {
                status = false;
                message = "Inventory is full";
                foreach (var data in list) {
                    ItemInfo swap = new ItemInfo
                    {
                        Item = data,
                        Qty = 1
                    };
                    PlayerData.instance.Equipment.SwapItem(swap);
                }
            }
            
            return (status, message);
        }
        #endregion
    }
}
