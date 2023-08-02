using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _drivingVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera _shootingVirtualCamera;

    public void EnableDrivingVirtualCamera()
    {
        _drivingVirtualCamera.gameObject.SetActive(true);
        _shootingVirtualCamera.gameObject.SetActive(false);
    }
    
    public void EnableShootingVirtualCamera()
    {
        _shootingVirtualCamera.gameObject.SetActive(true);
        _drivingVirtualCamera.gameObject.SetActive(false);
    }
}
