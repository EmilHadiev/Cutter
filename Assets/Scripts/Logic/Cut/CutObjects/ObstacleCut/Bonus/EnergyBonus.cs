using Zenject;

public class EnergyBonus : Bonus
{
    private PlayerData _data;
    private IEnergy _energy;

    private const int AdditionalEnergy = 1;

    [Inject]
    private void Constructor(IPlayer player)
    {
        _data = player.Data;
        _energy = player.Energy;
    }

    protected override void OnCut()
    {
        _energy.TryAddEnergy(AdditionalEnergy);
    }
}