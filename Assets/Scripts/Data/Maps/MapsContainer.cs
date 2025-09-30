using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.Map, fileName = "MapsContainer")]
public class MapsContainer : ScriptableObject
{
    [SerializeField] private Map[] _maps;

    public Map GetMap(int mapIndex)
    {
        if (_maps.Length < mapIndex + 1)
            return GetRandomMap();

        return _maps[mapIndex];
    }

    private Map GetRandomMap()
    {
        int index = Random.Range(0, _maps.Length);
        return _maps[index];
    }
}