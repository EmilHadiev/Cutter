using SplineMesh;
using UnityEngine;

public class PlayerMovePattern : IMover
{
    private readonly Spline _spline;
    private readonly Transform _player;

    private readonly FloatProperty _speed;
    private readonly float _rotationSpeed;

    private float _splinePosition = 0f;

    private bool _isWorking;

    public PlayerMovePattern(Spline spline, Transform transform, PlayerData data, FloatProperty speed)
    {
        _spline = spline;
        _player = transform;

        _speed = speed;
        _rotationSpeed = data.RotationSpeed;
    }

    public void StartMove()
    {
        _isWorking = true;
    }

    public void StopMove()
    {
        _isWorking = false;
    }

    public void Update()
    {
        if (_isWorking == false)
            return;

        _splinePosition += _speed.Value * Time.deltaTime;

        if (_splinePosition <= _spline.nodes.Count - 1)
        {
            GetPlace();
        }
    }

    private void GetPlace()
    {
        CurveSample sample = _spline.GetSample(_splinePosition);

        _player.localPosition = sample.location;   
        SetRotation(sample.Rotation);
    }

    private void SetRotation(Quaternion sample)
    {
        _player.localRotation = Quaternion.Slerp(
            _player.localRotation,
            sample,
            _rotationSpeed * Time.deltaTime
        );
    }
}
