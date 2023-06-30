using System.Collections.Generic;
using UnityEngine;

public class LoadProgressService : ILoadProgressService
{
    private ISaveService _saveService;
    private IPersistentProgressService _persistentProgressService;
    
    public LoadProgressService(ISaveService saveService, IPersistentProgressService persistentProgressService)
    {
        _saveService = saveService;
        _persistentProgressService = persistentProgressService;
    }
    
    public void LoadOrInitNewProgress()
    {
        _persistentProgressService.Progress = _saveService.LoadProgress() ?? InitNewPlayerProgress();
    }

    private PlayerProgress InitNewPlayerProgress()
    {
        return new PlayerProgress
        {
            Money = 0, 
            KillCount = 0,
            PlayerDpc = 1,
            PlayerDps = 0,
            Items = new List<ItemSaveData>()
        };
    }
}
