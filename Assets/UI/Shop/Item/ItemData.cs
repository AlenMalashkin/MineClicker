using UnityEngine;

public class ItemData : MonoBehaviour
{
    public ItemStaticData ItemStaticData => _itemStaticData;
    public ItemSaveData ItemSaveData => _itemSaveData;

    private IPersistentProgressService _persistentProgressService;
    private ItemStaticData _itemStaticData;
    private ItemSaveData _itemSaveData;
    
    public void Construct(ItemStaticData itemStaticData, IPersistentProgressService persistentProgressService)
    {
        _itemStaticData = itemStaticData;
        _persistentProgressService = persistentProgressService;
        InitOrCreateNewItemSaveData();
    }
    
    private void InitOrCreateNewItemSaveData()
    {
        _itemSaveData = _persistentProgressService.Progress.Items.Find(x => x.Id == _itemStaticData.Id)
                        ?? InitNewItemSaveData();
    }

    private ItemSaveData InitNewItemSaveData()
    {
        return new ItemSaveData()
        {
            Cost = _itemStaticData.BaseCost,
            DamagePerClick = 0,
            DamagePerSecond = 0,
            Id = _itemStaticData.Id
        };
    }
}
