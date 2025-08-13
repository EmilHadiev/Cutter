using UnityEngine;
using Zenject;

public class SceneReloader : MonoBehaviour
{
    [Inject]
    private readonly ISceneLoader _sceneLoader;

    public void Reload() => _sceneLoader.Restart();
}