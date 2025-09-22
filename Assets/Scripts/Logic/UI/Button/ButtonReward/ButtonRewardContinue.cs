﻿using Zenject;

public class ButtonRewardContinue : ButtonReward
{
    private IGameOverService _gameOverService;
    private IRewardService _rewardService;

    [Inject]
    private void Constructor(IGameOverService gameOverService, IRewardService rewardService)
    {
        _gameOverService = gameOverService;
        _rewardService = rewardService;
    }

    protected override void SetReward()
    {
        _rewardService.AddAdditionalReward(GetRewardCoins());
        _gameOverService.Continued();
        HideAfterSetReward();
    }

    private void HideAfterSetReward()
    {
        gameObject.SetActive(false);
    }

    private int GetRewardCoins() => _rewardService.StandartReward;
}