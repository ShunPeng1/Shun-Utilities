using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shun_Utilities
{
    public class ObjectPool : MonoBehaviour
    {
        public List<Pool> PoolList;

        protected void OnValidate()
        {
            if (PoolList == null || PoolList.Count == 0)
                return;
            
            foreach (var pool in PoolList)
            {
                pool.Validate();
            }
        }
    }
}
