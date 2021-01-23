using UnityEngine;
using RPG.Resources.Player;
using RPG.Data.Inventory;
using System.Collections.Generic;
using RPG.Scriptable.Base.Equipment;
using RPG.Scriptable.Base;
using RPG.Inventory.Base.Slot;

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
        public EquipmentManager Equipment;
        public GameObject Player;
    }
}
