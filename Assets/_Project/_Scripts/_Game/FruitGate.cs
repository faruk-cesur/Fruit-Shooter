using System;
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
    [BoxGroup("Gate Settings"), HideIf(nameof(IsTypeOfLuckGate))] public int GateFruitAmount;
    [BoxGroup("Gate Settings"), Range(2, 10), ShowIf(nameof(IsTypeOfLuckGate))] public int LuckGateDifficulty = 2;
    [BoxGroup("Gate Settings"), Range(1, 100), ShowIf(nameof(IsTypeOfLuckGate))] public int LuckGateMaximumFruitAmount = 100;
    [BoxGroup("Gate Setup")] public FruitGate OtherFruitGate;
    [BoxGroup("Gate Setup"), SerializeField] private TextMeshProUGUI _gateFruitAmountText;
    [BoxGroup("Gate Setup"), SerializeField] private ParticleSystem _fruitGateParticle;

    [HideInInspector] public bool IsGateTriggered;

    private void OnValidate()
    {
        switch (GateType)
        {
            case GateTypes.Add:
                _gateFruitAmountText.text = "+";
                _gateFruitAmountText.text += GateFruitAmount;
                break;
            case GateTypes.Substract:
                _gateFruitAmountText.text = "-";
                _gateFruitAmountText.text += GateFruitAmount;
                break;
            case GateTypes.Luck:
                _gateFruitAmountText.text = "?";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void TriggerFruitGate(int amount)
    {
        switch (GateType)
        {
            case GateTypes.Add:
                AddGate(amount);
                break;
            case GateTypes.Substract:
                SubstractGate(amount);
                break;
            case GateTypes.Luck:
                LuckGate();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PlayFruitGateParticle()
    {
        _fruitGateParticle.transform.SetParent(null);
        _fruitGateParticle.Play();
        Destroy(_fruitGateParticle.gameObject, 5f);
    }

    private bool IsTypeOfLuckGate()
    {
        return GateType == GateTypes.Luck;
    }

    private void AddGate(int amount)
    {
        PlayFruitGateParticle();
        Inventory.OnAddFruit?.Invoke(amount);
    }

    private void SubstractGate(int amount)
    {
        PlayFruitGateParticle(); // fruit kaybedince particle cikmamasi icin bunu sil
        Inventory.OnRemoveFruit?.Invoke(amount);
    }

    private void LuckGate()
    {
        var randomAmount = Random.Range(1f, LuckGateMaximumFruitAmount);
        var randomLuck = Random.Range(0, LuckGateDifficulty); // positive or negative number

        if (randomLuck == 0)
        {
            PlayFruitGateParticle();
            Inventory.OnAddFruit?.Invoke((int)randomAmount);
        }
        else
        {
            PlayFruitGateParticle(); // fruit kaybedince particle cikmamasi icin bunu sil
            Inventory.OnRemoveFruit?.Invoke((int)randomAmount);
        }
    }
}