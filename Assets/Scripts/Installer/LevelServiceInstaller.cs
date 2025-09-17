using Zenject;

public class LevelServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPauseService();
        BindGameStarterService();
        BindCombatSessionService();
        BindGameOverService();
        BindFactory();
        BindAddressablesLoader();
    }

    private void BindPauseService()
    {
        Container.BindInterfacesTo<Pause>().AsSingle();
    }

    private void BindGameStarterService()
    {
        Container.BindInterfacesTo<GameStarter>().AsSingle();
    }

    private void BindCombatSessionService()
    {
        Container.BindInterfacesTo<CombatSession>().AsSingle();
    }

    private void BindGameOverService()
    {
        Container.BindInterfacesTo<GameOverService>().AsSingle();
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