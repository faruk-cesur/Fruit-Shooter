using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static UnityAction<float> OnAddFruit;
    public static UnityAction<float> OnRemoveFruit;
    public static UnityAction OnChangeFruitAmount;

    [SerializeField, ReadOnly] private float _fruitAmount;
    [SerializeField] private float _fruitCaseRatio = 10f;
    [SerializeField] private List<GameObject> _fruitCaseList;

    private void Start()
    {
        OnAddFruit += AddFruit;
        OnRemoveFruit += RemoveFruit;
        OnChangeFruitAmount += ChangeFruitCaseAmount;
        ChangeFruitCaseAmount();
    }

    public void AddFruit(float amount)
    {
        _fruitAmount += amount;
    }

    public void RemoveFruit(float amount)
    {
        _fruitAmount -= amount;

        if (_fruitAmount < 0)
        {
            _fruitAmount = 0;
        }
    }

    private void ChangeFruitCaseAmount()
    {
        int activeFruitCount = Mathf.RoundToInt(_fruitAmount / _fruitCaseRatio);

        foreach (GameObject fruitCase in _fruitCaseList)
        {
            fruitCase.SetActive(false);
        }

        for (int i = 0; i < activeFruitCount && i < _fruitCaseList.Count; i++)
        {
            _fruitCaseList[i].SetActive(true);
        }
    }
}