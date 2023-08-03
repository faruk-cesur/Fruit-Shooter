using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private PlayerData _playerData;


    private void Update()
    {
        MoveBulletForward();
    }

    private void MoveBulletForward()
    {
        transform.Translate(Vector3.forward * (_bulletSpeed * Time.deltaTime),Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<EnemyStickman>(out EnemyStickman enemyBase))
        {
            enemyBase.GainWeight(_playerData.FruitBulletDamage);
        }
    }
}
