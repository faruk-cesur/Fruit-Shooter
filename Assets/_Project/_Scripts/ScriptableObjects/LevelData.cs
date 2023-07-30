using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public bool IsPlayingFirstTime { get; set; }
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public int FakeLevelHeader { get; set; }
    [field: SerializeField] public List<int> MoneyRewardList { get; set; }
    [field: SerializeField] public List<string> TimeSpentList { get; set; }

    [Button("Reset Levels")]
    public void ResetLevels()
    {
        Level = 1;
        FakeLevelHeader = 1;
        IsPlayingFirstTime = true;
        SaveManager.DeleteData(this.name);
    }

    [Button("Reset Money Reward List")]
    public void ResetMoneyRewardList()
    {
        MoneyRewardList.Clear();
        SaveManager.DeleteData(this.name);
    }

    [Button("Reset Time Spent List")]
    public void ResetTimeSpent()
    {
        TimeSpentList.Clear();
        SaveManager.DeleteData(this.name);
    }
}