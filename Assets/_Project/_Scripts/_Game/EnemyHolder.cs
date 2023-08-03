using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField] private List<EnemyBase> _enemyList;
    public static UnityAction OnKillFromOverweight;

    private void Start()
    {
        DisableAllEnemiesOnStart();
        OnKillFromOverweight += TriggerWinAfterEnemiesKilled;
    }

    private void OnDestroy()
    {
        OnKillFromOverweight -= TriggerWinAfterEnemiesKilled;
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
            enemy.EnemySpeedRatio = 1000;
        }

        SetEnemySpeedRatio();
    }

    public void RemoveEnemyFromList(EnemyBase enemy)
    {
        _enemyList.Remove(enemy);
        if (CheckIfEnemyListEmpty())
        {
            GameManager.Instance.Lose(0);
        }
    }

    private void SetEnemySpeedRatio()
    {
        foreach (var enemy in _enemyList)
        {
            DOTween.To(() => enemy.EnemySpeedRatio, x => enemy.EnemySpeedRatio = x, 50, 10).SetEase(Ease.Linear);
        }
    }

    private void TriggerWinAfterEnemiesKilled()
    {
        if (CheckIfEnemyListKilledFromOverweight())
        {
            GameManager.Instance.Win(0);
        }
    }

    private bool CheckIfEnemyListEmpty()
    {
        return _enemyList.Count == 0;
    }

    private bool CheckIfEnemyListKilledFromOverweight()
    {
        foreach (var enemy in _enemyList)
        {
            if (!enemy.IsEnemyKilled)
            {
                return false;
            }
        }

        return true;
    }
}