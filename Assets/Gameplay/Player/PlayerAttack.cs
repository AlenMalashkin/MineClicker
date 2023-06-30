using UnityEngine;
using VavilichevGD.Utils.Timing;
using Zenject;

public class PlayerAttack : MonoBehaviour, IProgressUpdater
{
    public int DamagePerClick
    {
        get => Mathf.RoundToInt(_damagePerClick * _damagePerClickMultiplier);
    }

    public int DamagePerSecond
    {
        get => Mathf.RoundToInt(_damagePerSecond * _damagePerSecondMultiplier);
    }

    [SerializeField] private int baseDamagePerClick;
    [SerializeField] private int baseDamagePerSecond;
    [SerializeField] private float damagePerClickMultiplier = 1;
    [SerializeField] private float damagePerSecondMultiplier = 1;
    
    private int _damagePerClick;
    private int _damagePerSecond;
    private float _damagePerClickMultiplier;
    private float _damagePerSecondMultiplier;

    private SyncedTimer _timer;
    private IPlayerClickHandlerService _playerClickHandlerService;
    private IPersistentProgressService _persistentProgressService;
    private IHealth _damageable;

    [Inject]
    private void Construct(IPlayerClickHandlerService playerClickHandlerService, IPersistentProgressService persistentProgressService)
    {
        _playerClickHandlerService = playerClickHandlerService;
        _persistentProgressService = persistentProgressService;
    }

    private void Awake()
    {
        _damagePerClickMultiplier = damagePerClickMultiplier;
        _damagePerSecondMultiplier = damagePerSecondMultiplier;
        
        SetupAttackDamage(_persistentProgressService.Progress);
        _timer = new SyncedTimer(TimerType.OneSecTick);
        _timer.Start(1);
    }

    private void OnEnable()
    {
        _timer.TimerFinished += OnOneSecTick;
        _playerClickHandlerService.PlayerClickHandled += OnClickHandler;
    }

    private void OnDisable()
    {
        _playerClickHandlerService.PlayerClickHandled -= OnClickHandler;
        _timer.TimerFinished -= OnOneSecTick;
    }

    private void OnClickHandler()
    {
        _damageable?.TakeDamage(DamagePerClick);
    }

    private void OnOneSecTick()
    {
        if (DamagePerSecond != 0)
            _damageable?.TakeDamage(DamagePerSecond);
        
        _timer.Start(1);
    }

    private void SetupAttackDamage(PlayerProgress progress)
    {
        _damagePerClick = baseDamagePerClick;
        _damagePerSecond = baseDamagePerSecond;

        foreach (var item in progress.Items)
        {
            _damagePerClick += item.DamagePerClick;
            _damagePerSecond += item.DamagePerSecond;
        }
    }

    public void OnDpcBoostStarted()
    {
        _damagePerClickMultiplier = 2;
    }

    public void OnDpcBoostEnded()
    {
        _damagePerClickMultiplier = damagePerClickMultiplier;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        SetupAttackDamage(progress);
        _persistentProgressService.Progress.PlayerDpc = _damagePerClick;
        _persistentProgressService.Progress.PlayerDps = _damagePerSecond;
    }

    public void SetAttackTarget(IHealth damageable)
    {
        _damageable = damageable;
    }
}
