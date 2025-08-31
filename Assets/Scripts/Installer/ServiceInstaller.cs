using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private SoundContainer _soundContainer;

    public override void InstallBindings()
    {
        BindSoundContainer();
        BindSceneLoader();
        BindAddressablesLoader();
        BindFactory();
        BindGameOverService();
    }

    private void BindGameOverService()
    {
        Container.BindInterfacesTo<GameOverService>().AsSingle();
    }

    private void BindSoundContainer()
    {
        Container.BindInterfacesTo<SoundContainer>().FromComponentInNewPrefab(_soundContainer).AsSingle();
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