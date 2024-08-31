#if UNITY_EDITOR


using UnityEditor;

namespace Shun_Utilities
{
    [CustomPropertyDrawer(typeof (CountdownPropertyOnce))]
    public class CountdownOncePropertyDrawer : CountdownPropertyDrawerBase
    {
    }
}

#endif