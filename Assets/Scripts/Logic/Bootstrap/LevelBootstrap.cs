using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private MapSpawner _mapSpawner;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private MapsContainer _mapsContainer;
    [SerializeField] private TMP_Text _currentLevelText;

    private PlayerProgress _progress;

    private void Awake()
    {
        CreateMap();
        BuildNavMesh();
        SetCurrentLevelText();
    }

    [Inject]
    private void Constructor(PlayerProgress playerProgress)
    {
        _progress = playerProgress;
    }

    private void SetCurrentLevelText()
    {
        _currentLevelText.text = _progress.CurrentLevel.ToString();
    }

    private void BuildNavMesh()
    {
        _surface.BuildNavMesh();
    }

    private void CreateMap()
    {
        int level = _progress.CurrentLevel;

        var template = _mapsContainer.GetMap(level);

        var mapPrefab = _mapSpawner.CreateMapAndGetSpline(template);
        var color = _mapsContainer.GetMapColor(level);

        mapPrefab.SetColor(color);
        _playerMover.SetSpline(mapPrefab.Spline);
        ChangeBackgroundColor(color.BackgroundColor);
    }

    private void ChangeBackgroundColor(Color color)
    {
        _camera.backgroundColor = color;
    }
}