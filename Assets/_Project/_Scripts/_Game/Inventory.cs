using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float FruitAmount;
    
    public void ChangeItemAmount(ref float itemAmount, float amount)
    {
        itemAmount += amount;
    }
}
