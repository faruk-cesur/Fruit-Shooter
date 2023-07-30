using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField, BoxGroup("Player Setup")] public Transform PlayerModel;
    [SerializeField, BoxGroup("Player Setup")] private PlayerController _playerController;
    [SerializeField, BoxGroup("Wheels")] private Transform _wheelFrontLeft;
    [SerializeField, BoxGroup("Wheels")] private Transform _wheelFrontRight;
    [SerializeField, BoxGroup("Wheels")] private Transform _wheelBackLeft;
    [SerializeField, BoxGroup("Wheels")] private Transform _wheelBackRight;
    [SerializeField, BoxGroup("Wheels")] private Transform _wheelFrontLeftParent;
    [SerializeField, BoxGroup("Wheels")] private Transform _wheelFrontRightParent;
    [SerializeField, BoxGroup("Wheel Settings")] private float _wheelRotationSpeed = 10f;
    [SerializeField, BoxGroup("Wheel Settings")] private float _wheelRotationLimited = 35f;
    [SerializeField, BoxGroup("Wheel Settings")] private float _mouseInputSpeedNew = 500f;
    private List<Transform> _wheels = new List<Transform>();
    private float _mousePositionX;
    private Vector3 _wheelSpinDirection;

    private void Start()
    {
        SetListOfWheels();
        _wheelSpinDirection = new Vector3(transform.right.x, 0, 0);
    }

    private void Update()
    {
        switch (_playerController.PlayerState)
        {
            case PlayerController.PlayerStates.DriveAndCollectFruits:
                SpinWheels();
                WheelRotationInput();
                WheelRotation();
                break;
            case PlayerController.PlayerStates.DriveAndShootEnemies:
                SpinWheels();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetListOfWheels()
    {
        if (_wheels.Count == 4)
            return;
        _wheels.Clear();
        _wheels.Add(_wheelFrontLeft);
        _wheels.Add(_wheelFrontRight);
        _wheels.Add(_wheelBackLeft);
        _wheels.Add(_wheelBackRight);
    }

    private void SpinWheels()
    {
        foreach (var wheel in _wheels)
        {
            wheel.Rotate(_wheelSpinDirection * (50 * _playerController.ForwardMoveSpeed * Time.deltaTime));
        }
    }

    private void WheelRotationInput()
    {
        _mousePositionX = Mathf.Clamp(_playerController.InputDrag.x * _mouseInputSpeedNew, -_wheelRotationLimited, _wheelRotationLimited);
    }

    private void WheelRotation()
    {
        if (Input.GetMouseButton(0))
        {
            _wheelFrontLeftParent.localRotation = Quaternion.Lerp(_wheelFrontLeftParent.localRotation, Quaternion.Euler(0, _mousePositionX, 0), Time.deltaTime * _wheelRotationSpeed);
            _wheelFrontRightParent.localRotation = Quaternion.Lerp(_wheelFrontRightParent.localRotation, Quaternion.Euler(0, _mousePositionX, 0), Time.deltaTime * _wheelRotationSpeed);
            PlayerModel.localRotation = Quaternion.Lerp(PlayerModel.localRotation, Quaternion.Euler(0, _mousePositionX, 0), Time.deltaTime * _wheelRotationSpeed);
        }
        else
        {
            _wheelFrontLeftParent.localRotation = Quaternion.Lerp(_wheelFrontLeftParent.localRotation, Quaternion.identity, Time.deltaTime * _wheelRotationSpeed);
            _wheelFrontRightParent.localRotation = Quaternion.Lerp(_wheelFrontRightParent.localRotation, Quaternion.identity, Time.deltaTime * _wheelRotationSpeed);
            PlayerModel.localRotation = Quaternion.Lerp(PlayerModel.localRotation, Quaternion.identity, Time.deltaTime * _wheelRotationSpeed);
        }
    }

    // public void WheelDestroy()
    // {
    //     foreach (var wheel in _wheels)
    //     {
    //         wheel.DOScale(wheel.transform.localScale * 1.3f, 2f).OnComplete(() =>
    //         {
    //             wheel.gameObject.SetActive(false);
    //             transform.parent.DOShakeRotation(0.5f, 30f);
    //             transform.parent.DOLocalMoveY(-0.5f, 1f);
    //         });
    //     }
    // }
}