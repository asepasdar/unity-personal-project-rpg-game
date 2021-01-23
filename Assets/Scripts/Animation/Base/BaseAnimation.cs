using UnityEngine;

namespace RPG.Animation.Base
{
    public class BaseAnimation : MonoBehaviour
    {
        protected Animator animator;
        protected AnimatorOverrideController animatorOverrideController;
        protected AnimationSet _currentSet;
        public virtual void MovementAnimation(float speedPercent, float smoothTime, float delta)
        {
            animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);
        }

        public virtual void Attack(float speed) {
            animator.SetFloat("attackSpeed", _currentSet.Clips.Attack.length / speed);
            animator.SetTrigger("Attack");
        }

        protected void OverrideAnimator() {
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;
        }

        protected virtual void SetAnmationClip(AnimationSet anim)
        {
            _currentSet = anim;
            animatorOverrideController["Idle"] = anim.Clips.Idle;
            animatorOverrideController["Walk_Static"] = anim.Clips.Walk;
            animatorOverrideController["Run_Static"] = anim.Clips.Run;
            animatorOverrideController["Punch"] = anim.Clips.Attack;
        }
    }
}
