using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MoneyBig"))
        {
            Destroy(other.gameObject);
        }

        if (other.TryGetComponent<FruitGate>(out FruitGate fruitGate))
        {
            if (IsGateTriggered(fruitGate))
                return;

            fruitGate.IsGateTriggered = true;
            fruitGate.OtherFruitGate.IsGateTriggered = true;

            Destroy(other.gameObject);
        }
    }

    private bool IsGateTriggered(FruitGate fruitGate)
    {
        return fruitGate.IsGateTriggered || fruitGate.OtherFruitGate.IsGateTriggered;
    }
}