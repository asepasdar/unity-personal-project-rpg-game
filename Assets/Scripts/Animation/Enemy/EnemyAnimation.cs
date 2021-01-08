using System.Linq;
using UnityEngine;

namespace RPG.Animation.Base.Enemy
{
    public class EnemyAnimation : BaseAnimation
    {
        public Animator anim;
        public IAnimationType AnimationType;
        // Start is called before the first frame update
        void Start()
        {
            animator = anim;
            OverrideAnimator();

            SetAnmationClip(AnimationData.instance.Animations.Where(w => w.Type == AnimationType).FirstOrDefault());
        }

        public override void MovementAnimation(float speedPercent, float smoothTime, float delta)
        {
            base.MovementAnimation(speedPercent, smoothTime, delta);
        }

        public override void Attack(float speed)
        {
            base.Attack(speed);
        }
    }
}
