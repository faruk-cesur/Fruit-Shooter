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
    [BoxGroup("Enemy Base")] public float EnemySpeed;
    [BoxGroup("Enemy Base")] public int EnemyDamageAmount;
    private static readonly int Death = Animator.StringToHash("Death");

    protected virtual void Start()
    {
        EnemyHealth.OnDeath += KillFromOverweight;
    }

    protected virtual void OnDestroy()
    {
        EnemyHealth.OnDeath -= KillFromOverweight;
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
}