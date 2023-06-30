using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private HudMenu hudMenu;
    [SerializeField] private Shop shop;
    [SerializeField] private BoostIndicatorsDisplayer boostIndicatorsDisplayer;
    [SerializeField] private PlayerStatsDisplayer playerStatsDisplayer;

    public BoostIndicatorsDisplayer BoostIndicatorsDisplayer => boostIndicatorsDisplayer;
    public PlayerStatsDisplayer PlayerStatsDisplayer => playerStatsDisplayer;

    private void OnEnable()
    {
        shop.ShopStateChanged += OnShopStateChanged;
    }

    private void OnDisable()
    {
        shop.ShopStateChanged -= OnShopStateChanged;
    }

    private void OnShopStateChanged(ShopState state)
    {
        switch (state)
        {
            case ShopState.Opened:
                hudMenu.CloseHudMenu();
                break;
            case ShopState.Closed:
                hudMenu.OpenHudMenu();
                break;
        }
    }
}
