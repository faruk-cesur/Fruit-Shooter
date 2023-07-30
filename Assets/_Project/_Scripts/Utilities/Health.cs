using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float StartingHealth { get; set; }
    public float SliderSpeed = 0.5f;
    [field: SerializeField] private Slider HealthSlider { get; set; }
    [field: SerializeField] private Gradient GradientHealthColor { get; set; }
    [field: SerializeField] private Image Fill { get; set; }
    [field: SerializeField] private Image HeartImage { get; set; }
    public UnityAction OnDeath;

    public bool IsDead { get; set; }
    private float _currentHealth;

    public float CurrentHealth
    {
        get => _currentHealth;

        set
        {
            if (IsDead)
                return;

            _currentHealth = value;
            SetHealthSliderValue();

            if (IsHealthLowerThanZero())
            {
                Kill();
            }
        }
    }

    private bool IsHealthLowerThanZero()
    {
        return _currentHealth <= 0;
    }

    private void Start()
    {
        SetStartingHealth();
    }

    private void SetStartingHealth()
    {
        CurrentHealth = StartingHealth;
        HealthSlider.maxValue = StartingHealth;
        HealthSlider.minValue = 0;
        SetHealthSliderValue();
    }

    private void SetHealthSliderValue()
    {
        if (HealthSlider.value <= 0)
            return;

        HealthSlider.DOValue(_currentHealth, SliderSpeed);
        SetGradientHealthColor();
    }

    private void SetGradientHealthColor()
    {
        var targetHealthSliderValue = (HealthSlider.maxValue - CurrentHealth);
        var normalizedSliderValue = Mathf.InverseLerp(HealthSlider.maxValue, 0, targetHealthSliderValue);
        Fill.DOColor(GradientHealthColor.Evaluate(normalizedSliderValue), SliderSpeed);
    }

    public void Damage(float damageAmount)
    {
        if (IsDead)
            return;

        CurrentHealth -= damageAmount;
    }

    public void Heal(float healAmount)
    {
        if (IsDead)
            return;

        CurrentHealth += healAmount;
        if (CurrentHealth > StartingHealth)
        {
            CurrentHealth = StartingHealth;
        }
    }

    private void Kill()
    {
        if (IsDead)
            return;

        IsDead = true;
        _currentHealth = 0;
        OnDeath?.Invoke();
    }
}