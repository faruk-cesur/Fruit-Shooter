using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStickman : EnemyBase
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private FindClosestTarget _findClosestTarget;
    [SerializeField] private Transform _targetToChase;
    [SerializeField] private Transform _enemyModel;
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
        ChaseTheTarget();
    }

    private void ChaseTheTarget()
    {
        Vector3 targetForwardPosition = Vector3.zero;
        Vector3 targetSidePosition = new Vector3(_targetToChase.localPosition.x + _randomSideMovement, 0, 0);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetForwardPosition, EnemySpeed * Time.deltaTime);
        _enemyModel.transform.localPosition = Vector3.MoveTowards(_enemyModel.transform.localPosition, targetSidePosition, 10 * Time.deltaTime);

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
                _randomSideMovement = Random.Range(-3f, 3f);
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
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GainWeight(float damage)
    {
        EnemyHealth.Damage(damage);
        SetBlendShapeWeight();
    }
}