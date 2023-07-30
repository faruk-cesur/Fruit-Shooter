using UnityEngine;

public class SafeAreaScript : MonoBehaviour
{
    private RectTransform rectTransform; // Rect Transform is 2D Transform for UI objects
    private Rect safeArea; // A 2D Rectangle defined by X and Y position, width and height
    private Vector2 minAnchor;
    private Vector2 maxAnchor;

    //Please visit Unity Docs. for details,
    //https://docs.unity3d.com/ScriptReference/Rect.html
    //https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/class-RectTransform.html
    
    private void Awake() {
        
        rectTransform = GetComponent<RectTransform>();
        
        safeArea = Screen.safeArea;
        
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}
