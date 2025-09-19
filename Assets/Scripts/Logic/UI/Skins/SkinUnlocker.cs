using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinUnlocker
{
    private readonly List<SkinTemplateView> _skins;
    private readonly ICoinsStorage _coinsStorage;
    private readonly CoinsCalculator _coinsCalculator;

    public SkinUnlocker(List<SkinTemplateView> skins, ICoinsStorage coinsStorage, CoinsCalculator calculator)
    {
        _skins = skins;
        _coinsStorage = coinsStorage;
        _coinsCalculator = calculator;
    }

    public int GetCurrentPrice()
    {
        int maxSkins = _skins.Count;
        int availableSkins = _skins.Where(s => s.IsLock).Count();

        return _coinsCalculator.GetNewPrice(availableSkins, maxSkins);
    }

    public bool TryUnlock()
    {
        if (_coinsStorage.TrySpendCoins(GetCurrentPrice()))
        {
            GetRandomSkin().Unlock();
            return true;
        }
        return false;
    }

    private SkinTemplateView GetRandomSkin()
    {
        SkinTemplateView[] lockSkins = _skins.Where(s => s.IsLock == false).ToArray();

        int random = Random.Range(0, lockSkins.Length);
        return lockSkins[random];
    }
}