using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerEnergy))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerEnergy _energy;

    private ICameraFollower _cameraFollower;

    public IMovable Movable => _mover;
    public IHealth Health => _health;
    public IEnergy Energy => _energy;
    public PlayerData Data { get; private set; }

    private void OnValidate()
    {
        _mover ??= GetComponent<PlayerMover>();
        _health ??= GetComponent<PlayerHealth>();
        _energy ??= GetComponent<PlayerEnergy>();
    }

    private void Start()
    {
        _cameraFollower.Follow(transform);
    }

    [Inject]
    private void Constructor(ICameraFollower follower, PlayerData data)
    {
        _cameraFollower = follower;
        Data = data;
    }
}