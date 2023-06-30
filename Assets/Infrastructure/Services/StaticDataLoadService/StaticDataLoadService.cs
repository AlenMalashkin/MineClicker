using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataLoadService : IStaticDataLoadService
{
    private Dictionary<EnemyType, EnemyData> _enemyStaticDataMap;
    private Dictionary<Scene, SceneData> _sceneStaticDataMap;
    private Dictionary<ItemType, ItemStaticData> _itemStaticDataMap;
    private Dictionary<BoostType, BoosterData> _boosterStaticDataMap;

    public void LoadData()
    {
        _enemyStaticDataMap = Resources.LoadAll<EnemyData>("StaticData/Enemy")
            .ToDictionary(x => x.Type, x => x);

        _sceneStaticDataMap = Resources.LoadAll<SceneData>("StaticData/Scene")
            .ToDictionary(x => x.Scene, x => x);

        _itemStaticDataMap = Resources.LoadAll<ItemStaticData>("StaticData/ShopItems")
            .ToDictionary(x => x.Type, x => x );

        _boosterStaticDataMap = Resources.LoadAll<BoosterData>("StaticData/PlayerBoosters")
            .ToDictionary(x => x.Type, x => x);
    }

    public EnemyData ForEnemy(EnemyType type)
        => _enemyStaticDataMap[type];

    public SceneData ForScene(Scene scene)
        => _sceneStaticDataMap[scene];

    public ItemStaticData ForShopItem(ItemType type)
        => _itemStaticDataMap[type];

    public BoosterData ForBooster(BoostType type)
        => _boosterStaticDataMap[type];
}