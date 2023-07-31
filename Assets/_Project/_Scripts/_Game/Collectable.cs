using System;
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

    public void Collect()
    {
        switch (CollectableType)
        {
            case CollectableTypes.Money:
                CurrencyManager.Instance.EarnMoney(_collectableAmount);
                break;
            case CollectableTypes.Fruit:
                Inventory.OnAddFruit?.Invoke(_collectableAmount);
                Inventory.OnChangeFruitAmount?.Invoke();
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
        Destroy(_collectableParticle.gameObject,5f);
    }

    private void UnparentParticle()
    {
        _collectableParticle.transform.SetParent(null);
    }
}