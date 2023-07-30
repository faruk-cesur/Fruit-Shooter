using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [field: SerializeField] public Transform CameraTransform { get; set; }

    private void Awake()
    {
        if (CameraTransform == null)
        {
            if (Camera.main != null)
                CameraTransform = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + CameraTransform.forward);
    }
}