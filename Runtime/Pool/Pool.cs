using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shun_Utilities
{
    [Serializable]
    public class Pool
    {
        [HideInInspector] public string Name;
        public GameObject Prefab;
        public int InitSize = 10;
        public bool HasMaxSize = false;
        public int MaxSize = 100;

        
        private Transform _originalParent;
        private List<GameObject> _pooledInstances;
        private List<GameObject> _aliveInstances;


        public Pool(GameObject prefab, int initSize, Transform originalParent = null)
        {
            this.Prefab = prefab;
            this.InitSize = initSize;
            Init(originalParent);
        }

        public void Validate()
        {
            Name = Prefab != null ? Prefab.name : "No Prefab Pool";
            if (HasMaxSize && MaxSize < InitSize)
                MaxSize = InitSize;
                
        }

        public bool Init(Transform parent = null)
        {
            if (Prefab == null)
                return false;
            _pooledInstances = new List<GameObject>();
            _aliveInstances = new List<GameObject>();
            _originalParent = parent;

            for (int i = 0; i < InitSize; i++)
            {
                GameObject instance = GameObject.Instantiate(Prefab);
                instance.SetActive(false);
                instance.transform.parent = _originalParent;
                _pooledInstances.Add(instance);
            }
            return true;
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null)
        {
            if (_pooledInstances == null) return null;
            
            if (_pooledInstances.Count <= 0)
            {
                if (HasMaxSize)
                {
                    if (_aliveInstances.Count >= MaxSize)
                    {
                        Debug.LogWarning("[PoolManager] MaxSize of \"" + Prefab.name + "\" reached. Cannot create more!");
                        return null;
                    }
                }
                GameObject newInstance = GameObject.Instantiate(Prefab);
                newInstance.SetActive(true);
                newInstance.transform.SetParent(parent);
                newInstance.transform.position = position;
                newInstance.transform.rotation = rotation;
                newInstance.transform.localScale = scale;

                _aliveInstances.Add(newInstance);
                
                Debug.LogWarning("[PoolManager] Pool of \"" + Prefab.name + "\" is empty. Created a new instance.");
                
                return newInstance;
            }

            GameObject obj = _pooledInstances[^1];
            if (obj == null) return null;

            obj.SetActive(true);
            obj.transform.SetParent(parent);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            _pooledInstances.RemoveAt(_pooledInstances.Count - 1);
            _aliveInstances.Add(obj);

            obj.GetComponent<IPoolable>()?.OnSpawned();
            return obj;
        }

        public void Despawn(GameObject obj)
        {
            int index = _aliveInstances.FindIndex(o => obj == o);
            if (index == -1)
            {
                GameObject.Destroy(obj);
                return;
            }
            obj.SetActive(false);
            obj.transform.parent = _originalParent;
            _aliveInstances.RemoveAt(index);
            _pooledInstances.Add(obj);
            obj.GetComponent<IPoolable>()?.OnDespawned();
        }

        public bool IsResponsibleForObject(GameObject obj)
        {
            int index = _aliveInstances.FindIndex(o => ReferenceEquals(obj, o));
            if (index == -1)
                return false;
            return true;
        }
    }
}
