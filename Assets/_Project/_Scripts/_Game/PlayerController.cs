using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    public PlayerStates PlayerState;

    public enum PlayerStates
    {
        DriveAndCollectFruits,
        DriveAndShootEnemies
    }

    #region Movement Variables

    [SerializeField, BoxGroup("Movement Settings")] private float _sideMovementSensitivity = 2f;
    [SerializeField, BoxGroup("Movement Settings"), Range(0, 1)] private float _defaultForwardMovementSpeed = 1f;
    [SerializeField, BoxGroup("Movement Settings"), Range(0, 1)] private float _defaultSideMovementSpeed = 1f;
    [SerializeField, BoxGroup("Movement Setup")] private Transform _sideMovementRoot;
    [SerializeField, BoxGroup("Movement Setup")] private Transform _sideMovementLeftLimit, _sideMovementRightLimit;
    private float LeftLimitX => _sideMovementLeftLimit.localPosition.x;
    private float RightLimitX => _sideMovementRightLimit.localPosition.x;
    private float _sideMovementTarget = 0f;
    public float ForwardMoveSpeed => _defaultForwardMovementSpeed * _playerData.BonusForwardMovementSpeed;

    #endregion

    #region Input Variables

    [HideInInspector] public Vector2 InputDrag;
    private Vector2 _previousMousePosition;

    private Vector2 MousePositionCm
    {
        get
        {
            Vector2 pixels = Input.mousePosition;
            var inches = pixels / Screen.dpi;
            var centimeters = inches * 2.54f;

            return centimeters;
        }
    }

    #endregion


    private void Update()
    {
        switch (GameManager.Instance.CurrentGameState)
        {
            case GameState.None:
                break;
            case GameState.Start:
                break;
            case GameState.Gameplay:
                switch (PlayerState)
                {
                    case PlayerStates.DriveAndCollectFruits:
                        HandleInput();
                        HandleForwardMovement();
                        HandleSideMovement();
                        break;
                    case PlayerStates.DriveAndShootEnemies:
                        HandleForwardMovement();
                        HandleSideMovement();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #region Movement

    private void HandleForwardMovement()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * _defaultForwardMovementSpeed * _playerData.BonusForwardMovementSpeed), Space.Self);
    }

    private void HandleSideMovement()
    {
        _sideMovementTarget += InputDrag.x * _sideMovementSensitivity;
        _sideMovementTarget = Mathf.Clamp(_sideMovementTarget, LeftLimitX, RightLimitX);

        var localPos = _sideMovementRoot.localPosition;

        localPos.x = Mathf.Lerp(localPos.x, _sideMovementTarget, Time.deltaTime * _defaultSideMovementSpeed * _playerData.BonusSideMovementSpeed);

        _sideMovementRoot.localPosition = localPos;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _previousMousePosition = MousePositionCm;
        }

        if (Input.GetMouseButton(0))
        {
            var deltaMouse = MousePositionCm - _previousMousePosition;
            //InputDrag = deltaMouse; todo input daha smooth olsun diye lerp koydum eger oynanis zorlanirsa eski haline getir.
            InputDrag = Vector2.Lerp(InputDrag, deltaMouse, Time.deltaTime * 30);
            _previousMousePosition = MousePositionCm;
        }
        else
        {
            InputDrag = Vector2.zero;
        }
    }

    public void SetSideMoveLimits(float leftSideLimit, float rightSideLimit, float duration)
    {
        _sideMovementLeftLimit.DOLocalMoveX(leftSideLimit, duration);
        _sideMovementRightLimit.DOLocalMoveX(rightSideLimit, duration);
    }

    public void ResetSideMovementRootPosition()
    {
        _sideMovementRoot.ResetLocalPos();
        _sideMovementRoot.ResetLocalRot();
    }

    #endregion
}