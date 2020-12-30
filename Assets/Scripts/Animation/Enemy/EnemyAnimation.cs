using RPG.Data.Player;
using UnityEngine;

namespace RPG.Animation.Base.Enemy
{
    public class EnemyAnimation : BaseAnimation
    {
        public Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            animator = anim;
        }

        public override void MovementAnimation(float speedPercent, float smoothTime, float delta)
        {
            base.MovementAnimation(speedPercent, smoothTime, delta);
        }
    }
}
