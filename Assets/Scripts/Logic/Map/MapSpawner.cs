using SplineMesh;
using UnityEngine;
using Zenject;
using System;

public class MapSpawner : MonoBehaviour
{
    [Inject]
    private readonly IFactory _factory;

    public Spline CreateMapAndGetSpline(Map map)
    {
        var prefab = _factory.Create(map.gameObject);

        if (prefab.TryGetComponent(out Map mapPrefab))
            return mapPrefab.Spline;

        throw new ArgumentException(nameof(map));
    }
}