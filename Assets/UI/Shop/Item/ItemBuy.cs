using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemBuy : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemUI itemUI;

    private IPersistentProgressService _persistentProgressService;
    private ISaveService _saveService;
    private IBank _bank;

    [Inject]
    private void Construct(IPersistentProgressService persistentProgressService, ISaveService saveService, IBank bank)
    {
        _persistentProgressService = persistentProgressService;
        _saveService = saveService;
        _bank = bank;
    }

    private void OnEnable()
    {
        itemUI.UpdateUI(button, _bank.Money);
        
        button.onClick.AddListener(BuyItem);
        _bank.MoneyValueChanged += OnMoneyValueChanged;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(BuyItem);
        _bank.MoneyValueChanged -= OnMoneyValueChanged;
    }

    private void BuyItem()
    {
        if (_bank.SpendMoney(itemData.ItemSaveData.Cost))
        {
            itemData.ItemSaveData.Id = itemData.ItemStaticData.Id;
            itemData.ItemSaveData.Cost += itemData.ItemSaveData.Cost;
            itemData.ItemSaveData.Level += 1;
            itemData.ItemSaveData.DamagePerClick += (itemData.ItemStaticData.BaseDpcAddPerLevel + itemData.ItemSaveData.DamagePerClick);
            itemData.ItemSaveData.DamagePerSecond += (itemData.ItemStaticData.BaseDpsAddPerLevel + itemData.ItemSaveData.DamagePerSecond);

            if (!_persistentProgressService.Progress.Items.Contains(itemData.ItemSaveData))
                _persistentProgressService.Progress.Items.Add(itemData.ItemSaveData);
            
            _saveService.SaveProgress();
            itemUI.UpdateUI(button, _bank.Money);
        }
    }

    private void OnMoneyValueChanged(int money)
    {
        itemUI.UpdateUI(button, money);
    }
}
