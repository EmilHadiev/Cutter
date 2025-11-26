using UnityEngine;
using Zenject;

public class PlayerHardcoreEnergy : MonoBehaviour
{
    [Inject] private readonly PlayerProgress _progress;
    [Inject] private readonly PlayerData _data;

    public void TrySetEnergy(int points = 1)
    {
        if (_progress.IsHardcoreMode)
            _data.HardcoreEnergy = points;
    }
}