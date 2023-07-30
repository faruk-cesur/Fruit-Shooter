using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitGate : MonoBehaviour
{
    public enum GateTypes
    {
        Add,
        Substract,
        Luck
    }

    [BoxGroup("Gate Settings")] public GateTypes GateType;
    [BoxGroup("Gate Settings")] public int GateNumber;
    [BoxGroup("Gate Settings"),Range(1,10)] public int LuckGateDifficulty;
    [BoxGroup("Gate Setup")] public FruitGate OtherFruitGate;
    [BoxGroup("Gate Setup"), SerializeField] private TextMeshProUGUI _gateNumberText;
    [HideInInspector] public bool IsGateTriggered;

    private void OnValidate()
    {
        switch (GateType)
        {
            case GateTypes.Add:
                _gateNumberText.text = "+";
                _gateNumberText.text += GateNumber;
                break;
            case GateTypes.Substract:
                _gateNumberText.text = "-";
                _gateNumberText.text += GateNumber;
                break;
            case GateTypes.Luck:
                _gateNumberText.text = "?";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public float AddGate()
    {
        return GateNumber;
    }
    
    public float SubstractGate()
    {
        return -GateNumber;
    }
    
    public float LuckGate()
    {
        var randomNumber = Random.Range(1, 101);
        var randomLuck = Random.Range(0, LuckGateDifficulty);

        if (randomLuck == 0)
        {
            return randomNumber;
        }
        else
        {
            return -randomNumber;
        }
    }

    // public void TriggerFruitGate(ref float itemAmount, float amount)
    // {
    //     switch (GateType)
    //     {
    //         case GateTypes.Add:
    //             AddGate(ref itemAmount,amount);
    //             break;
    //         case GateTypes.Substract:
    //             SubstractGate(ref itemAmount,amount);
    //             break;
    //         case GateTypes.Luck:
    //             LuckGate(ref itemAmount);
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }
    //
    // private void AddGate(ref float itemAmount, float amountToAdd)
    // {
    //     itemAmount += amountToAdd;
    // }
    //
    // public void SubstractGate(ref float itemAmount, float amountToRemove)
    // {
    //     itemAmount -= amountToRemove;
    // }
    //
    // public void LuckGate(ref float itemAmount)
    // {
    //     var randomNumber = Random.Range(1, 101);
    //     var randomLuck = Random.Range(0, LuckGateDifficulty);
    //
    //     if (randomLuck == 0)
    //     {
    //         itemAmount += randomNumber;
    //     }
    //     else
    //     {
    //         itemAmount -= randomNumber;
    //     }
    // }
}