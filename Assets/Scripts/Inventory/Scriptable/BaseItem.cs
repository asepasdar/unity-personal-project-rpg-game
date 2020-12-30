using UnityEngine;

namespace RPG.Scriptable.Base
{
    public enum IRarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }

    public enum IType
    {
        Loot,
        Equipment
    }
    public enum EquipmentType
    {
        Head,
        Body,
        Shoes,
        Accessories,
        Weapon,
        Secondary
    }
    public class BaseItem : ScriptableObject
    {
        public string ItemName = "New Item";
        public Sprite ItemIcon = null;
        public IRarity ItemRarity;
        public IType ItemType;
        public int ItemCost = 10;
    }
}