using UnityEngine;
namespace RPG.Resources.Player
{
    public class PlayerResources : MonoBehaviour
    {
        [Header("Player Settings")]
        public Animator PlayerAnimator;
        public Transform PlayerMainCamera;
        public Transform WeaponPosition;
        public Transform ShieldPosition;

        [Header("Canvas Battle")]
        public Transform HealthUICanvas;
        public Transform DamageUICanvas;
    }
}
