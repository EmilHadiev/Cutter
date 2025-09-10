using SplineMesh;
using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour, IMovable
{
    [SerializeField] private Spline _spine;

    [Inject]
    private PlayerData _data;

    private IMover _mover;

    public Transform Transform => transform;

    public FloatProperty MoveSpeed => throw new System.NotImplementedException();

    private void Awake()
    {
        _mover = new PlayerMovePattern(_spine, Transform, _data);
        SetMove(_mover);
    }

    public void SetMove(IMover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }

    public void StartMove()
    {
        enabled = true;
        _mover.StartMove();
    }

    public void StopMove()
    {
        enabled = false;
        _mover.StopMove();
    }

    private void Update()
    {
        _mover.Update();
    }
}