using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.Collect();
            Destroy(other.gameObject);
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