using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform _aimGunHead;
    [SerializeField] private Transform _fruitBulletSpawnPosition;
    [SerializeField] private List<GameObject> _fruitBulletsPrefabList;
    [SerializeField] private List<GameObject> _spawnedFruitBulletList;

    private GameObject GetRandomFruitBullet()
    {
        var randomFruitBullet = _fruitBulletsPrefabList[Random.Range(0, _fruitBulletsPrefabList.Count)];
        return randomFruitBullet;
    }

    private void SpawnFruitBullet()
    {
        var spawnedFruitBullet = Instantiate(GetRandomFruitBullet(), _fruitBulletSpawnPosition);
        
        _spawnedFruitBulletList.Add(spawnedFruitBullet);
    }
}
