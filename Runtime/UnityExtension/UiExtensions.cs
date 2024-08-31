using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Shun_Utilities
{
    public static class UiExtensions
    {
        public static Vector3 GetWorldPositionOfCanvasElement(this RectTransform element, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            
            if (camera == null)
            {
                Debug.LogError("No camera found");
                return Vector3.zero;
            }
            
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, camera, out var result);
            return result;
        }
        
        
        public static Vector3 GetUIPositionFromWorldPosition(this Canvas parentCanvas, Vector3 worldPos, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            
            if (camera == null)
            {
                Debug.LogError("No camera found");
                return Vector3.zero;
            }
            
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            Vector3 screenPos = camera.WorldToScreenPoint(worldPos);

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out var movePos);
            //Convert the local point to world point
            return parentCanvas.transform.TransformPoint(movePos);
        }

    }
}