using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    #region Variables

    [field: SerializeField, BoxGroup("LEVEL DATA")] public LevelData GetLevelData { get; private set; }
    [field: SerializeField, BoxGroup("SETTINGS")] public int FirstLevelAfterLoop { get; private set; }
    [HideInInspector] public int MoneyReward;
    private float _timeSpent;
    private Tween _timeTween;

    #endregion

    private void Awake()
    {
        FirstLevelAfterLoopWarning();
        GameManager.Instance.OnGameStart += LoadLevel;
        GameManager.Instance.OnGameplay += StartTimeCounterTween;
        GameManager.Instance.OnGameWin += LevelUp;
        GameManager.Instance.OnGameWin += StopTimeCounterTween;
        GameManager.Instance.OnGameLose += StopTimeCounterTween;
    }

    #region Time Counter

    private void StartTimeCounterTween()
    {
        _timeSpent = 0;
        _timeTween = DOTween.To(() => _timeSpent, x => _timeSpent = x, 1, 1).SetLoops(-1, LoopType.Incremental);
    }

    private void StopTimeCounterTween()
    {
        GetLevelData.TimeSpentList.Add("Level " + (GetLevelData.Level - 1) + " Time Spent = (" + Math.Round(_timeSpent, 2) + ")");
        SaveManager.Instance.SaveData(GetLevelData, GetLevelData.name);
        _timeTween.Kill();
    }

    #endregion

    #region Level Functions

    private void FirstLevelAfterLoopWarning()
    {
        if (FirstLevelAfterLoop <= 0)
        {
            FirstLevelAfterLoop = 1;
            Debug.LogWarning("Set (First Level After Loop) on Level Manager!");
        }
    }

    private int LevelCompleteMoneyReward()
    {
        // If money reward exists for current level, return money value.
        if (GetLevelData.MoneyRewardList.Count >= GetLevelData.Level + 1)
        {
            return GetLevelData.MoneyRewardList[GetLevelData.Level - 1];
        }

        // If money reward doesn't exist for current level, create a new one and return last index of money value.

        if (GetLevelData.MoneyRewardList.Count <= 0)
        {
            GetLevelData.MoneyRewardList.Add(MoneyReward);
        }

        return GetLevelData.MoneyRewardList[^1];
    }

    private void LoadLevel()
    {
        SaveManager.Instance.LoadData(GetLevelData, GetLevelData.name);
        SetFirstLevelNumbers();
        LoopInfiniteLevels();
        SceneManager.LoadScene(GetLevelData.Level);
    }

    private void LoopInfiniteLevels()
    {
        // If completed all levels, restart from first level.
        if (GetLevelData.Level + 1 > SceneManager.sceneCountInBuildSettings)
        {
            GetLevelData.Level = FirstLevelAfterLoop;
        }
    }

    private void SetFirstLevelNumbers()
    {
        // Make sure to open Level 1 on first play.
        if (GetLevelData.Level <= 0)
        {
            GetLevelData.Level = 1;
        }

        if (GetLevelData.FakeLevelHeader <= 0)
        {
            GetLevelData.FakeLevelHeader = 1;
        }
    }

    private void LevelUp()
    {
        MoneyReward = LevelCompleteMoneyReward();
        GetLevelData.Level++;
        GetLevelData.FakeLevelHeader++;
        SaveManager.Instance.SaveData(GetLevelData, GetLevelData.name);
    }

    #endregion
}