using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform _aimGunHead;
    [SerializeField] private SkinnedMeshRenderer _gunMeshRenderer;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerController _playerController;
    private float _bulletReloadTimer;

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
                InputForShoot();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void InputForShoot()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(SpawnBulletFromObjectPool());
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            BlendshapeGunHead();
        }
    }

    private void DecreaseReloadBulletTimer()
    {
        _bulletReloadTimer -= Time.deltaTime;
    }

    private void ResetReloadBulletTimer()
    {
        _bulletReloadTimer = _playerData.BulletReloadTime;
    }

    private void BlendshapeGunHead()
    {
        int blendShape = 0;
        DOTween.To(() => blendShape, x => blendShape = x, 100, 0.25f).SetEase(Ease.Linear).OnUpdate(()=> _gunMeshRenderer.SetBlendShapeWeight(0, blendShape)).OnComplete(() =>
        {
            DOTween.To(() => blendShape, x => blendShape = x, 0, 0.25f).SetEase(Ease.Linear).OnUpdate(() => _gunMeshRenderer.SetBlendShapeWeight(0, blendShape));
        });
    }

    private IEnumerator SpawnBulletFromObjectPool()
    {
        var objectPool = ObjectPool.Instance;
        var randomIndex = Random.Range(0, objectPool.Pools.Length);
        var spawnedRandomBullet = objectPool.GetPooledObject(randomIndex);

        yield return new WaitForSeconds(2f);

        objectPool.SetPooledObject(spawnedRandomBullet, randomIndex);
        spawnedRandomBullet.transform.ResetLocalPos();
        spawnedRandomBullet.transform.ResetLocalRot();
    }
}