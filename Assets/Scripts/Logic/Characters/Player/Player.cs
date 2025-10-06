using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerEnergy))]
[RequireComponent(typeof(Resurrector))]
[RequireComponent(typeof(ComboSystem))]
[RequireComponent(typeof(PlayerBlinder))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerEnergy _energy;
    [SerializeField] private ComboSystem _comboSystem;
    [SerializeField] private PlayerBlinder _blinder;

    public IMovable Movable => _mover;
    public IHealth Health => _health;
    public IEnergy Energy => _energy;
    public IComboSystem ComboSystem => _comboSystem;
    public IBlindable Blinder => _blinder;
    public PlayerData Data { get; private set; }

    private void OnValidate()
    {
        _mover ??= GetComponent<PlayerMover>();
        _health ??= GetComponent<PlayerHealth>();
        _energy ??= GetComponent<PlayerEnergy>();
        _comboSystem ??= GetComponent<ComboSystem>();
        _blinder ??= GetComponent<PlayerBlinder>();
    }

    [Inject]
    private void Constructor(PlayerData data)
    {
        Data = data;
    }
}