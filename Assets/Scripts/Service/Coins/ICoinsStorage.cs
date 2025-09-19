using System;

public interface ICoinsStorage
{
    event Action<int> CoinsChanged;

    public int Coins { get; }
    void AddCoins(int coins);
    bool TrySpendCoins(int price);
}