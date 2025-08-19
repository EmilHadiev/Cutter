using UnityEngine;

public class MousePosition : IMousePosition
{
    private const int DistanceToCamera = 10;

    private readonly Camera _camera;

    public MousePosition()
    {
        _camera = Camera.main;
    }    

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;

        // Проверяем границы экрана
        if (mousePos.x < 0 || mousePos.x > Screen.width ||
            mousePos.y < 0 || mousePos.y > Screen.height)
        {
            return Vector3.zero; // или предыдущая валидная позиция
        }

        mousePos.z = DistanceToCamera;
        return _camera.ScreenToWorldPoint(mousePos);
    }
}