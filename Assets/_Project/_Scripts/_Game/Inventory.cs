using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField, BoxGroup("Inventory Setup")] private Transform _fruitBackground;
    [SerializeField, BoxGroup("Inventory Setup")] private PlayerData _playerData;
    [SerializeField, BoxGroup("Inventory Setup")] public Transform CollectableTargetPosition;

    private void Start()
    {
        OnAddFruit += AddFruit;
        OnRemoveFruit += RemoveFruit;
        OnChangeFruitAmount += ChangeFruitCaseAmount;
        GameManager.Instance.OnGameLose += DisableFruitBackground;
        GameManager.Instance.OnGameWin += DisableFruitBackground;
        ChangeFruitCaseAmount();
    }

    private void OnDestroy()
    {
        OnAddFruit -= AddFruit;
        OnRemoveFruit -= RemoveFruit;
        OnChangeFruitAmount -= ChangeFruitCaseAmount;
        GameManager.Instance.OnGameLose -= DisableFruitBackground;
        GameManager.Instance.OnGameWin -= DisableFruitBackground;
    }

    public void AddFruit(int amount)
    {
        _fruitAmount = amount * _playerData.CollectedFruitBonus;
    }

    public void RemoveFruit(int amount)
    {
        _fruitAmount -= amount;

        if (_fruitAmount <= 0)
        {
            _fruitAmount = 0;
            GameManager.Instance.Lose(0);
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

    public void SetFruitBackgroundShootingStance()
    {
        _fruitBackground.transform.DOLocalMove(new Vector3(1.25f, 5, -1), 1f);
        _fruitBackground.transform.DOScale(0.5f, 1f);
    }

    private void DisableFruitBackground()
    {
        _fruitBackground.gameObject.SetActive(false);
    }
}