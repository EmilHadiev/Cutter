using Zenject;

public class ButtonRewardContinue : ButtonReward
{
    [Inject]
    private readonly IGameOverService _gameOverService;

    protected override void SetReward() => _gameOverService.Continued();
}