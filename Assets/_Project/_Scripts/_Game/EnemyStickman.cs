using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStickman : EnemyBase
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private FindClosestTarget _findClosestTarget;
    [SerializeField] private Transform _targetToChase;
    [SerializeField] private float _secondsBetweenSideMove;
    private float _randomSideMovement;
    private bool _isEnemyCloseToTheTarget;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(UpdateRandomSideMovement());
        StartCoroutine(LoseWeightBySeconds());
    }

    private void Update()
    {
        switch (GameManager.Instance.CurrentGameState)
        {
            case GameState.None:
                break;
            case GameState.Start:
                break;
            case GameState.Gameplay:
                ChaseTheTarget();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ChaseTheTarget()
    {
        Vector3 targetPosition = new Vector3(_randomSideMovement, 0, _targetToChase.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);

        if (_findClosestTarget.IsClosestTargetNull())
        {
            _isEnemyCloseToTheTarget = false;
        }
        else
        {
            _isEnemyCloseToTheTarget = true;
            _randomSideMovement = 0;
        }
    }

    private void SetBlendShapeWeight()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0, EnemyHealth.StartingHealth - EnemyHealth.CurrentHealth);
    }

    private IEnumerator UpdateRandomSideMovement()
    {
        while (true)
        {
            if (!_isEnemyCloseToTheTarget)
            {
                _randomSideMovement = Random.Range(-5.5f, 5.5f);
                yield return new WaitForSeconds(_secondsBetweenSideMove);
            }

            yield return null;
        }
    }

    private IEnumerator LoseWeightBySeconds()
    {
        while (true)
        {
            EnemyHealth.Heal(1);
            SetBlendShapeWeight();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GainWeight(float damage)
    {
        EnemyHealth.Damage(damage);
        SetBlendShapeWeight();
    }
}