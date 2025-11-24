using Zenject;

public class EnergyBonus : Bonus
{
    private PlayerData _data;

    private const int AdditionalEnergy = 1;

    [Inject]
    private void Constructor(IPlayer player)
    {
        _data = player.Data;
    }

    protected override void OnCut()
    {
        _data.Energy += AdditionalEnergy;
    }
}