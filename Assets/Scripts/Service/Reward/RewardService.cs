public class RewardService : IRewardService
{
    private const int DefaultReward = 60;

    private readonly IComboSystem _comboSystem;
    private readonly ICoinsStorage _coinsStorage;

    public int StandartReward => DefaultReward;
    public int AdditionalReward => _comboSystem.GetComboReward;

    public RewardService(IPlayer player, ICoinsStorage coinsStorage)
    {
        _comboSystem = player.ComboSystem;
        _coinsStorage = coinsStorage;
    }

    public void GiveReward() => _coinsStorage.AddCoins(DefaultReward + AdditionalReward);
}