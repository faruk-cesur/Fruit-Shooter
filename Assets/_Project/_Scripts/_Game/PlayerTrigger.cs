using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    
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

            switch (fruitGate.GateType)
            {
                case FruitGate.GateTypes.Add:
                    _inventory.ChangeItemAmount(ref _inventory.FruitAmount,fruitGate.AddGate());
                    break;
                case FruitGate.GateTypes.Substract:
                    _inventory.ChangeItemAmount(ref _inventory.FruitAmount,fruitGate.SubstractGate());
                    break;
                case FruitGate.GateTypes.Luck:
                    _inventory.ChangeItemAmount(ref _inventory.FruitAmount,fruitGate.LuckGate());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            //fruitGate.TriggerFruitGate(ref _inventory.FruitAmount,fruitGate.GateNumber);

            Destroy(other.gameObject);
        }
    }

    private bool IsGateTriggered(FruitGate fruitGate)
    {
        return fruitGate.IsGateTriggered || fruitGate.OtherFruitGate.IsGateTriggered;
    }
}