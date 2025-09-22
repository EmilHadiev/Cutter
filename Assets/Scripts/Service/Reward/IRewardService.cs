public interface IRewardService
{
    int AdditionalReward { get; }
    int StandartReward { get; }

    void GiveReward();
    void AddAdditionalReward(int coins);
}