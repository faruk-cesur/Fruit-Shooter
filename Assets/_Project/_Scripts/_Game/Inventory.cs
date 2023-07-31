using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static UnityAction<int> OnAddFruit;
    public static UnityAction<int> OnRemoveFruit;
    public static UnityAction OnChangeFruitAmount;

    [SerializeField, ReadOnly, BoxGroup("Inventory Debug")] private int _fruitAmount;
    [SerializeField, BoxGroup("Inventory Settings")] private float _fruitCaseRatio = 10f;
    [SerializeField, BoxGroup("Inventory Setup")] private List<GameObject> _fruitCaseList;
    [SerializeField, BoxGroup("Inventory Setup")] private TextMeshProUGUI _fruitAmountText;

    private void Start()
    {
        OnAddFruit += AddFruit;
        OnRemoveFruit += RemoveFruit;
        OnChangeFruitAmount += ChangeFruitCaseAmount;
        ChangeFruitCaseAmount();
    }

    public void AddFruit(int amount)
    {
        _fruitAmount += amount;
    }

    public void RemoveFruit(int amount)
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
        
        SetFruitAmountText();
    }

    private void SetFruitAmountText()
    {
        _fruitAmountText.text = _fruitAmount.ToString();
    }
}