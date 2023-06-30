using YG;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BonusWindow : MonoBehaviour
{
    [SerializeField] private int adId;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button showRewardAdButton;
    [SerializeField] private GameObject bonusWindow;

    private YandexGame _yandexGame;
    private IPlayerBoostService _playerBoostService;

    [Inject]
    private void Construct(YandexGame yandexGame, IPlayerBoostService playerBoostService)
    {
        _yandexGame = yandexGame;
        _playerBoostService = playerBoostService;
    }
    
    private void OnEnable()
    {
        openButton.onClick.AddListener(OpenWindow);
        closeButton.onClick.AddListener(CloseWindow);
        showRewardAdButton.onClick.AddListener(ShowRewardAd);
        YandexGame.CloseVideoEvent += OnRewardVideoClosed;
        YandexGame.CheaterVideoEvent += OnCheaterVideo;
    }

    private void OnDisable()
    {
        openButton.onClick.RemoveListener(OpenWindow);
        closeButton.onClick.RemoveListener(CloseWindow);
        showRewardAdButton.onClick.RemoveListener(ShowRewardAd);
        YandexGame.CloseVideoEvent -= OnRewardVideoClosed;
        YandexGame.CheaterVideoEvent -= OnCheaterVideo;
    }

    private void OpenWindow()
    {
        bonusWindow.SetActive(true);
    }

    private void CloseWindow()
    {
        bonusWindow.SetActive(false);
    }

    private void ShowRewardAd()
    {
        _yandexGame._RewardedShow(adId);
    }

    private void OnRewardVideoClosed(int rewAdId)
    {
        if (rewAdId == adId)
            Reward();
        
        CloseWindow();
    }

    private void OnCheaterVideo()
    {
        Time.timeScale = 1;
        CloseWindow();
    }

    private void Reward()
    {
        _playerBoostService.UseBooster(BoostType.DamagePerClickBoost);
    }
}
