using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerHealth _health;

    private void OnValidate()
    {
        _mover ??= GetComponent<PlayerMover>();
        _health ??= GetComponent<PlayerHealth>();
    }

    public IMovable Movable => _mover;
    public IHealth Health => _health;
}