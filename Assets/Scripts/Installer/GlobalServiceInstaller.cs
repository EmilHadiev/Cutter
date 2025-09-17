using UnityEngine;
using Zenject;

public class GlobalServiceInstaller : MonoInstaller
{
    [SerializeField] private GamePlaySoundContainer _gamePlayerSoundContainer;
    [SerializeField] private AmbientSoundContainer _ambientSoundContainer;
    [SerializeField] private UISoundContainer _uiSoundContainer;

    public override void InstallBindings()
    {
        BindSceneLoader();
        BindSoundContainer();
    }

    private void BindSoundContainer()
    {
        Container.BindInterfacesTo<GamePlaySoundContainer>().FromComponentInNewPrefab(_gamePlayerSoundContainer).AsSingle();
        Container.BindInterfacesTo<AmbientSoundContainer>().FromComponentInNewPrefab(_ambientSoundContainer).AsSingle().NonLazy();
        Container.BindInterfacesTo<UISoundContainer>().FromComponentInNewPrefab(_uiSoundContainer).AsSingle();
    }

    private void BindSceneLoader()
    {
        Container.BindInterfacesTo<SceneLoader>().AsSingle();
    }
}