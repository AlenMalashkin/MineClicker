using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProgress
{
    public int KillCount;
    public int PlayerDpc;
    public int PlayerDps;
    public int Money;
    public List<ItemSaveData> Items;
}
