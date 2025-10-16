using UnityEngine;
using Zenject;

public class MapSpawner : MonoBehaviour
{
    [Inject]
    private readonly IFactory _factory;

    public Map CreateMapAndGetSpline(Map prefab)
    {
        var pref = _factory.Create(prefab.gameObject);

        Map map = pref.GetComponent<Map>();
        return map;
    }
}