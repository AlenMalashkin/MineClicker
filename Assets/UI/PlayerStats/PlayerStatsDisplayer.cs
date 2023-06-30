using System;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerStatsDisplayer : MonoBehaviour, IProgressUpdater
{
    [SerializeField] private TextMeshProUGUI killCountStatText;
    [SerializeField] private TextMeshProUGUI dpcStatText;
    [SerializeField] private TextMeshProUGUI dpsStatText;

    private IPersistentProgressService _persistentProgressService;
    private INumFormatterService _numFormatterService;
    private ISceneObjectsProvider _sceneObjectsProvider;
    private PlayerAttack _playerAttack;

    private bool _dpcBoostActivated;
    
    [Inject]
    private void Construct(IPersistentProgressService persistentProgressService, INumFormatterService numFormatterService, ISceneObjectsProvider sceneObjectsProvider)
    {
        _persistentProgressService = persistentProgressService;
        _numFormatterService = numFormatterService;
        _sceneObjectsProvider = sceneObjectsProvider;
    }
    
    private void Start()
    {
        _playerAttack = _sceneObjectsProvider.SceneObjectsMap[typeof(Player)].GetComponent<PlayerAttack>();
        UpdateText();
    }

    public void UpdateText()
    {
        killCountStatText.text = _numFormatterService.FormatNum(_persistentProgressService.Progress.KillCount);
        dpcStatText.text = "DPC: " + _numFormatterService.FormatNum(_playerAttack.DamagePerClick);
        dpsStatText.text = "DPS: " + _numFormatterService.FormatNum(_playerAttack.DamagePerSecond);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        UpdateText();
    }
}
