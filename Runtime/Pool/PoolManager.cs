using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shun_Utilities
{
    public class PoolManager : MonoBehaviour
    {
        private static Dictionary<GameObject, Pool> _prefabToPoolDict;
        private static List<Pool> _poolList;

        private static Transform _trans;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            // Reset the pool manager
            _trans = this.transform;
            _prefabToPoolDict = new Dictionary<GameObject, Pool>();
            _poolList = new List<Pool>();
            
            ObjectPool[] objectPools = gameObject.GetComponentsInChildren<ObjectPool>();
            if (objectPools == null || objectPools.Length == 0)
                return;
            
            foreach (ObjectPool objectPool in objectPools)
            {
                foreach (Pool p in objectPool.PoolList)
                {
                    if (p == null)
                        continue;
                    if (!p.Init(objectPool.transform))
                        continue;
                    _prefabToPoolDict.Add(p.Prefab, p);
                }
            }
        }

        private static GameObject SpawnNonPooledObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null)
        {
            Debug.LogWarning("[PoolManager] You are spawning a non-pooled prefab \"" + prefab.name + "\".");
            Pool pool = NewPool(prefab, 1);
            return pool.Spawn(position, rotation, scale, parent);
        }

        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null)
        {
            if (_prefabToPoolDict == null)
                _prefabToPoolDict = new Dictionary<GameObject, Pool>();
            if (!_prefabToPoolDict.ContainsKey(prefab))
                return SpawnNonPooledObject(prefab, position, rotation, scale, parent);
            return _prefabToPoolDict[prefab].Spawn(position, rotation, scale, parent);
        }

        public static Pool NewPool(GameObject obj, int initSize)
        {
            GameObject newContainer = new GameObject(obj.name)
            {
                transform =
                {
                    parent = _trans
                }
            };
            Pool pool = new Pool(obj, initSize, newContainer.transform);
            _poolList ??= new List<Pool>();
            _poolList.Add(pool);
            _prefabToPoolDict.Add(pool.Prefab, pool);
            return pool;
        }

        public static void Despawn(GameObject obj, bool destroyEvenWithoutPool = true)
        {
            if (_prefabToPoolDict == null)
                return;
            foreach (var (prefab, pool) in _prefabToPoolDict)
            {
                if (pool.IsResponsibleForObject(obj))
                {
                    pool.Despawn(obj);
                    return;
                }
            }

            if (destroyEvenWithoutPool)
            {
                Destroy(obj);
            }
            else
            {
                Debug.LogWarning("Object -" + obj.name + "- is killed but not in pool! Use Destroy instead!");
            }
            
            obj.SetActive(false);
        }

        private void OnDestroy()
        {
            _prefabToPoolDict.Clear();
            _poolList.Clear();
            _trans = null;
        }
    }
}

