using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shun_Utilities
{
    // this is Optional for pooled Monobehaviour
    // extend this interface to be able to call the methods on spawn
    public interface IPoolable
    {
        void OnSpawned();
        void OnDespawned();
    }
}
