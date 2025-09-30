using Zenject;

public class GoldBossReward : BossReward
{
    private const int RewardReducer = 30;

    [Inject]
    private readonly IRewardService _rewardService;

    protected override void SetReward() => 
        _rewardService.AddAdditionalReward(_rewardService.StandartReward / RewardReducer);
}
