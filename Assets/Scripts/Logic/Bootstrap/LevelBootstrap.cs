using System.Collections.Generic;
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
    private IMetricService _metric;
    private PlayerData _data;

    private void Awake()
    {
        CreateMap();
        BuildNavMesh();
        SetCurrentLevelText();
        SendMetric();
    }

    [Inject]
    private void Constructor(PlayerProgress playerProgress, IMetricService metricService, PlayerData data)
    {
        _progress = playerProgress;
        _metric = metricService;
        _data = data;
    }

    private void SetCurrentLevelText()
    {
        _currentLevelText.text = GetCurrentLevel().ToString();
    }

    private void BuildNavMesh()
    {
        _surface.BuildNavMesh();
    }

    private void CreateMap()
    {
        int level = GetCurrentLevel();

        var template = _mapsContainer.GetMap(level);

        var mapPrefab = _mapSpawner.CreateMapAndGetSpline(template);
        var color = _mapsContainer.GetMapColor(level);

        mapPrefab.SetColor(color);
        _playerMover.SetSpline(mapPrefab.Spline);
        ChangeBackgroundColor(color.BackgroundColor);
    }

    private int GetCurrentLevel()
    {
        if (_progress.IsHardcoreMode)
            return _progress.CurrentHardcoreLevel;

        return _progress.CurrentLevel;
    }

    private void ChangeBackgroundColor(Color color)
    {
        _camera.backgroundColor = color;
    }

    private void SendMetric()
    {
        _metric.SendMetric(MetricsName.LoadGame);

        Dictionary<string, string> data = new Dictionary<string, string>(2)
        {
            { MetricsName.ParticleSelect, _data.Particle.ToString() },
            { MetricsName.SwordSelect, _data.Sword.ToString() }
        };

        _metric.SendMetric(MetricsName.Views, data);
    }
}