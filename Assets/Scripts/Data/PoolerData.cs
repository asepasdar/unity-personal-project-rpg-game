using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pooler.Base
{
    [System.Serializable]
    public struct Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }
    public class PoolerData : MonoBehaviour
    {
        public List<Pool> Pools;
        public Dictionary<string, Queue<GameObject>> PoolDictionary;

        #region Singleton
        public static PoolerData instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in Pools) {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.Size; i++) {
                    GameObject obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                PoolDictionary.Add(pool.Tag, objectPool);
            }
        }
        #endregion

        public GameObject Spawn(string tag, Transform parent = null) {
            if (!PoolDictionary.ContainsKey(tag)) return null;

            GameObject obj = PoolDictionary[tag].Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(parent, false);

            return obj;
        }

        public void BackToPool(string tag, GameObject obj) {
            obj.SetActive(false);
            obj.transform.SetParent(null, false);
            PoolDictionary[tag].Enqueue(obj);
        }
    }
}
