using UnityEngine;

namespace RPG.Scriptable.Base.Loot
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Loot")]
    public class ItemLoot : BaseItem
    {
        public bool UpgradeMaterial = false;
    }
}
