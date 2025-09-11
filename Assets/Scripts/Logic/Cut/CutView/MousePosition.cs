using UnityEngine;

public class MousePosition : IMousePosition
{
    private readonly Camera _camera;
    private readonly int _distanceToCamera = 10;

    public MousePosition(PlayerData playerData)
    {
        _camera = Camera.main;
        _distanceToCamera = playerData.AttackDistance;
    }    

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x < 0 || mousePos.x > Screen.width ||
            mousePos.y < 0 || mousePos.y > Screen.height)
        {
            return Vector3.zero;
        }

        mousePos.z = _distanceToCamera;
        return _camera.ScreenToWorldPoint(mousePos);
    }
}