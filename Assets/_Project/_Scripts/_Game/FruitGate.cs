using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

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
}