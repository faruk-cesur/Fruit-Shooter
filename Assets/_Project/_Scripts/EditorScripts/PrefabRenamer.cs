#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PrefabRenamer : MonoBehaviour
{
    private GameObject _prefab;

    [Obsolete("Obsolete")]
    private void OnEnable()
    {
        if (PrefabUtility.GetPrefabType(gameObject) == PrefabType.PrefabInstance)
        {
            _prefab = PrefabUtility.GetPrefabParent(gameObject) as GameObject;
            EditorApplication.update += CheckForNameChange;
        }
    }

    private void CheckForNameChange()
    {
        if (_prefab.name != name)
        {
            print("Changing instance name from: " + name + " to " + _prefab.name);
            name = _prefab.name;
        }
    }

    private void OnDisable()
    {
        EditorApplication.update -= CheckForNameChange;
    }
}
#endif