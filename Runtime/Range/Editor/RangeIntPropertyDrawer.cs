#if UNITY_EDITOR


using UnityEditor;

namespace Shun_Utilities
{
    [CustomPropertyDrawer(typeof (RangeInt))]
    public class RangeIntPropertyDrawer : RangePropertyDrawerBase
    {
    }
}

#endif