using System;
using UnityEngine;
using Zenject;

public class LevelServiceInstaller : MonoInstaller
{
    [SerializeField] private UIStateMachine _stateMachine;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private ProjectileSpawnContainer _projectileSpawnContainer;
    [SerializeField] private TrapSpawner _trapSpawner;
    [SerializeField] private TicTacToeSpawner _ticTacToeSpawner;
    [SerializeField] private CameraColorChanger _cameraColor;

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
        BindCameraColorChanger();
    }

    private void BindCameraColorChanger()
    {
        Container.BindInterfacesTo<CameraColorChanger>().FromInstance(_cameraColor).AsSingle();
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
        Container.BindInterfacesTo<TrapSpawner>().FromInstance(_trapSpawner).AsSingle();
        Container.BindInterfacesTo<TicTacToeSpawner>().FromInstance(_ticTacToeSpawner).AsSingle();
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