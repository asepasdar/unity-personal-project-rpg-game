using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources.Inventory
{
    public class InventoryResources : MonoBehaviour
    {
        [Header("Resources Item Info")]
        public Image InfoIcon;
        public TextMeshProUGUI InfoName;
        public TextMeshProUGUI InfoDescription;

        [Header("Resources Item Action")]
        public Button ActionEquip;
        public Button ActionRemove;
        public Button ActionUnequip;

    }
}