#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

/// <summary>
/// Colorful Hierarchy Window Group Header
/// Author: github.com/farukcan
/// Thanks for concept of idea :
/// http://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header
/// Sample GameObject Names: "#red CAMERA" , "#" , "##E7A5F6 Hex" , "# "
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGroupHeader
{
    static HierarchyWindowGroupHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null && gameObject.name.StartsWith("#", System.StringComparison.Ordinal))
        {
            // format is #color
            var colorName = gameObject.name.Substring(1).Split(' ')[0];
            // covert colorname to unity color
            var color = Color.gray;
            if (ColorUtility.TryParseHtmlString(colorName.ToLower(), out var _color))
            {
                color = _color;
            }

            EditorGUI.DrawRect(selectionRect, color);
            EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("#" + colorName + " ", "").ToUpperInvariant());
        }
    }
}
#endif