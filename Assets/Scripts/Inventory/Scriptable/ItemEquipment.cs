using UnityEngine;

namespace RPG.Scriptable.Base.Equipment
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
    public class ItemEquipment : BaseItem
    {
        public int Health = 0;
        public int Damage = 0;
        public int Def = 0;
        public int Str = 0;
        public int Agi = 0;
        public int Luck = 0;
        public EquipmentType Slot = EquipmentType.Head;
        public GameObject Object;
    }
}
