using System.Collections.Generic;

public class PlayerBoostService : IPlayerBoostService
{
    public Dictionary<BoostType, IPlayerBooster> BoostersMap { get; }

    private IStaticDataLoadService _staticDataLoadService;
    private ISceneObjectsProvider _sceneObjectsProvider;
    
    public PlayerBoostService(IStaticDataLoadService staticDataLoadService, ISceneObjectsProvider sceneObjectsProvider)
    {
        _staticDataLoadService = staticDataLoadService;
        _sceneObjectsProvider = sceneObjectsProvider;
        BoostersMap = new Dictionary<BoostType, IPlayerBooster>();
    }

    public void InitBoostersMap()
    {
        List<IPlayerBooster> boosters = new List<IPlayerBooster>()
        {
            new PlayerDpcBooster(_staticDataLoadService.ForBooster(BoostType.DamagePerClickBoost), _sceneObjectsProvider),
        };
        
        foreach (var booster in boosters)
        {
            BoostersMap.Add(booster.BoosterData.Type, booster);
        }
    }

    public void UseBooster(BoostType type)
    {
        BoostersMap[type].StartBoost();
    }
}
