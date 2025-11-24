using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class PlayerEnergy : MonoBehaviour, IEnergy
{
    private const int MinEnergyValue = 1;
    private const int RestoreTime = 1000;

    private int _maxEnergy;
    private int _currentEnergy;

    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    public event Action<int, int> EnergyChanged;

    [Inject]
    private void Constructor(PlayerData data)
    {
        _maxEnergy = data.Energy;
        _currentEnergy = data.Energy;
    }

    public bool TrySpendEnergy()
    {
        if (_currentEnergy < MinEnergyValue)
        {
            Debug.Log("Не могу потратить энергию!");
            return false;
        }

        _currentEnergy -= MinEnergyValue;
        EnergyChanged?.Invoke(_currentEnergy, _maxEnergy);
        return true;
    }

    private void Start()
    {
        AddEnergy().Forget();
    }

    private void OnDestroy()
    {        
        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask AddEnergy()
    {
        try
        {
            while (_cts.Token.IsCancellationRequested == false)
            {
                await UniTask.Delay(RestoreTime, cancellationToken: _cts.Token);

                if (_currentEnergy < _maxEnergy)
                {
                    _currentEnergy += MinEnergyValue;

                    if (_currentEnergy >= _maxEnergy)
                        _currentEnergy = _maxEnergy;

                    EnergyChanged?.Invoke(_currentEnergy, _maxEnergy);
                }
            }
        }
        catch
        {

        }
    }
}