using System.Collections.Generic;
using YG;

public class Saver : ISavable
{
    private readonly ICoinsStorage _coinsStorage;
    private readonly PlayerData _playerData;
    private readonly PlayerProgress _playerProgress;

    private readonly ItemSaver _swordSaver;
    private readonly ItemSaver _particleSaver;

    private SavesYG Saves => YG2.saves;

    public Saver(ICoinsStorage coinsStorage, PlayerData playerData, PlayerProgress playerProgress, IEnumerable<SwordData> swords, IEnumerable<ParticleData> particles)
    {
        _coinsStorage = coinsStorage;
        _playerData = playerData;
        _playerProgress = playerProgress;

        _swordSaver = new ItemSaver(Saves.Swords, swords);
        _particleSaver = new ItemSaver(Saves.Particles, particles);
    }

    public void Save()
    {
        SaveData();

        YG2.SaveProgress();
    }

    public void Load()
    {
        LoadData();
    }

    public void ResetAllProgress() => YG2.SetDefaultSaves();

    private void SaveData()
    {
        SaveCoins();
        SavePlayerData();
        SavePlayerProgress();
        SaveItems();
    }

    private void LoadData()
    {
        LoadCoins();
        LoadPlayerData();
        LoadPlayerProgress();
        LoadItems();
    }

    #region Coins
    private void SaveCoins() => Saves.coins = _coinsStorage.Coins;
    private void LoadCoins() => _coinsStorage.AddCoins(Saves.coins);
    #endregion;

    #region PlayerData
    private void SavePlayerData()
    {
        Saves.CurrentParticle = _playerData.Particle;
        Saves.CurrentSword = _playerData.Sword;
    }

    private void LoadPlayerData()
    {
        _playerData.Particle = Saves.CurrentParticle;
        _playerData.Sword = Saves.CurrentSword;
    }
    #endregion

    #region SwordAndParticleData
    private void SaveItems()
    {
        _swordSaver?.Save();
        _particleSaver?.Save();
    }

    private void LoadItems()
    {
        _swordSaver.Load();
        _particleSaver.Load();
    }
    #endregion

    #region PlayerProgress
    private void SavePlayerProgress()
    {
        Saves.completedLevels = _playerProgress.CurrentLevel;
        Saves.completedHardcoreLevels = _playerProgress.CurrentHardcoreLevel;
        Saves.isHardcoreOpen = _playerProgress.IsHardcoreOpen;
        Saves.HardcoreHealth = _playerData.HardcoreHealth;
        Saves.HardcoreEnergy = _playerData.HardcoreEnergy;
    }

    private void LoadPlayerProgress()
    {
        _playerProgress.CurrentLevel = Saves.completedLevels;
        _playerProgress.CurrentHardcoreLevel = Saves.completedHardcoreLevels;
        _playerProgress.IsHardcoreOpen = Saves.isHardcoreOpen;
        _playerData.HardcoreHealth = Saves.HardcoreHealth;
        _playerData.HardcoreEnergy = Saves.HardcoreEnergy;
    }
    #endregion
}