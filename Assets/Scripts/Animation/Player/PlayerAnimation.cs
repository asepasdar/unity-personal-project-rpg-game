using RPG.Data.Inventory;
using RPG.Data.Player;
using RPG.Scriptable.Base;
using RPG.Scriptable.Base.Equipment;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Animation.Base.Player
{
    public class PlayerAnimation : BaseAnimation
    {
        List<AnimationSet> _animationData;
        
        void Start()
        {
            InventoryData.instance.onEquipmentCallback += UpdateAnimator;
            _animationData = AnimationData.instance.Animations;
            animator = PlayerData.instance.Resources.PlayerAnimator;

            OverrideAnimator();
            SetAnmationClip(_animationData.Where(w => w.Type == IAnimationType.Default).FirstOrDefault());
        }

        public override void MovementAnimation(float speedPercent, float smoothTime, float delta)
        {
            base.MovementAnimation(speedPercent, smoothTime, delta);
        }

        public override void Attack(float speed)
        {
            base.Attack(speed);
        }

        private void UpdateAnimator(ItemEquipment equip, List<ItemEquipment> unequip)
        {
            foreach (ItemEquipment data in unequip) { 
                if(data.Slot == EquipmentType.OneHandWeapon || data.Slot == EquipmentType.TwoHandWeapon)
                    SetAnmationClip(_animationData.Where(w => w.Type == IAnimationType.Default).FirstOrDefault());
            }

            if (equip != null && equip.Slot == EquipmentType.TwoHandWeapon)
                SetAnmationClip(_animationData.Where(w => w.Type == IAnimationType.GreatSword).FirstOrDefault());
            else if((equip != null && equip.Slot == EquipmentType.OneHandWeapon) || 
                (equip != null && equip.Slot == EquipmentType.Shield))
                SetAnmationClip(_animationData.Where(w => w.Type == IAnimationType.SwordShield).FirstOrDefault());
        }
    }
}
