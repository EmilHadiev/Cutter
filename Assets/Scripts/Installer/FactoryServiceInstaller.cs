using Zenject;

public class FactoryServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactory();
        BindAddressablesLoader();
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