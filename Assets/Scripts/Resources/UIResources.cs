using System.Collections.Generic;
using UnityEngine;

namespace RPG.Resources.UI
{
    public class UIResources : MonoBehaviour
    {
        public GameObject LoadingScreen;

        [Header("Default UI Objects")]
        public List<GameObject> Default;
        [Header("Inventory UI Objects")]
        public List<GameObject> Inventory;

    }
}
