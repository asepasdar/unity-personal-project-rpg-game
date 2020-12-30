using UnityEngine;
using RPG.Resources.Player;

namespace RPG.Data.Player
{
    public class PlayerData : MonoBehaviour
    {
        #region Singleton
        public static PlayerData instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one insance of PlayerData");
                return;
            }
            instance = this;
        }
        #endregion
        public PlayerResources Resources;
    }
}
