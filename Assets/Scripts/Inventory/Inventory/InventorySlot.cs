using UnityEngine.UI;
using UnityEngine;
using RPG.Data.Inventory;

namespace RPG.Inventory.Base.Slot {
    public class InventorySlot : MonoBehaviour
    {
        public Image Icon;
        public Text Qty;

        ItemInfo item;
        public void AddItem(ItemInfo newItem) {
            item = newItem;
            Icon.enabled = true;
            Icon.sprite = item.Item.ItemIcon;
            Qty.text = item.Qty > 1 ? item.Qty.ToString() : " ";
        }

        public void ClearSlot() {
            item = null;
            Icon.enabled = false;
            Icon.sprite = null;
            Qty.text = " ";
        }

        public void OnClick() {
            Debug.Log("Button On click");
        }
    }
}
