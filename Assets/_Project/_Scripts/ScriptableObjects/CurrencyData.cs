using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class CurrencyData : ScriptableObject
{
    [field: SerializeField] public int Money { get; set; }
    [field: SerializeField] public int SecondaryMoney { get; set; }

    [Button("Reset Money")]
    public void ResetMoney()
    {
        Money = 0;
        SecondaryMoney = 0;
        SaveManager.DeleteData(this.name);
    }

    public bool IsMoneyEnough(int targetPrice)
    {
        return Money >= targetPrice;
    }
}