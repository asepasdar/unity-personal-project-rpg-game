using System.Collections.Generic;
using UnityEngine;

namespace RPG.Data.Enemy
{
    public class EnemyData : MonoBehaviour
    {
        #region Singleton
        public static EnemyData instance;
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

        public List<Transform> Enemies;

    }
}
