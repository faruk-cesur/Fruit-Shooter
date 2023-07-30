using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class FindClosestTarget : MonoBehaviour
{
    [SerializeField, BoxGroup("SETTINGS")] public float Radius = 1000f;
    [SerializeField, BoxGroup("SETTINGS")] private LayerMask _targetLayerMask;
    [SerializeField, ReadOnly, BoxGroup("DEBUGGING")] public GameObject ClosestTarget = null;
    [SerializeField, ReadOnly, BoxGroup("DEBUGGING")] public float SqrDistanceToTarget = 0;

    private void SetClosestTarget()
    {
        Vector3 myPosition = transform.position;
        float distanceToClosestTarget = Mathf.Infinity;
        GameObject closestTarget = null;
        int maxColliders = 25;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(myPosition, Radius, hitColliders, _targetLayerMask);

        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i].transform.parent.gameObject == gameObject)
            {
                continue;
            }

            float distanceToTarget = (hitColliders[i].transform.position - myPosition).sqrMagnitude;
            if (distanceToTarget < distanceToClosestTarget)
            {
                distanceToClosestTarget = distanceToTarget;
                closestTarget = hitColliders[i].gameObject;
            }
        }

        ClosestTarget = closestTarget;
        SqrDistanceToTarget = distanceToClosestTarget;
    }

    public bool IsClosestTargetNull()
    {
        return !ClosestTarget;
    }

    public GameObject[] FindTargetsInRadius(Vector3 overLapPosition, float radius)
    {
        int maxColliders = 25;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(overLapPosition, radius, hitColliders, _targetLayerMask);
        GameObject[] targetsInRadius = new GameObject[numColliders];

        for (int i = 0; i < numColliders; i++)
        {
            targetsInRadius[i] = hitColliders[i].gameObject;
        }

        return targetsInRadius;
    }

    private void FixedUpdate()
    {
        SetClosestTarget();
    }
}