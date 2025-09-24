using SplineMesh;
using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour, IMovable, ISpeedChangable
{
    [SerializeField] private Spline _spine;

    [Inject]
    private PlayerData _data;

    private IMover _mover;
    private FloatProperty _speed;
    private float _defaultSpeed;

    public Transform Transform => transform;

    private void Awake()
    {
        _defaultSpeed = _data.Speed;
        _speed = new FloatProperty(_data.Speed);
        _mover = new PlayerMovePattern(_spine, Transform, _data, _speed);

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
        if (gameObject == null)
            return;

        enabled = true;
        _mover.StartMove();
    }

    public void StopMove()
    {
        if (gameObject == null)
            return;

        enabled = false;
        _mover.StopMove();
    }

    public void SpeedUp(float speed) => _speed.Value = speed;
    public void SetDefaultSpeed() => _speed.Value = _defaultSpeed;

    private void Update()
    {
        _mover.Update();
    }
}