using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField] private List<EnemyBase> _enemyList;

    private void Start()
    {
        DisableAllEnemiesOnStart();
    }

    private void DisableAllEnemiesOnStart()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void EnableAllEnemies()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}
