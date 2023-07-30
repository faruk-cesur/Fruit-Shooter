using System.Collections;
using System.Globalization;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region Variables

    [BoxGroup("GAME STATE UI"), SerializeField] private CanvasGroup _startUI;
    [BoxGroup("GAME STATE UI"), SerializeField] private CanvasGroup _gameplayUI;
    [BoxGroup("GAME STATE UI"), SerializeField] private CanvasGroup _winUI;
    [BoxGroup("GAME STATE UI"), SerializeField] private CanvasGroup _loseUI;

    [BoxGroup("TEXT SETUP"), SerializeField] private TextMeshProUGUI _levelText;
    [BoxGroup("TEXT SETUP"), SerializeField] private TextMeshProUGUI _totalMoneyText;
    [BoxGroup("TEXT SETUP"), SerializeField] public TextMeshProUGUI EarnedMoneyTextOnWin;
    [BoxGroup("TEXT SETUP"), SerializeField] public TextMeshProUGUI EarnedMoneyTextOnLose;

    [BoxGroup("GAMEPLAY UI SETUP"), SerializeField] private CanvasGroup _levelTextCanvasGroup;
    [BoxGroup("GAMEPLAY UI SETUP"), SerializeField] private CanvasGroup _moneyBackgroundCanvasGroup;

    #endregion

    #region Print Functions

    public string GetShortenedNumber(float number)
    {
        string formattedNumber;

        if (number >= 1000 && number < 1000000)
        {
            formattedNumber = (number / 1000f).ToString("F2") + "K"; // Kısaltma için "K" ekleniyor
        }
        else if (number >= 1000000 && number < 1000000000)
        {
            formattedNumber = (number / 1000000f).ToString("F2") + "M"; // Kısaltma için "M" ekleniyor
        }
        else if (number >= 1000000000)
        {
            formattedNumber = (number / 1000000000f).ToString("F2") + "B"; // Kısaltma için "B" ekleniyor
        }
        else
        {
            formattedNumber = number.ToString(CultureInfo.InvariantCulture); // Dört basamaktan daha küçükse kısaltma yapılmıyor
        }

        return formattedNumber;
    }

    public void PrintTotalMoneyText()
    {
        _totalMoneyText.text = GetShortenedNumber(CurrencyManager.Instance.GetCurrencyData.Money);
    }

    public void PrintEarnedMoneyText(int earnedMoney, TextMeshProUGUI earnedMoneyText)
    {
        earnedMoneyText.text = "+" + GetShortenedNumber(earnedMoney);
    }

    public void PrintFakeLevelHeader()
    {
        _levelText.text = "LEVEL " + LevelManager.Instance.GetLevelData.FakeLevelHeader;
    }

    #endregion

    #region Set UI Canvas Groups

    private IEnumerator SetUIMenu(GameObject menu, float time, bool trueOrFalse)
    {
        yield return new WaitForSeconds(time);
        menu.SetActive(trueOrFalse);
    }

    private void FadeCanvasGroup(CanvasGroup canvasGroup, float duration, bool enableUI)
    {
        if (enableUI)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1, duration);
        }
        else
        {
            canvasGroup.DOFade(0, duration).OnComplete(() => { canvasGroup.gameObject.SetActive(false); });
        }
    }

    public void SetStartUI()
    {
        FadeCanvasGroup(_startUI, 1f, true);
        FadeCanvasGroup(_gameplayUI, 0, false);
        FadeCanvasGroup(_winUI, 0, false);
        FadeCanvasGroup(_loseUI, 0, false);
    }

    public void SetGameplayUI()
    {
        FadeCanvasGroup(_startUI, 0, false);
        FadeCanvasGroup(_gameplayUI, 1f, true);
        FadeCanvasGroup(_winUI, 0, false);
        FadeCanvasGroup(_loseUI, 0, false);
    }

    public void SetWinUI()
    {
        FadeCanvasGroup(_startUI, 0, false);
        FadeCanvasGroup(_gameplayUI, 0, false);
        FadeCanvasGroup(_winUI, 1f, true);
        FadeCanvasGroup(_loseUI, 0, false);
    }

    public void SetLoseUI()
    {
        FadeCanvasGroup(_startUI, 0, false);
        FadeCanvasGroup(_gameplayUI, 0, false);
        FadeCanvasGroup(_winUI, 0, false);
        FadeCanvasGroup(_loseUI, 1f, true);
    }

    #endregion
}