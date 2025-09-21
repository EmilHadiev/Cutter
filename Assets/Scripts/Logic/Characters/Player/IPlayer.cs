public interface IPlayer
{
    IMovable Movable { get; }
    IHealth Health { get; }
    IEnergy Energy { get; }
    PlayerData Data { get; }
    IComboSystem ComboSystem { get; }
}