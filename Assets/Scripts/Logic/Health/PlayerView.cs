using System.Collections;
using UnityEngine;

public class PlayerView
{
    private readonly Camera _camera;
    private readonly ParticleView _view;

    public PlayerView(Camera camera, ParticleView view)
    {
        _camera = camera;
        _view = view;
    }

    public void SetCameraPosition(Vector3 position)
    {
        _camera.transform.localPosition = position;
    }

    public void SetParent(Transform parent)
    {
        _camera.transform.parent = parent;
        _view.transform.parent = parent;
    }
}