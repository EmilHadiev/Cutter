using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.Map, fileName = "MapsContainer")]
public class MapsContainer : ScriptableObject
{
    [SerializeField] private Map[] _maps;

    public Map GetMap(int mapIndex) => _maps[mapIndex];
}