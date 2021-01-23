using RPG.Resources.UI;
using RPG.Scriptable.Base.Event.Boolean;
using System.Linq;
using UnityEngine;

namespace RPG.Data.UI {
    public class UIData : MonoBehaviour
    {
        public UIResources Resources;

        [Header("Listening Channel")]
        public EventBool ChannelInventory;
        public EventBool ChannelEquipment;
        public EventBool ChannelItemInfo;
        public EventBool ChannelLoading;

        #region Singleton
        private void Awake()
        {
            ChannelInventory.Channel += ToggleInventory;
            ChannelEquipment.Channel += ToggleEquipment;
            ChannelItemInfo.Channel += ToggleItemInfo;
            ChannelLoading.Channel += ToggleLoading;
        }
        #endregion

        void ToggleInventory(bool status) {
            Resources.Inventory.SetActive(status);
        }
        void ToggleItemInfo(bool status)
        {
            Resources.ItemInfo.SetActive(status);
        }
        void ToggleEquipment(bool status)
        {
            Resources.Equipment.SetActive(status);
        }
        void ToggleLoading(bool status) {
            Resources.LoadingScreen.SetActive(status);
        }
    } 
}
