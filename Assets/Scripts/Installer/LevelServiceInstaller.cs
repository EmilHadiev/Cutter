using UnityEngine;
using Zenject;

public class LevelServiceInstaller : MonoInstaller
{
    [SerializeField] private UIStateMachine _stateMachine;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BonusSpawner _bonusSpawner;

    public override void InstallBindings()
    {
        BindPauseService();
        BindGameStarterService();
        BindCombatSessionService();
        BindGameOverService();
        BindFactory();
        BindAddressablesLoader();
        BindUIStateMachine();
        BindRewardService();
        BindSpawners();
    }

    private void BindSpawners()
    {
        Container.BindInterfacesTo<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
        Container.BindInterfacesTo<BonusSpawner>().FromInstance(_bonusSpawner).AsSingle();
    }

    private void BindRewardService()
    {
        Container.BindInterfacesTo<RewardService>().AsSingle();
    }

    private void BindUIStateMachine()
    {
        Container.BindInterfacesTo<UIStateMachine>().FromInstance(_stateMachine).AsSingle();
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