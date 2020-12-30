using UnityEngine;

namespace RPG.Scriptable.Base.Equipment
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
    public class ItemEquipment : BaseItem
    {
        public int Attack = 0;
        public int Defence = 0;
        public EquipmentType Slot = EquipmentType.Head;
        public GameObject Object;
    }
}
