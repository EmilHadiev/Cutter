using Zenject;

public class MoneyBonus : Bonus
{
    private const int RewardReducer = 3;

    private IRewardService _rewardService;

    [Inject]
    private void Constructor(IRewardService rewardService)
    {
        _rewardService = rewardService;
    }

    protected override void OnCut() => _rewardService.AddAdditionalReward(GetReward());

    private int GetReward() => _rewardService.StandartReward / RewardReducer;
}