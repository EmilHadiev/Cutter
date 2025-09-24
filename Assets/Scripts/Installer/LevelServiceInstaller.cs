using UnityEngine;
using Zenject;

public class LevelServiceInstaller : MonoInstaller
{
    [SerializeField] private UIStateMachine _stateMachine;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private ProjectileSpawnContainer _projectileSpawnContainer;

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
        BindSlowMotionService();
        BindSpawners();
    }

    private void BindSlowMotionService()
    {
        Container.BindInterfacesTo<SlowMotion>().AsSingle();
    }

    private void BindSpawners()
    {
        Container.BindInterfacesTo<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
        Container.BindInterfacesTo<BonusSpawner>().FromInstance(_bonusSpawner).AsSingle();
        Container.BindInterfacesTo<ProjectileSpawnContainer>().FromInstance(_projectileSpawnContainer).AsSingle();
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