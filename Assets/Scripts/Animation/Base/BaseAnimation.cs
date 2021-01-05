using UnityEngine;

namespace RPG.Animation.Base
{
    public class BaseAnimation : MonoBehaviour
    {
        protected Animator animator;

        public virtual void MovementAnimation(float speedPercent, float smoothTime, float delta)
        {
            animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);
        }

        public virtual void Attack() {
            animator.SetTrigger("Attack");
        }
    }
}
