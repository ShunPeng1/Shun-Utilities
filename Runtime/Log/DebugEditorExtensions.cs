using UnityEditor;

namespace Shun_Utilities
{
    public class DebugEditorExtensions
    {
#if UNITY_EDITOR
        public static void PingObjectAtPath(string path)
        {
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
#endif
        
    }
}