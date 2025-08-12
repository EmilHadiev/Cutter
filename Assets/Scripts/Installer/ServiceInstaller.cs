using Zenject;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindAddresablesLoader();
    }

    private void BindAddresablesLoader()
    {
        Container.BindInterfacesTo<AddressablesLoaderService>().AsSingle();
    }
}