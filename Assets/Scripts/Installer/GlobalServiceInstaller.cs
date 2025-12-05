using UnityEngine;
using Zenject;

public class GlobalServiceInstaller : MonoInstaller
{
    [SerializeField] private GamePlaySoundContainer _gamePlayerSoundContainer;
    [SerializeField] private AmbientSoundContainer _ambientSoundContainer;
    [SerializeField] private UISoundContainer _uiSoundContainer;
    [SerializeField] private EnemySoundContainer _enemySoundContainer;

    public override void InstallBindings()
    {
        BindSceneLoader();
        BindSoundContainer();
        BindAdvService();
        BindSaveService();
        BindCoinsCalculator();
        BindCoinStorage();
        BindGameReadyService();
        BindLeaderBoardService();
        BindMetricService();
    }

    private void BindMetricService()
    {
        Container.BindInterfacesAndSelfTo<MetricService>().AsSingle();
    }

    private void BindLeaderBoardService()
    {
        Container.BindInterfacesTo<LeaderBoard>().AsSingle();
    }

    private void BindGameReadyService()
    {
        Container.BindInterfacesTo<GameReadyService>().AsSingle();
    }

    private void BindCoinStorage()
    {
        Container.BindInterfacesTo<CoinsStorage>().AsSingle();
    }

    private void BindCoinsCalculator()
    {
        Container.Bind<CoinsCalculator>().AsSingle();
    }

    private void BindSaveService()
    {
        Container.BindInterfacesTo<Saver>().AsSingle();
    }

    private void BindAdvService()
    {
        Container.BindInterfacesTo<AdvService>().AsSingle();
    }

    private void BindSoundContainer()
    {
        Container.BindInterfacesTo<GamePlaySoundContainer>().FromComponentInNewPrefab(_gamePlayerSoundContainer).AsSingle();
        Container.BindInterfacesTo<AmbientSoundContainer>().FromComponentInNewPrefab(_ambientSoundContainer).AsSingle().NonLazy();
        Container.BindInterfacesTo<UISoundContainer>().FromComponentInNewPrefab(_uiSoundContainer).AsSingle();
        Container.BindInterfacesTo<EnemySoundContainer>().FromComponentInNewPrefab(_enemySoundContainer).AsSingle();
    }

    private void BindSceneLoader()
    {
        Container.BindInterfacesTo<AddressablesSceneLoader>().AsSingle();
    }
}