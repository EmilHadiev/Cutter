public class RewardCalculator
{
    private readonly PlayerProgress _progress;

    private const int RewardPerLevel = 10;

    public RewardCalculator(PlayerProgress progress)
    {
        _progress = progress;
    }

    public float GetPercent()
    {
        int currentLevel = _progress.IsHardcoreMode 
            ? _progress.CurrentHardcoreLevel 
            : _progress.CurrentLevel;

        if (currentLevel == 0) return 0;

        int progressInCycle = currentLevel % RewardPerLevel;

        if (progressInCycle == 0)
        {
            return 1f;
        }

        return (float)progressInCycle / RewardPerLevel;
    }
}