using UnityEngine;

public class SaveService : ISaveService
{
    private const string Key = "Progress";

    private IPersistentProgressService _persistentProgressService;
    private IGameFactory _gameFactory;

    public SaveService(IPersistentProgressService persistentProgressService, IGameFactory gameFactory)
    {
        _persistentProgressService = persistentProgressService;
        _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
        foreach (var progressUpdater in _gameFactory.ProgressUpdaters)
        {
            progressUpdater.UpdateProgress(_persistentProgressService.Progress);
        }
        
        string json = JsonUtility.ToJson(_persistentProgressService.Progress);
        PlayerPrefs.SetString(Key, json);
    }

    public PlayerProgress LoadProgress()
        => JsonUtility.FromJson<PlayerProgress>(PlayerPrefs.GetString(Key));
}
