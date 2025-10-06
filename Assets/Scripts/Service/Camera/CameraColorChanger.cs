using UnityEngine;

public class CameraColorChanger : MonoBehaviour, ICameraColorChanger
{
    [SerializeField] private Camera _camera;

    private void OnValidate()
    {
        _camera = Camera.main;
    }

    public void SetColor(Color color)
    {
        _camera.backgroundColor = color;
    }
}
