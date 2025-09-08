using SplineMesh;
using UnityEngine;

public class PlayerMovePattern : IMover
{
    private readonly Spline _spline;
    private readonly float _speed;
    private readonly Transform _player;

    private float _splinePosition = 0f;

    private bool _isWorking;

    public PlayerMovePattern(Spline spline, Transform transform, float speed)
    {
        _spline = spline;
        _speed = speed;
        _player = transform;
    }

    public void StartMove() => _isWorking = true;

    public void StopMove() => _isWorking = false;

    public void Update()
    {
        if (_isWorking == false)
            return;

        _splinePosition += _speed * Time.deltaTime;

        if (_splinePosition <= _spline.nodes.Count - 1)
        {
            GetPlace();
        }
    }

    private void GetPlace()
    {
        CurveSample sample = _spline.GetSample(_splinePosition);

        _player.localPosition = sample.location;
        _player.localRotation = sample.Rotation;
    }
}
