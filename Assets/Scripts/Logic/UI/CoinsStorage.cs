using System;

public class CoinsStorage : ICoinsStorage
{
    public int Coins { get; private set; }

    public event Action<int> CoinsChanged;

    public bool TrySpendCoins(int price)
    {
        if (IsValidValue(price) == false)
            return false;

        if (price <= Coins)
        {
            Coins -= price;
            CoinsChanged?.Invoke(Coins);
            return true;
        }

        return false;
    }

    public void AddCoins(int coins)
    {
        if (IsValidValue(coins) == false)
            return;

        Coins += coins;
        CoinsChanged?.Invoke(Coins);
    }

    private bool IsValidValue(int value) => value >= 0;
}