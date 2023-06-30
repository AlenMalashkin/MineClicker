using System;

public interface IBank
{
    event Action<int> MoneyValueChanged;

    int Money { get; set; }

    void AddMoney(int amount);
    bool SpendMoney(int amount);
}
