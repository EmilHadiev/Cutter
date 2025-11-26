using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerHardcoreEnergy))]
public class PlayerEnergy : MonoBehaviour, IEnergy
{
    [SerializeField] private PlayerHardcoreEnergy _hardcoreEnergy;

    private const int MinEnergyValue = 1;
    private const int RestoreTime = 1000;

    private int _maxEnergy;
    private int _currentEnergy;

    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    public event Action<int, int> EnergyChanged;

    [Inject]
    private void Constructor(PlayerData data, PlayerProgress progress)
    {
        if (progress.IsHardcoreMode)
            SetEnergy(data.HardcoreEnergy);
        else
            SetEnergy(data.Energy);

        Debug.Log(progress.IsHardcoreMode);
    }

    private void OnValidate()
    {
        _hardcoreEnergy ??= GetComponent<PlayerHardcoreEnergy>();
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

    public void TryAddEnergy(int energy = 1)
    {
        _maxEnergy += energy;
        _hardcoreEnergy.TrySetEnergy(_maxEnergy);
        SetEnergy(_maxEnergy);
    }

    private void SetEnergy(int energy)
    {
        _maxEnergy = energy;
        _currentEnergy = energy;

        Debug.Log($"Current energy {_maxEnergy}");
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