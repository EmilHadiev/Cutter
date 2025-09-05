using System;

public interface IEnergy
{
    /// <summary>
    /// param 1 - current energy ;
    /// param 2 - max energy
    /// </summary>
    event Action<int, int> EnergyChanged;

    bool TrySpendEnergy();
}