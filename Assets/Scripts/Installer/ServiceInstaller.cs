using System;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSceneLoader();
        BindAddressablesLoader();
        BindFactory();
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