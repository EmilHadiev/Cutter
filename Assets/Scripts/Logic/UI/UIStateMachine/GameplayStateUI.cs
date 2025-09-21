using Zenject;

public class GameplayStateUI : UiState
{
    [Inject]
    private readonly IGameStarter _start;

    public override void Show()
    {
        base.Show();
        _start.Start();
    }
}