using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private GamePlaySoundContainer _gamePlayerSoundContainer;
    [SerializeField] private AmbientSoundContainer _ambientSoundContainer;

    public override void InstallBindings()
    {
        BindSoundContainer();
        BindSceneLoader();
        BindAddressablesLoader();
        BindFactory();
        BindGameOverService();
        BindCombatSessionInstaller();
    }

    private void BindCombatSessionInstaller()
    {
        Container.BindInterfacesTo<CombatSession>().AsSingle();
    }

    private void BindGameOverService()
    {
        Container.BindInterfacesTo<GameOverService>().AsSingle();
    }

    private void BindSoundContainer()
    {
        Container.BindInterfacesTo<GamePlaySoundContainer>().FromComponentInNewPrefab(_gamePlayerSoundContainer).AsSingle();
        Container.BindInterfacesTo<AmbientSoundContainer>().FromComponentInNewPrefab(_ambientSoundContainer).AsSingle().NonLazy();
    }

    private void BindSceneLoader()
    {
        Container.BindInterfacesTo<SceneLoader>().AsSingle();
    }

    private void BindFactory()
    {
        Container.BindInterfacesAndSelfTo<Factory>().AsSingle();
    }

    private void BindAddressablesLoader()
    {
        Container.BindInterfacesTo<AddressablesLoader>().AsSingle();
    }
}