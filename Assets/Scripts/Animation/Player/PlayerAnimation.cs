using RPG.Data.Player;
namespace RPG.Animation.Base.Player
{
    public class PlayerAnimation : BaseAnimation
    {
        // Start is called before the first frame update
        void Start()
        {
            animator = PlayerData.instance.Resources.PlayerAnimator;
        }

        public override void MovementAnimation(float speedPercent, float smoothTime, float delta)
        {
            base.MovementAnimation(speedPercent, smoothTime, delta);
        }
    }
}
