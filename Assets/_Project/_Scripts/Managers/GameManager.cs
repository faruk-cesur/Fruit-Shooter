using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum GameState
{
    None,
    Start,
    Gameplay,
    Win,
    Lose
}

public class GameManager : Singleton<GameManager>
{
    #region Variables

    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public UnityAction OnGameStart;
    public UnityAction OnGameplay;
    public UnityAction OnGameWin;
    public UnityAction OnGameLose;

    [ReadOnly] public GameState CurrentGameState;
    [SerializeField] private bool _isLevelMoneyRewardActive = true;
    [SerializeField] private bool _isEarnMoneyOnLoseActive = true;

    #endregion

    private void Awake()
    {
        //SingleEventSystem();
        SetTargetFrameRate(60);
        InitVibration();
        SetTweensCapacity();
    }

    private void Start() => StartGame();

    #region Game State Functions

    public void ChangeGameState(GameState newGameState)
    {
        if (CurrentGameState == newGameState)
            return;

        OnBeforeStateChanged?.Invoke(newGameState);

        CurrentGameState = newGameState;

        switch (newGameState)
        {
            case GameState.None:
                break;
            case GameState.Start:
                OnGameStart?.Invoke();
                UIManager.Instance.SetStartUI();
                UIManager.Instance.PrintFakeLevelHeader();
                UIManager.Instance.PrintTotalMoneyText();
                break;
            case GameState.Gameplay:
                OnGameplay?.Invoke();
                UIManager.Instance.SetGameplayUI();
                break;
            case GameState.Win:
                OnGameWin?.Invoke();
                UIManager.Instance.SetWinUI();
                Vibration.VibratePeek();
                break;
            case GameState.Lose:
                OnGameLose?.Invoke();
                UIManager.Instance.SetLoseUI();
                Vibration.VibratePeek();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }

        OnAfterStateChanged?.Invoke(newGameState);

        Debug.Log($"New state: {newGameState}");
    }

    public void StartGame()
    {
        ChangeGameState(GameState.Start);
    }

    public void Gameplay()
    {
        ChangeGameState(GameState.Gameplay);
    }

    public void Win(int earnedMoney)
    {
        if (CurrentGameState is GameState.Win or GameState.Lose)
            return;

        ChangeGameState(GameState.Win);

        if (_isLevelMoneyRewardActive)
        {
            int moneyReward = earnedMoney + LevelManager.Instance.MoneyReward;
            CurrencyManager.Instance.EarnMoney(moneyReward);
            UIManager.Instance.PrintEarnedMoneyText(moneyReward, UIManager.Instance.EarnedMoneyTextOnWin);
        }
        else
        {
            CurrencyManager.Instance.EarnMoney(earnedMoney);
            UIManager.Instance.PrintEarnedMoneyText(earnedMoney, UIManager.Instance.EarnedMoneyTextOnWin);
        }
    }

    public void Lose(int earnedMoney)
    {
        if (CurrentGameState is GameState.Lose or GameState.Win)
            return;

        ChangeGameState(GameState.Lose);

        if (_isEarnMoneyOnLoseActive)
        {
            CurrencyManager.Instance.EarnMoney(earnedMoney);
            UIManager.Instance.PrintEarnedMoneyText(earnedMoney, UIManager.Instance.EarnedMoneyTextOnLose);
        }
    }

    #endregion

    #region General Functions

    public void SetTargetFrameRate(int targetFrameRate)
    {
        Application.targetFrameRate = targetFrameRate;
    }

    private void InitVibration()
    {
        Vibration.Init();
    }

    private void SetTweensCapacity()
    {
        DOTween.SetTweensCapacity(3000, 50);
    }

    private void SingleEventSystem()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            eventSystem.transform.SetParent(gameObject.transform);
        }
    }

    #endregion
}