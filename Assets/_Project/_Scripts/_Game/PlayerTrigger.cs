using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.Collect(_inventory.GetActiveLastFruitCase);
        }

        if (other.TryGetComponent<FruitGate>(out FruitGate fruitGate))
        {
            if (IsGateTriggered(fruitGate))
                return;

            TriggerGates(fruitGate);
            fruitGate.TriggerFruitGate(fruitGate.GateFruitAmount);
            Inventory.OnChangeFruitAmount?.Invoke();
            Destroy(other.gameObject);
        }

        if (other.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
        {
            if (enemyBase.IsEnemyTriggered)
                return;

            enemyBase.IsEnemyTriggered = true;
            Inventory.OnRemoveFruit?.Invoke(enemyBase.EnemyDamageAmount);
            Inventory.OnChangeFruitAmount?.Invoke();
            enemyBase.ExplodeOnTrigger();
        }
    }

    private bool IsGateTriggered(FruitGate fruitGate)
    {
        return fruitGate.IsGateTriggered || fruitGate.OtherFruitGate.IsGateTriggered;
    }

    private void TriggerGates(FruitGate fruitGate)
    {
        fruitGate.IsGateTriggered = true;
        fruitGate.OtherFruitGate.IsGateTriggered = true;
    }
}