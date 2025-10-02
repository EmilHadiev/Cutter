using YG;

public class GameReadyService : IGameReadyService
{
    public void StartGame()
    {
        YG2.GameReadyAPI();
    }

    public void StartGameplay()
    {
        YG2.GameplayStart();
    }

    public void StopGameplay()
    {
        YG2.GameplayStop();
    }
}