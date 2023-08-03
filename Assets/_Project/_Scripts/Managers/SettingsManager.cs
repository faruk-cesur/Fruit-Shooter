using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : Singleton<SettingsManager>
{
    #region Variables

    [field: SerializeField] public CanvasGroup SettingsUI { get; set; }
    [field: SerializeField, BoxGroup("Sound and Vibration")] public GameObject SoundOnButton { get; set; }
    [field: SerializeField, BoxGroup("Sound and Vibration")] public GameObject SoundOffButton { get; set; }
    [field: SerializeField, BoxGroup("Sound and Vibration")] public GameObject VibrationOnButton { get; set; }
    [field: SerializeField, BoxGroup("Sound and Vibration")] public GameObject VibrationOffButton { get; set; }
    [field: SerializeField, BoxGroup("Bullet Damage")] public Slider BulletDamageSlider { get; set; }
    [field: SerializeField, BoxGroup("Shooting Speed")] public Slider BulletReloadDurationSlider { get; set; }
    [field: SerializeField, BoxGroup("Fruit Collect Bonus")] public Slider FruitCollectBonusSlider { get; set; }
    [field: SerializeField, BoxGroup("Enemy Difficulty")] public Slider EnemyDifficultySlider { get; set; }
    [SerializeField] private PlayerData _playerData;

    public bool IsSoundActivated { get; private set; }
    public bool IsVibrationActivated { get; private set; }
    private bool IsSettingsOpened { get; set; }

    #endregion

    #region Get Starting Data

    private void Start() => GetStartingData();

    private void GetStartingData()
    {
        if (!PlayerPrefs.HasKey("SoundSettings"))
        {
            EnableSound(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("SoundSettings") == 0)
            {
                DisableSound();
            }
            else if (PlayerPrefs.GetInt("SoundSettings") == 1)
            {
                EnableSound(false);
            }
        }

        if (!PlayerPrefs.HasKey("VibrationSettings"))
        {
            EnableVibration(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("VibrationSettings") == 0)
            {
                DisableVibration(false);
            }
            else if (PlayerPrefs.GetInt("VibrationSettings") == 1)
            {
                EnableVibration(false);
            }
        }

        if (!PlayerPrefs.HasKey("BulletDamage"))
        {
            BulletDamageSlider.value = 20;
        }
        else
        {
            BulletDamageSlider.value = PlayerPrefs.GetFloat("BulletDamage");
        }

        if (!PlayerPrefs.HasKey("BulletReloadDuration"))
        {
            BulletReloadDurationSlider.value = 0.5f;
        }
        else
        {
            BulletReloadDurationSlider.value = PlayerPrefs.GetFloat("BulletReloadDuration");
        }

        if (!PlayerPrefs.HasKey("CollectedFruitBonus"))
        {
            FruitCollectBonusSlider.value = 1f;
        }
        else
        {
            FruitCollectBonusSlider.value = PlayerPrefs.GetFloat("CollectedFruitBonus");
        }

        if (!PlayerPrefs.HasKey("EnemyDifficulty"))
        {
            EnemyDifficultySlider.value = 1f;
        }
        else
        {
            EnemyDifficultySlider.value = PlayerPrefs.GetFloat("EnemyDifficulty");
        }
    }

    #endregion

    #region Settings

    public void OpenSettingsUI()
    {
        UIManager.Instance.FadeCanvasGroup(SettingsUI, 0.5f, true);
    }

    public void CloseSettingsUI()
    {
        UIManager.Instance.FadeCanvasGroup(SettingsUI, 0.5f, false);
        EditSettings();
    }

    public void EnableSound(bool playAudio)
    {
        SoundOnButton.SetActive(true);
        SoundOffButton.SetActive(false);
        IsSoundActivated = true;
        PlayerPrefs.SetInt("SoundSettings", 1);
        AudioManager.Instance.gameObject.SetActive(true);
    }

    public void DisableSound()
    {
        SoundOnButton.SetActive(false);
        SoundOffButton.SetActive(true);
        IsSoundActivated = false;
        PlayerPrefs.SetInt("SoundSettings", 0);
        AudioManager.Instance.gameObject.SetActive(false);
    }

    public void EnableVibration(bool playAudio)
    {
        VibrationOnButton.SetActive(true);
        VibrationOffButton.SetActive(false);
        IsVibrationActivated = true;
        PlayerPrefs.SetInt("VibrationSettings", 1);
        Vibration.VibratePeek();
    }

    public void DisableVibration(bool playAudio)
    {
        VibrationOnButton.SetActive(false);
        VibrationOffButton.SetActive(true);
        IsVibrationActivated = false;
        PlayerPrefs.SetInt("VibrationSettings", 0);
    }

    public void EditSettings()
    {
        _playerData.BulletDamage = BulletDamageSlider.value;
        _playerData.BulletReloadDuration = BulletReloadDurationSlider.value;
        _playerData.CollectedFruitBonus = (int)FruitCollectBonusSlider.value;
        PlayerPrefs.SetFloat("BulletDamage", BulletDamageSlider.value);
        PlayerPrefs.SetFloat("BulletReloadDuration", BulletReloadDurationSlider.value);
        PlayerPrefs.SetFloat("CollectedFruitBonus", FruitCollectBonusSlider.value);
        PlayerPrefs.SetFloat("EnemyDifficulty", EnemyDifficultySlider.value);
    }

    #endregion
}