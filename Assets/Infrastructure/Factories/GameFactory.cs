using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private IStaticDataLoadService _staticDataLoadService;
    private IAssets _assets;
    private IPersistentProgressService _persistentProgressService;
    private DiContainer _diContainer;
   
    public List<IProgressUpdater> ProgressUpdaters { get; } = new List<IProgressUpdater>();

    public GameFactory(IStaticDataLoadService staticDataLoadService, IPersistentProgressService persistentProgressService,
        IAssets assets, DiContainer diContainer)
    {
        _staticDataLoadService = staticDataLoadService;
        _persistentProgressService = persistentProgressService;
        _assets = assets;
        _diContainer = diContainer;
    }

    public GameObject CreateEnemy(EnemyType type, Transform at)
    {
        EnemyData enemyData = _staticDataLoadService.ForEnemy(type);

        GameObject enemy = _diContainer.InstantiatePrefab(enemyData.Prefab, at);

        IHealth enemyHealth = enemy.GetComponent<IHealth>();
        
        enemyHealth.MaxHealth += enemyData.Health * _persistentProgressService.Progress.PlayerDpc + + _persistentProgressService.Progress.PlayerDps;
        enemyHealth.CurrentHealth += enemyData.Health * _persistentProgressService.Progress.PlayerDpc + + _persistentProgressService.Progress.PlayerDps;

        ActorUI enemyUI = enemy.GetComponent<ActorUI>();
        enemyUI.Construct(enemyHealth);

        EnemyDeath enemyDeath = enemy.GetComponent<EnemyDeath>();
        enemyDeath.Reward = enemyData.Reward;
        
        return enemy;
    }

    public GameObject CreatePlayer(Transform at)
    {
        GameObject player = InstantiateRegistered("Prefabs/Player/Player", at);
        return player;
    }

    public GameObject CreateHud(Transform at)
    {
        GameObject hud = InstantiateRegistered("Prefabs/HUD/Hud", at);
        return hud;
    }

    public GameObject CreateShopItem(ItemType type, Transform at)
    {
        ItemStaticData itemStaticData = _staticDataLoadService.ForShopItem(type);

        GameObject shopItemPrefab = _assets.GetPrefab("Prefabs/HUD/ShopItems/Item");

        GameObject shopItem = _diContainer.InstantiatePrefab(shopItemPrefab, at);

        shopItem.GetComponent<ItemData>().Construct(itemStaticData, _persistentProgressService);

        return shopItem;
    }

    private GameObject InstantiateRegistered(string prefabPath, Transform at)
    {
        GameObject gameObject = _diContainer.InstantiatePrefab(_assets.GetPrefab(prefabPath), at);
        RegisterProgressWatchers(gameObject);

        return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (IProgressUpdater progressUpdater in gameObject.GetComponentsInChildren<IProgressUpdater>())
        {
            ProgressUpdaters.Add(progressUpdater);
        }
    }
}
