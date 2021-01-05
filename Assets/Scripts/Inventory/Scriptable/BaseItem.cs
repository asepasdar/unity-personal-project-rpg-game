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
        Ring,
        Accessories,
        TwoHandWeapon,
        OneHandWeapon,
        Shield,
        Scroll
    }
    public class BaseItem : ScriptableObject
    {
        public string ItemName = "New Item";
        public Sprite ItemIcon = null;
        [TextArea]
        public string ItemDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book";
        public IRarity ItemRarity;
        public IType ItemType;
        public int ItemCost = 10;
    }
}