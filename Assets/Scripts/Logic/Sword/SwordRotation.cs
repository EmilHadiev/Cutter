using UnityEngine;

public class SwordRotation
{
    private const int XMin = -45;
    private const int XMax = 45;
    private const int YMin = -55;
    private const int YMax = 55;

    private Vector2 _currentRotation = Vector2.zero;

    public Vector2 GetRotateDirection(Vector3 mouseDirection)
    {
        float rotationX = mouseDirection.x;
        float rotationY = mouseDirection.y;

        _currentRotation.x = Mathf.Clamp(rotationX, XMin, XMax);
        _currentRotation.y = Mathf.Clamp(rotationY, YMin, YMax);

        return _currentRotation;
    }
}