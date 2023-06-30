using System.Collections.Generic;

public interface IPlayerBoostService
{
    Dictionary<BoostType, IPlayerBooster> BoostersMap { get; }
    void InitBoostersMap();
    void UseBooster(BoostType type);
}
