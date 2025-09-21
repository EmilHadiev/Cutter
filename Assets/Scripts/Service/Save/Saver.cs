using YG;

public class Saver : ISavable
{
    private readonly ICoinsStorage _coinsStorage;

    public Saver(ICoinsStorage coinsStorage)
    {
        _coinsStorage = coinsStorage;
    }

    public void Save()
    {
        LoadData();

        YG2.SaveProgress();
    }

    public void Load()
    {
        LoadCoins();
    }

    public void ResetAllProgress() => YG2.SetDefaultSaves();

    private void LoadData()
    {
        SaveCoins();
    }

    private void SaveCoins() => YG2.saves.coins = _coinsStorage.Coins;
    private void LoadCoins() => _coinsStorage.AddCoins(YG2.saves.coins);
}