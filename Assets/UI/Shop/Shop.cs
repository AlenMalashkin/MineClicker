using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    public event Action<ShopState> ShopStateChanged;
    
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Transform itemContainer;

    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }
    
    private void Start()
    {
        CreateItems();
        CloseShopPanel();
    }

    private void CreateItems()
    {
        ItemType[] itemTypes = (ItemType[])Enum.GetValues(typeof(ItemType));

        foreach (var itemType in itemTypes)
        {
            _gameFactory.CreateShopItem(itemType, itemContainer);
        }
    }

    private void SetShopPanelActive(bool active)
    {
        shopPanel.SetActive(active);
    }

    public void OpenShopPanel()
    {
        shopPanel.transform.DOLocalMoveY(-700f, 0.5f)
            .OnStart(() => SetShopPanelActive(true));
        ShopStateChanged?.Invoke(ShopState.Opened);
    }

    public void CloseShopPanel()
    {
        shopPanel.transform.DOLocalMoveY(-1500f, 0.5f)
            .OnComplete(() => SetShopPanelActive(false));
        ShopStateChanged?.Invoke(ShopState.Closed);
    }
}
