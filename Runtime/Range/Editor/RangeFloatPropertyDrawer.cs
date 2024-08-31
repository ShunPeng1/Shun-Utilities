#if UNITY_EDITOR

using UnityEditor;

namespace Shun_Utilities
{
    [CustomPropertyDrawer(typeof (RangeFloat))]
    public class RangeFloatPropertyDrawer : RangePropertyDrawerBase
    {
    }
}

#endif