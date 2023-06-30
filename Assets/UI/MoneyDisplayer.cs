using TMPro;
using UnityEngine;
using Zenject;

public class MoneyDisplayer : MonoBehaviour, IProgressUpdater
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private IPersistentProgressService _persistentProgressService;
    private INumFormatterService _numFormatterService;

    [Inject]
    private void Construct(IPersistentProgressService persistentProgressService, INumFormatterService numFormatterService)
    {
        _persistentProgressService = persistentProgressService;
        _numFormatterService = numFormatterService;
    }

    private void Start()
    {
        UpdateMoneyText(_persistentProgressService.Progress.Money);
    }

    private void UpdateMoneyText(int money)
    {
        moneyText.text = _numFormatterService.FormatNum(money);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        UpdateMoneyText(progress.Money);
    }
}
