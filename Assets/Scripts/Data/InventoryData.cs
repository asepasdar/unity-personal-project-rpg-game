using RPG.Scriptable.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public int space = 20;
        public List<ItemInfo> Items = new List<ItemInfo>();

        public delegate void OnItemChanged(ItemInfo item, int index);
        public OnItemChanged onItemChangedCallback;

        public bool Add(BaseItem item)
        {
            if (Items.Count >= space) return false;

            FindSimiliar(item);
            return true;
        }

        public void Remove(ItemInfo item, int index)
        {
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(item, index);
            Items.Remove(item);
        }

        public ItemInfo GetItem(int index)
        {
            if (Items.Count > index && Items[index] != null) return Items[index];
            return null;
        }

        public void ShowQty()
        {
            foreach (ItemInfo item in Items)
            {
                Debug.Log(item.Item.ItemName + " :" + item.Qty);
            }
        }

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
                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke(Items[indexLocation[0]], indexLocation[0]);
                    return true;
                }
            }
            ItemInfo slot = new ItemInfo
            {
                Item = item,
                Qty = 1
            };
            Items.Add(slot);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(slot, Items.Count-1);
            return true;
        }
    }
}
