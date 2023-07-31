using System;
using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectableTypes
    {
        Money,
        Fruit
    }

    public CollectableTypes CollectableType;
    [SerializeField] private ParticleSystem _collectableParticle;
    [SerializeField] private int _collectableAmount;
    private bool _isCollected;

    public void Collect(Transform targetTransformForAnimation)
    {
        if (_isCollected)
            return;

        _isCollected = true;

        switch (CollectableType)
        {
            case CollectableTypes.Money:
                CurrencyManager.Instance.EarnMoney(_collectableAmount);
                break;
            case CollectableTypes.Fruit:
                Inventory.OnAddFruit?.Invoke(_collectableAmount);
                Inventory.OnChangeFruitAmount?.Invoke();
                CollectAnimation(targetTransformForAnimation);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        PlayCollectableParticle();
    }

    private void PlayCollectableParticle()
    {
        UnparentParticle();
        _collectableParticle.Play();
        Destroy(_collectableParticle.gameObject, 5f);
    }

    private void UnparentParticle()
    {
        _collectableParticle.transform.SetParent(null);
    }

    private void CollectAnimation(Transform targetTransform)
    {
        transform.SetParent(targetTransform);
        transform.DOLocalRotate(Vector3.zero, 0.5f);
        transform.DOLocalJump(Vector3.zero, 5f, 1, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}