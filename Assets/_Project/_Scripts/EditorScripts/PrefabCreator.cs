#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabCreator : MonoBehaviour
{
    #region Parameters

    public string parentObjectName;
    public GameObject prefab;
    public Vector3 startPosition;
    public bool isFixedDistance;
    public bool isRandomX;
    public bool isRandomY;

    public float distance, minZ, maxZ;
    public int numberOfObjects;
    public float XValue, minX, maxX;
    public float YValue, minY, maxY;

    #endregion

    #region MonoBehaviour Methods

    #endregion

    #region My Methods

    public void InstantiateObjects()
    {
        GameObject parentObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        parentObject.transform.position = Vector3.zero;
        PrefabUtility.UnpackPrefabInstance(parentObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        parentObject.name = parentObjectName;
        foreach (var comp in parentObject.GetComponents<Component>())
        {
            if (!(comp is Transform))
            {
                DestroyImmediate(comp);
            }
        }

        if (numberOfObjects > 0)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parentObject.transform);
                obj.transform.localPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), minZ * i);
            }
        }
        else
        {
            int i = 0;
            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parentObject.transform);
            obj.transform.localPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), minZ + (distance * i));
            while (obj.transform.localPosition.z + distance < maxZ)
            {
                i++;
                obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parentObject.transform);
                obj.transform.localPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), minZ + (distance * i));
            }
        }
    }

    #endregion
}

[CustomEditor(typeof(PrefabCreator))]
public class CreatorEditor : Editor
{
    #region Parameters

    #endregion

    #region Editor Methods

    public override void OnInspectorGUI()
    {
        PrefabCreator creator = (PrefabCreator)target;

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        creator.parentObjectName = EditorGUILayout.TextField("Parent Object Name:", creator.parentObjectName);
        creator.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab: ", creator.prefab, typeof(GameObject), true);

        GUILayout.Label("Distance(Z) Value Set", EditorStyles.boldLabel);
        creator.isFixedDistance = GUILayout.Toggle(creator.isFixedDistance, "Is Fixed Distance");
        if (creator.isFixedDistance)
        {
            creator.distance = EditorGUILayout.FloatField("Distance:", creator.distance);
            creator.minZ = creator.distance;
            creator.maxZ = creator.distance;
            creator.numberOfObjects = EditorGUILayout.IntField("Number Of Objects:", creator.numberOfObjects);
        }
        else
        {
            creator.distance = EditorGUILayout.FloatField("Distance:", creator.distance);
            creator.minZ = EditorGUILayout.FloatField("Min Z:", creator.minZ);
            creator.maxZ = EditorGUILayout.FloatField("Max Z:", creator.maxZ);
            creator.numberOfObjects = 0;
        }

        GUILayout.Label("X Value Set", EditorStyles.boldLabel);
        creator.isRandomX = GUILayout.Toggle(creator.isRandomX, "Is Random X");
        if (creator.isRandomX)
        {
            creator.minX = EditorGUILayout.FloatField("Min X:", creator.minX);
            creator.maxX = EditorGUILayout.FloatField("Max X:", creator.maxX);
        }
        else
        {
            creator.XValue = EditorGUILayout.FloatField("X Value:", creator.XValue);
            creator.minX = creator.XValue;
            creator.maxX = creator.XValue;
        }

        GUILayout.Label("Y Value Set", EditorStyles.boldLabel);
        creator.isRandomY = GUILayout.Toggle(creator.isRandomY, "Is Random Y");
        if (creator.isRandomY)
        {
            creator.minY = EditorGUILayout.FloatField("Min Y:", creator.minY);
            creator.maxY = EditorGUILayout.FloatField("Max Y:", creator.maxY);
        }
        else
        {
            creator.YValue = EditorGUILayout.FloatField("Y Value:", creator.YValue);
            creator.minY = creator.YValue;
            creator.maxY = creator.YValue;
        }

        if (GUILayout.Button("Run Creator!"))
        {
            creator.InstantiateObjects();
        }
    }

    #endregion

    #region My Methods

    #endregion
}
#endif