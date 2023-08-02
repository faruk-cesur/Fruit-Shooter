using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class EnemyBase : MonoBehaviour
{
    [BoxGroup("Enemy Base"), HideInInspector] public bool IsEnemyExplode;
    [BoxGroup("Enemy Base"), HideInInspector] public bool IsEnemyKilled;
    [BoxGroup("Enemy Base"), SerializeField] private ParticleSystem _enemyParticle;
    [BoxGroup("Enemy Base"), SerializeField] private Animator _enemyAnimator;
    [BoxGroup("Enemy Base")] public Health EnemyHealth;
    [BoxGroup("Enemy Base")] protected float EnemySpeed;
    [BoxGroup("Enemy Base")] public float EnemySpeedRatio;
    [BoxGroup("Enemy Base")] public int EnemyDamageAmount;
    private static readonly int Death = Animator.StringToHash("Death");

    protected virtual void Start()
    {
        EnemyHealth.OnDeath += KillFromOverweight;
        EnemyHealth.OnChangeHealth += SetEnemySpeed;
        GameManager.Instance.OnGameLose += ExplodeOnTrigger;
    }

    protected virtual void OnDestroy()
    {
        EnemyHealth.OnDeath -= KillFromOverweight;
        EnemyHealth.OnChangeHealth -= SetEnemySpeed;
        GameManager.Instance.OnGameLose -= ExplodeOnTrigger;
    }

    public void ExplodeOnTrigger()
    {
        IsEnemyExplode = true;
        PlayEnemyParticle();
        Destroy(gameObject);
    }

    private void KillFromOverweight()
    {
        IsEnemyKilled = true;
        EnemySpeed = 0;
        _enemyAnimator.SetTrigger(Death);
        EnemyHolder.OnKillFromOverweight?.Invoke();
    }

    private void PlayEnemyParticle()
    {
        UnparentParticle();
        _enemyParticle.Play();
        Destroy(_enemyParticle.gameObject, 5f);
    }

    private void UnparentParticle()
    {
        _enemyParticle.transform.SetParent(null);
    }
    
    private void SetEnemySpeed()
    {
        if (EnemyHealth.CurrentHealth > 60)
        {
            EnemySpeed = EnemyHealth.CurrentHealth / EnemySpeedRatio;
        }
        else if (EnemyHealth.CurrentHealth < 60 && EnemyHealth.CurrentHealth >= 30)
        {
            EnemySpeed = 0;
        }
        else if (EnemyHealth.CurrentHealth < 30 && EnemyHealth.CurrentHealth >= 0)
        {
            EnemySpeed = -1f;
        }
    }
}