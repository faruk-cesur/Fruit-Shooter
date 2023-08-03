using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform _aimGunHead;

    //[SerializeField] private Transform _fruitBulletSpawnPosition;

    //[SerializeField] private List<GameObject> _fruitBulletsPrefabList;
    //[SerializeField] private List<GameObject> _spawnedFruitBulletList;

    // private GameObject GetRandomFruitBullet()
    // {
    //     var randomFruitBullet = _fruitBulletsPrefabList[Random.Range(0, _fruitBulletsPrefabList.Count)];
    //     return randomFruitBullet;
    // }
    //
    // private void SpawnFruitBullet()
    // {
    //     var spawnedFruitBullet = Instantiate(GetRandomFruitBullet(), _fruitBulletSpawnPosition);
    //
    //     _spawnedFruitBulletList.Add(spawnedFruitBullet);
    // }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var objectPool = ObjectPool.Instance;
            var randomIndex = Random.Range(0, objectPool.Pools.Length);
            var spawnedRandomBullet = objectPool.GetPooledObject(randomIndex);
            StartCoroutine(ReloadBulletCoroutine(spawnedRandomBullet, randomIndex));
        }
    }

    private IEnumerator ReloadBulletCoroutine(GameObject bullet, int index)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.Instance.SetPooledObject(bullet, index);
        bullet.transform.ResetLocalPos();
        bullet.transform.ResetLocalRot();
    }
}