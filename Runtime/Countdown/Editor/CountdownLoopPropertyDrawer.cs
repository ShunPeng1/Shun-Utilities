#if UNITY_EDITOR


using UnityEditor;

namespace Shun_Utilities
{
    [CustomPropertyDrawer(typeof (CountdownPropertyLoop))]
    public class CountdownLoopPropertyDrawer : CountdownPropertyDrawerBase
    {
    }
}

#endif