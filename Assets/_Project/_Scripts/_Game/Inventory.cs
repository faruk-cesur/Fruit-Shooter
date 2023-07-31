using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public float FruitAmount;

    public static UnityAction<float> OnAddFruit;
    public static UnityAction<float> OnRemoveFruit;

    private void Start()
    {
        OnAddFruit += AddFruit;
        OnRemoveFruit += RemoveFruit;
    }

    public void AddFruit(float amount)
    {
        FruitAmount += amount;
    }

    public void RemoveFruit(float amount)
    {
        FruitAmount -= amount;

        if (FruitAmount < 0)
        {
            FruitAmount = 0;
        }
    }
}