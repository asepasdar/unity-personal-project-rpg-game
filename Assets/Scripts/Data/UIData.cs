using RPG.Data.Player;
using RPG.Resources.UI;
using System.Linq;
using UnityEngine;

namespace RPG.Data.UI {
    public class UIData : MonoBehaviour
    {
        public UIResources Resources;
        #region Singleton
        public static UIData instance;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one EnemyData instance");
                return;
            }
            instance = this;
        }
        #endregion

        public void ToggleInventory() {
            Resources.Inventory = Resources.Inventory.Select(s => { s.SetActive(!s.activeSelf); return s; }).ToList();
        }
        public void ToggleDefaultUI() {
            Resources.Default = Resources.Default.Select(s => { s.SetActive(!s.activeSelf); return s; }).ToList();
            PlayerData.instance.IsOpenUI = !Resources.Default[0].activeSelf;
        }
    } 
}
