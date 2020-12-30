using RPG.Resources.UI;
using System.Linq;
using UnityEngine;

namespace RPG.Data.UI {
    public class UIData : MonoBehaviour
    {
        public UIResources Resources;

        public void ToggleInventory() {
            Resources.Inventory = Resources.Inventory.Select(s => { s.SetActive(!s.activeSelf); return s; }).ToList();
        }
        public void ToggleDefaultUI() { 
            Resources.Default = Resources.Default.Select(s => { s.SetActive(!s.activeSelf); return s; }).ToList();
        }
    } 
}
