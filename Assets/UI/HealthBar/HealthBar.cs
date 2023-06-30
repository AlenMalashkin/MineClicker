using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image filler;
    [SerializeField] private TextMeshProUGUI hp;

    private INumFormatterService _numFormatterService;

    [Inject]
    private void Construct(INumFormatterService numFormatterService)
    {
        _numFormatterService = numFormatterService;
    }

    public void UpdateHealthBar(int healthCurrent, int healthMax)
    {
        filler.fillAmount = (float) healthCurrent / healthMax;
        hp.text = _numFormatterService.FormatNum(healthCurrent) + " / " + _numFormatterService.FormatNum(healthMax);
    }
}
