using System;

public class CoinsCalculator
{
    private const int PlayerSkins = 2;

    public readonly int CoinsReward = 350;
    public readonly int StartPrice = 1000;
    public readonly int AdditionalPrice = 1000;

    public int GetNewPrice(int availableSkins, int maxSkins)
    {
        // Не учитываем первые 2 покупки
        int effectivePurchases = Math.Max(0, availableSkins - PlayerSkins);

        // Вычисляем цену: стартовая цена + (количество эффективных покупок * дополнительная цена)
        return StartPrice + (effectivePurchases * AdditionalPrice);
    }
}