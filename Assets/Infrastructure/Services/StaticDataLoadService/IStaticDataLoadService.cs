public interface IStaticDataLoadService
{
    void LoadData();
    EnemyData ForEnemy(EnemyType type);
    SceneData ForScene(Scene scene);
    ItemStaticData ForShopItem(ItemType type);
    BoosterData ForBooster(BoostType type);
}
