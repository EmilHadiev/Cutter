public class RewardService : IRewardService
{
    private const int DefaultReward = 60;

    private readonly IComboSystem _comboSystem;
    private readonly ICoinsStorage _coinsStorage;

    private int _totalReward;

    public int StandartReward => DefaultReward;
    public int AdditionalReward => _totalReward;

    public RewardService(IPlayer player, ICoinsStorage coinsStorage)
    {
        _comboSystem = player.ComboSystem;
        _coinsStorage = coinsStorage;
        _totalReward = 0;
    }

    public void GiveReward()
    {
        _totalReward += _comboSystem.GetComboReward;
        _coinsStorage.AddCoins(DefaultReward + AdditionalReward);
    }

    public void AddAdditionalReward(int coins)
    {
        _totalReward += coins;
    }
}