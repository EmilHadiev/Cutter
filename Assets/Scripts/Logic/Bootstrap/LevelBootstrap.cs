using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private MapSpawner _mapSpawner;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private MapsContainer _mapsContainer;

    private PlayerProgress _progress;

    private void Awake()
    {
        CreateMapAndGetSpline();
        BuildNavMesh();
    }

    [Inject]
    private void Constructor(PlayerProgress playerProgress)
    {
        _progress = playerProgress;
    }

    private void BuildNavMesh()
    {
        _surface.BuildNavMesh();
    }

    private void CreateMapAndGetSpline()
    {
        _playerMover.SetSpline(_mapSpawner.CreateMapAndGetSpline(_mapsContainer.GetMap(_progress.CurrentLevel)));
    }
}