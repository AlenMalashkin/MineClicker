using System;
using VavilichevGD.Utils.Timing;

public class PlayerDpcBooster : IPlayerBooster
{
    public BoosterData BoosterData { get; }
    public SyncedTimer Timer { get; }

    private ISceneObjectsProvider _sceneObjectsProvider;
    private PlayerAttack _playerAttack;
    private Hud _hud;
    
    public PlayerDpcBooster(BoosterData boosterData, ISceneObjectsProvider sceneObjectsProvider)
    {
        BoosterData = boosterData;
        _sceneObjectsProvider = sceneObjectsProvider;
        Timer = new SyncedTimer(TimerType.UpdateTick);
    }
    
    public void StartBoost()
    {
        Timer.Start(BoosterData.Duration);
        Timer.TimerFinished += EndBoost;
        Timer.TimerTicked += TickBoost;
        
        _playerAttack = _sceneObjectsProvider.SceneObjectsMap[typeof(Player)].GetComponent<PlayerAttack>();
        _hud = _sceneObjectsProvider.SceneObjectsMap[typeof(Hud)].GetComponent<Hud>();
        
        _playerAttack.OnDpcBoostStarted();
        _hud.PlayerStatsDisplayer.UpdateText();
        _hud.BoostIndicatorsDisplayer.OnBoostStarted(BoosterData.Type);
    }

    public void TickBoost()
    {
        _hud.BoostIndicatorsDisplayer.OnBoostTicked(BoosterData.Type);
    }

    public void EndBoost()
    {
        _playerAttack.OnDpcBoostEnded();
        _hud.PlayerStatsDisplayer.UpdateText();
        _hud.BoostIndicatorsDisplayer.OnBoostEnded(BoosterData.Type);
        Timer.TimerFinished -= EndBoost;
        Timer.TimerTicked -= TickBoost;
    }
}
