using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerMover _mover;

    private void OnValidate()
    {
        _mover ??= GetComponent<PlayerMover>();
    }

    public IMovable Movable => _mover;
}