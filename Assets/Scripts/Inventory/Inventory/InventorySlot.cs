using UnityEngine.UI;
using UnityEngine;
using RPG.Data.Inventory;

namespace RPG.Inventory.Base.Slot {
    public class InventorySlot : MonoBehaviour
    {
        public Image Icon;
        public Text Qty;

        ItemInfo item;
        public void UpdateSlot(ItemInfo newItem) {
            if (newItem != null && newItem.Qty != 0)
            {
                item = newItem;
                Icon.enabled = true;
                Icon.sprite = item.Item.ItemIcon;
                Qty.text = item.Qty > 1 ? item.Qty.ToString() : " ";
            }
            else {
                ClearSlot();
            }
        }

        public void ClearSlot() {
            item = null;
            Icon.enabled = false;
            Icon.sprite = null;
            Qty.text = " ";
        }

        public void OnClick() {
            if (item != null) InventoryData.instance.GetItem(item);
        }
    }
}
