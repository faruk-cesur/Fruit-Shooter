using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform _aimGunHead;
    [SerializeField] private SkinnedMeshRenderer _gunMeshRenderer;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _gunAimSensitivity = 0.3f;
    private float _durationBetweenBullets;
    private float _gunAimX = 0f;
    private float _gunAimY = 0f;

    private void Update()
    {
        switch (GameManager.Instance.CurrentGameState)
        {
            case GameState.None:
                break;
            case GameState.Start:
                break;
            case GameState.Gameplay:
                Shoot();
                DecreaseReloadBulletTimer();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Shoot()
    {
        switch (_playerController.PlayerState)
        {
            case PlayerController.PlayerStates.DriveAndCollectFruits:
                break;
            case PlayerController.PlayerStates.DriveAndShootEnemies:
                InputForAimAndShoot();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void InputForAimAndShoot()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Moved:
                    _gunAimX += touch.deltaPosition.x * _gunAimSensitivity;
                    _gunAimY += touch.deltaPosition.y * _gunAimSensitivity;
                    StartCoroutine(SpawnBulletFromObjectPool());
                    break;
                case TouchPhase.Stationary:
                    StartCoroutine(SpawnBulletFromObjectPool());
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        _gunAimY = Mathf.Clamp(_gunAimY, -20, 20);
        _gunAimX = Mathf.Clamp(_gunAimX, -45, 45);

        _aimGunHead.transform.localRotation = Quaternion.Euler(-_gunAimY, _gunAimX, 0);
    }

    private void DecreaseReloadBulletTimer()
    {
        _durationBetweenBullets -= Time.deltaTime;
    }

    private void ResetDurationBetweenBullets()
    {
        _durationBetweenBullets = _playerData.DurationBetweenBullets;
    }

    private void BlendshapeGunHead()
    {
        int blendShape = 0;
        DOTween.To(() => blendShape, x => blendShape = x, 100, 0.2f).SetEase(Ease.Linear).OnUpdate(() => _gunMeshRenderer.SetBlendShapeWeight(0, blendShape)).OnComplete(() => { DOTween.To(() => blendShape, x => blendShape = x, 0, 0.2f).SetEase(Ease.Linear).OnUpdate(() => _gunMeshRenderer.SetBlendShapeWeight(0, blendShape)); });
    }

    private IEnumerator SpawnBulletFromObjectPool()
    {
        while (_durationBetweenBullets > 0)
        {
            yield break;
        }

        ResetDurationBetweenBullets();
        var objectPool = ObjectPool.Instance;
        var randomIndex = Random.Range(0, objectPool.Pools.Length);
        var spawnedRandomBullet = objectPool.GetPooledObject(randomIndex);
        //spawnedRandomBullet.transform.SetParent(null); // todo it gives null ref?!
        BlendshapeGunHead();

        yield return new WaitForSeconds(2f);

        objectPool.SetPooledObject(spawnedRandomBullet, randomIndex);
        //spawnedRandomBullet.transform.SetParent(objectPool.transform); 
        spawnedRandomBullet.transform.ResetLocalPos();
        spawnedRandomBullet.transform.ResetLocalRot();
    }
}