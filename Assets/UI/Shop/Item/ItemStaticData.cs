using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData", fileName = "ItemData", order = 4)]
public class ItemStaticData : ScriptableObject
{
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] private int baseCost;
    [SerializeField] private int baseDpcAddPerLevel = 1;
    [SerializeField] private int baseDpsAddPerLevel = 1;

    public ItemType Type => type;
    public Sprite Icon => icon;
    public string Id => id;
    public int BaseCost => baseCost;
    public int BaseDpcAddPerLevel => baseDpcAddPerLevel;
    public int BaseDpsAddPerLevel => baseDpsAddPerLevel;
}
