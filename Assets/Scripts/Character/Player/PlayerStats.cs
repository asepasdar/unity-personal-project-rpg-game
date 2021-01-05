using RPG.Data.Inventory;
using RPG.Data.Player;
using RPG.Scriptable.Base.Equipment;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats.Base.Player
{
    public class PlayerStats : BaseStats
    {
        private void Start()
        {
            InventoryData.instance.onEquipmentCallback += OnEquipmentChanged;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                TakeDamage(Stats.Damage);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public override void Die()
        {
            base.Die();
            Debug.Log("Player Die");
        }

        private void OnEquipmentChanged(ItemEquipment equip, List<ItemEquipment> unequip) {
            if (equip != null)
            {
                Stats.Str.AddModifier(equip.Str);
                Stats.Agi.AddModifier(equip.Agi);
                Stats.Luck.AddModifier(equip.Luck);
                Stats.BaseDamage.AddModifier(equip.Damage);
                Stats.BaseDef.AddModifier(equip.Def);
                Stats.BaseHealth.AddModifier(equip.Health);
            }
            foreach (ItemEquipment data in unequip)
            {
                Stats.Str.RemoveModifier(data.Str);
                Stats.Agi.RemoveModifier(data.Agi);
                Stats.Luck.RemoveModifier(data.Luck);
                Stats.BaseDamage.RemoveModifier(data.Damage);
                Stats.BaseDef.RemoveModifier(data.Def);
                Stats.BaseHealth.RemoveModifier(data.Health);
            }
        }
    }
}
