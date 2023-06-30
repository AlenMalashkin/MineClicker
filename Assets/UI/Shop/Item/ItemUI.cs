using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image coinIcon;
    [SerializeField] private Image dpcIcon;
    [SerializeField] private Image dpsIcon;
    
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI dpcText;
    [SerializeField] private TextMeshProUGUI dpsText;

    private INumFormatterService _numFormatterService;

    [Inject]
    private void Construct(INumFormatterService numFormatterService)
    {
        _numFormatterService = numFormatterService;
    }

    private void Awake()
    {
        itemIcon.sprite = itemData.ItemStaticData.Icon;
    }

    private bool CheckItemEnabledToBuy(int money)
        => itemData.ItemSaveData.Cost <= money;

    private void DisableItemToBuy()
    {
        itemIcon.color = new Color32(255, 255, 255, 128);
        coinIcon.color = new Color32(255, 255, 255, 128);
        dpcIcon.color = new Color32(255, 255, 255, 128);
        dpsIcon.color = new Color32(255, 255, 255, 128);
    }

    private void EnableItemToBuy()
    {
        itemIcon.color = new Color32(255, 255, 255, 255);
        coinIcon.color = new Color32(255, 255, 255, 255);
        dpcIcon.color = new Color32(255, 255, 255, 255);
        dpsIcon.color = new Color32(255, 255, 255, 255);
    }

    private void EnableOrDisableItemToBuy(int money, Button button)
    {
        if (CheckItemEnabledToBuy(money))
        {
            button.interactable = true;
            EnableItemToBuy();
        }
        else
        {
            button.interactable = false;
            DisableItemToBuy();
        }
    }

    public void UpdateUI(Button button, int money)
    {
        int dpc = itemData.ItemSaveData.DamagePerClick + itemData.ItemStaticData.BaseDpcAddPerLevel;
        int dps = itemData.ItemSaveData.DamagePerSecond + itemData.ItemStaticData.BaseDpsAddPerLevel;
        
        levelText.text = "Level: " + _numFormatterService.FormatNum(itemData.ItemSaveData.Level);
        costText.text = _numFormatterService.FormatNum(itemData.ItemSaveData.Cost);
        dpcText.text = "DPC: +" + _numFormatterService.FormatNum(dpc);
        dpsText.text = "DPS: +" + _numFormatterService.FormatNum(dps);

        EnableOrDisableItemToBuy(money, button);
    }
}
