using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private ISavable _saver;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Constructor(ISavable savable, ISceneLoader sceneLoader)
    {
        _saver = savable;
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        ResetAll();
        LoadData();
        SwitchScene();
    }

    private void LoadData() => _saver.Load();
    private void SwitchScene() => _sceneLoader.SwitchTo(AssetProvider.Scenes.MainArena.ToString());

    private void ResetAll() => _saver.ResetAllProgress();
}