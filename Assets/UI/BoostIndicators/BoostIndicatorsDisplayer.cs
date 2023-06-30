using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoostIndicatorsDisplayer : MonoBehaviour
{
    [SerializeField] private BoostIndicator indicatorPrefab;

    private Dictionary<BoostType, BoostIndicator> _boostIndicators = new Dictionary<BoostType, BoostIndicator>();
    
    private IPlayerBoostService _playerBoostService;
    
    [Inject]
    private void Construct(IPlayerBoostService playerBoostService)
    {
        _playerBoostService = playerBoostService;
    }

    public void OnBoostStarted(BoostType type)
    {
        BoostIndicator indicator = Instantiate(indicatorPrefab, transform);
        IPlayerBooster booster = _playerBoostService.BoostersMap[type];
        indicator.Type = booster.BoosterData.Type;
        indicator.BoostImage.sprite = booster.BoosterData.Sprite;
        _boostIndicators.Add(booster.BoosterData.Type, indicator);
    }

    public void OnBoostTicked(BoostType type)
    {
        IPlayerBooster booster = _playerBoostService.BoostersMap[type];
        float fillValue = Mathf.Clamp(booster.Timer.remainingSeconds / booster.BoosterData.Duration, 0f, 1f);
        _boostIndicators[booster.BoosterData.Type].FillImage(fillValue);
    }

    public void OnBoostEnded(BoostType type)
    {
        IPlayerBooster booster = _playerBoostService.BoostersMap[type];
        Destroy(_boostIndicators[booster.BoosterData.Type].gameObject);
        _boostIndicators.Remove(booster.BoosterData.Type);
    }
}
