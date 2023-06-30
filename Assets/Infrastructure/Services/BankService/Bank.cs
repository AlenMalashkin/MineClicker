using System;

public class Bank : IBank
{
    public event Action<int> MoneyValueChanged;

    private readonly IPersistentProgressService _persistentProgressService;
    private readonly ISaveService _saveService;
    
    public int Money 
    {
        get => _persistentProgressService.Progress.Money;
        set => _persistentProgressService.Progress.Money = value;
    }

    public Bank(IPersistentProgressService persistentProgressService, ISaveService saveService)
    {
        _persistentProgressService = persistentProgressService;
        _saveService = saveService;
    }
    
    public void AddMoney(int amount)
    {
        Money += amount;
        _saveService.SaveProgress();
        MoneyValueChanged?.Invoke(Money);
    }

    public bool SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            _saveService.SaveProgress();
            MoneyValueChanged?.Invoke(Money);
            return true;
        }

        return false;
    }
}
