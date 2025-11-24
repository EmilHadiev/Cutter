using Zenject;

public class HealthBonus : Bonus
{
    private const int AdditionalHealth = 1;

    private PlayerData _data;
    private IHealth _playerHealth;

    [Inject]
    private void Constructor(IPlayer player)
    {
        _data = player.Data;
        _playerHealth = player.Health;
    }

    protected override void OnCut()
    {
        _data.HardcoreHealth += AdditionalHealth;
        _playerHealth.AddHealth(AdditionalHealth);
    }
}